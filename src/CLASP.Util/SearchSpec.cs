
// Created: 10th August 2009
// Updated: 13th August 2019

namespace Clasp.Util
{
    using Clasp.Interfaces;

    using Recls;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///  Represents a search specification pairing of directory and
    ///  pattern(s)
    /// </summary>
    public sealed class SearchSpec
    {
        #region Constants and Static Fields
        private static char[] WILDCARD_CHARACTERS = new char[] { '?', '*' };
        #endregion

        #region Properties

        /// <summary>
        ///  The directory
        /// </summary>
        public string Directory
        {
            get;
            private set;
        }

        /// <summary>
        ///  The patterns
        /// </summary>
        public string Patterns
        {
            get;
            private set;
        }
        #endregion

        #region Construction
        /// <summary>
        ///  Constructs an instance
        /// </summary>
        /// <param name="directory">
        ///  The directory. May NOT be <c>null</c>
        /// </param>
        /// <param name="patterns">
        ///  The patterns. May NOT be <c>null</c>
        /// </param>
        public SearchSpec(string directory, string patterns)
        {
            Directory = directory;
            Patterns = patterns;
        }

        /// <summary>
        ///  Gathers the search specifications from the given
        ///  <paramref name="values"/>
        ///  according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="values">
        ///  A sequence of value arguments
        /// </param>
        /// <param name="options">
        ///  Options
        /// </param>
        /// <returns></returns>
        public static SearchSpec[] Gather(IEnumerable<IArgument> values, GatherOptions options)
        {
            List<SearchSpec> specs = new List<SearchSpec>();
            string directory = null;
            List<string> patterns = new List<string>();

            foreach (IArgument value in values)
            {
                if (value.Value.IndexOfAny(WILDCARD_CHARACTERS) >= 0)
                {
                    patterns.Add(value.Value);
                }
                else
                {
                    IEntry entry = Recls.FileSearcher.Stat(value.Value);

                    if (null == entry)
                    {
                        patterns.Add(value.Value);
                    }
                    else if (entry.IsDirectory)
                    {
                        if (0 != patterns.Count)
                        {
                            // Already have specified directory/files, so need to
                            // push them into 
                            specs.Add(new SearchSpec(directory, JoinPatterns(patterns)));
                        }

                        directory = value.Value;
                        patterns.Clear();
                    }
                    else
                    {
                        patterns.Add(value.Value);
                    }
                }
            }
            if (0 != patterns.Count)
            {
                specs.Add(new SearchSpec(directory, JoinPatterns(patterns)));
            }
            else if (null != directory)
            {
                specs.Add(new SearchSpec(directory, FileSearcher.WildcardsAll));
            }

            if (0 == specs.Count)
            {
                if (GatherOptions.AddSearchAllSpecToEmptyList == (options & GatherOptions.AddSearchAllSpecToEmptyList))
                {
                    specs.Add(new SearchSpec(".", FileSearcher.WildcardsAll));
                }
            }

            return specs.ToArray();
        }
        #endregion

        #region Implementation
        private static string JoinPatterns(List<string> patterns)
        {
            return String.Join("|", patterns.ToArray());
        }
        #endregion
    }

    /// <summary>
    ///  Defines search specification-related extensions
    /// </summary>
    public static class SearchSpecExtensions
    {
        /// <summary>
        ///  Applies an <paramref name="action"/> to an array of
        ///  <see cref="SearchSpec"/> instances
        /// </summary>
        /// <param name="specs">
        ///  The <see cref="SearchSpec"/> instances
        /// </param>
        /// <param name="action">
        ///  The action to be performed on each <see cref="SearchSpec"/>
        ///  instance
        /// </param>
        public static void ForEach(this SearchSpec[] specs, Action<SearchSpec> action)
        {
            Debug.Assert(null != action);

            foreach (SearchSpec spec in specs)
            {
                action(spec);
            }
        }
    }
}
