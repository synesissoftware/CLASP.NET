
// Created: 22nd June 2010
// Updated: 18th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Util
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    /// <summary>
    ///  Utility class for additional CLASP-related functionality.
    /// </summary>
    public static class UsageUtil
    {
        #region constants

        /// <summary>
        ///  Specifies well-known constants used by the library and
        ///  users
        /// </summary>
        public static class Constants
        {
            /// <summary>
            ///  The default format string for forming the usage version string
            /// </summary>
            public const string     UsageVersionFormat_Default  =   "{0} version {1}.{2}.{3} (build {4})";

            /// <summary>
            ///  The default usage separator
            /// </summary>
            public const string     UsageSeparator_Default      =   "  ";

            /// <summary>
            ///  Options keys
            /// </summary>
            public static class OptionKeys
            {
                /// <summary>
                ///  Option-key for specifying the assembly (of type
                ///  <see cref="System.Reflection.Assembly"/>) from which
                ///  the program version information will be elicited; if
                ///  not specified then the entry assembly will be used
                /// </summary>
                public const string Assembly                    =   "assembly";
                /// <summary>
                ///  Option-key for specifying the program-name (of type
                ///  <see cref="System.String"/>); if not specified then the
                ///  program name will be inferred (via
                ///  <see cref="SynesisSoftware.SystemTools.Clasp.Arguments.ProgramName"/>
                ///  property)
                /// </summary>
                public const string ProgramName                 =   "program-name";
                /// <summary>
                ///  Option-key for specifying the separator (of type
                ///  <see cref="System.String"/>); defaults to
                ///  <see cref="UsageSeparator_Default"/>
                /// </summary>
                public const string Separator                   =   "separator";
                /// <summary>
                ///  Option-key for specifying the writer (of type
                ///  <see cref="System.IO.TextWriter"/>); if not
                ///  specified then the writer will be inferred as either
                ///  <see cref="System.Console.Out"/> or
                ///  <see cref="System.Console.Error"/>, depending on the
                ///  exit code
                /// </summary>
                public const string Writer                      =   "writer";
                /// <summary>
                ///  Option-key for specifying the usage version format
                ///  string (of type <see cref="System.String"/>); defaults to
                ///  <see cref="UsageVersionFormat_Default"/>
                /// </summary>
                public const string VersionFormat               =   "version-format";
                /// <summary>
                ///  Option-key for requesting (of type
                ///  <see cref="System.Boolean"/>)
                ///  to cause product-version, rather than file-version, to be
                ///  used when eliciting version information
                /// </summary>
                public const string UseProductVersion           =   "use-product-version";
            }
        }

        private static readonly IDictionary<string, object> EmptyOptions = new Dictionary<string, object>();
        #endregion

        #region usage methods

        /// <summary>
        ///  Shows usage for the attached aliases and exits the process
        ///  with the given exit code.
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <code>null</code>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode)
        {
            Debug.Assert(null != args);

            ShowUsageAndQuit_(args, exitCode, null);
        }

        /// <summary>
        ///  Shows usage for the attached aliases and exits the process
        ///  with the given exit code
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <code>null</code>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="options">
        ///  Options by which the operation will be modified
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowUsageAndQuit_(args, exitCode, options);
        }

        /// <summary>
        ///  Shows usage for the aliases and exits the process
        ///  with the given exit code
        /// </summary>
        /// <param name="aliases">
        ///  The aliases used to list the usage
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        [Obsolete("This method is obsolete. Use ShowUsageAndQuit(Arguments, int) instead")]
        public static void ShowUsageAndQuit(IEnumerable<Alias> aliases, int exitCode)
        {
            ShowUsageAndQuit_(aliases, exitCode, null);
        }

        /// <summary>
        ///  Writes the usage to the given writer.
        /// </summary>
        /// <param name="aliases"></param>
        /// <param name="writer"></param>
        [Obsolete("This method is obsolete. Use ShowUsage(Arguments, IDictionary<string, options>) instead")]
        public static void ShowUsage(IEnumerable<Alias> aliases, TextWriter writer)
        {
            if(null == writer)
            {
                ShowUsage_(aliases, null, null);
            }
            else
            {
                IDictionary<string, object> options = AddOption_(null, Constants.OptionKeys.Writer, writer);

                ShowUsage_(aliases, null, options);
            }
        }

        /// <summary>
        ///  Shows usage, based on the given <paramref name="aliases"/>m
        ///  to the given <paramref name="writer"/>, according to the
        ///  given <paramref name="options"/>
        /// </summary>
        /// <param name="aliases">
        /// </param>
        /// <param name="writer">
        /// </param>
        /// <param name="options">
        /// </param>
        public static void ShowUsage(IEnumerable<Alias> aliases, TextWriter writer, IDictionary<string, object> options)
        {
            options = AddOption_(options, Constants.OptionKeys.Writer, writer);

            ShowUsage_(aliases, null, options);
        }
        #endregion

        #region version methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <code>null</code>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowVersionAndQuit(Arguments args, int exitCode)
        {
            ShowVersionAndQuit_(args, exitCode, null);
        }

        /// <summary>
        ///  Shows the version and terminates the process
        /// </summary>
        /// <param name="args"></param>
        /// <param name="exitCode"></param>
        /// <param name="options"></param>
        public static void ShowVersionAndQuit(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            ShowVersionAndQuit_(args, exitCode, options);
        }
        #endregion

        #region utility methods

        /// <summary>
        ///  Infers the program name from the given
        ///  <paramref name="assembly"/>, or the executing assembly
        /// </summary>
        /// <param name="assembly">
        ///  The assembly from which to infer the program name, or
        ///  <c>null</c> to s
        /// </param>
        /// <returns>
        ///  The name of the program
        /// </returns>
        public static string InferProgramName(Assembly assembly)
        {
            if(null == assembly)
            {
                return Arguments.ProgramName;
            }
            else
            {
                return Path.GetFileNameWithoutExtension(assembly.Location);
            }
        }
        #endregion

        #region implementation

        private static void ShowUsageAndQuit_(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowUsageAndQuit_(args.Aliases, exitCode, options);
        }

        private static void ShowUsageAndQuit_(IEnumerable<Alias> aliases, int? exitCode, IDictionary<string, object> options)
        {
            ShowUsage_(aliases, exitCode, options);

            if(exitCode.HasValue)
            {
                Environment.Exit(exitCode.Value);
            }
        }

        private static void ShowVersionAndQuit_(Arguments args, int? exitCode, IDictionary<string, object> options)
        {
            ShowVersion_(args.Aliases, exitCode, options);

            if(exitCode.HasValue)
            {
                Environment.Exit(exitCode.Value);
            }
        }

        private static void ShowUsage_(IEnumerable<Alias> aliases, int? exitCode, IDictionary<string, object> options)
        {
            options = InferOptions_(exitCode, options);

            string      separator   =   GetOptionOrDefault_(options, Constants.OptionKeys.Separator, Constants.UsageSeparator_Default);
            TextWriter  writer      =   GetOptionOrDefault_(options, Constants.OptionKeys.Writer, Console.Out);

            if(null != aliases)
            {
                foreach(Alias alias in aliases)
                {
                    switch(alias.Type)
                    {
                        case ArgumentType.None:
                            writer.WriteLine("{1}{0}", alias.Description, separator);
                            writer.WriteLine();
                            break;
                        case ArgumentType.Flag:
                            if(!String.IsNullOrEmpty(alias.GivenName))
                            {
                                writer.WriteLine("{1}{0}", alias.GivenName, separator);
                            }
                            if(!String.IsNullOrEmpty(alias.ResolvedName))
                            {
                                writer.WriteLine("{1}{0}", alias.ResolvedName, separator);
                            }
                            writer.WriteLine("{1}{1}{0}", alias.Description, separator);
                            writer.WriteLine();
                            break;
                        case ArgumentType.Option:
                            if(!String.IsNullOrEmpty(alias.GivenName))
                            {
                                writer.WriteLine("{1}{0} <value>", alias.GivenName, separator);
                            }
                            if(!String.IsNullOrEmpty(alias.ResolvedName))
                            {
                                writer.WriteLine("{1}{0}=<value>", alias.ResolvedName, separator);
                            }
                            writer.WriteLine("{1}{1}{0}", alias.Description, separator);
                            writer.WriteLine();
                            break;
                    }
                }
            }
        }

        private static void ShowVersion_(IEnumerable<Alias> aliases, int? exitCode, IDictionary<string, object> options)
        {
            options = InferOptions_(exitCode, options);

            TextWriter      writer      =   GetOptionOrDefault_(options, Constants.OptionKeys.Writer, Console.Out);
            Assembly        assembly    =   GetOptionOrDefault_(options, Constants.OptionKeys.Assembly, Assembly.GetEntryAssembly());
            string          programName =   GetOptionOrDefault_(options, Constants.OptionKeys.ProgramName, InferProgramName(assembly));
            string          versionFmt  =   GetOptionOrDefault_(options, Constants.OptionKeys.VersionFormat, Constants.UsageVersionFormat_Default);
            bool            useProdVer  =   GetOptionOrDefault_(options, Constants.OptionKeys.UseProductVersion, false);

            FileVersionInfo fvi         =   FileVersionInfo.GetVersionInfo(assembly.Location);

            int             verMajor    =   useProdVer ? fvi.ProductMajorPart : fvi.FileMajorPart;
            int             verMinor    =   useProdVer ? fvi.ProductMinorPart : fvi.FileMinorPart;
            int             verRevision =   useProdVer ? fvi.ProductPrivatePart : fvi.FilePrivatePart;
            int             verBuild    =   useProdVer ? fvi.ProductBuildPart : fvi.FileBuildPart;

            writer.WriteLine(versionFmt, programName, verMajor, verMinor, verRevision, verBuild);
        }

        private static IDictionary<string, object> InferOptions_(int? exitCode, IDictionary<string, object> givenOptions)
        {
            if(null == givenOptions)
            {
                givenOptions = EmptyOptions;
            }

            Dictionary<string, object>  options = new Dictionary<string, object>(givenOptions);
            object                      v;


            // "stream"

            if(exitCode.HasValue && !options.TryGetValue(Constants.OptionKeys.Writer, out v))
            {
                options[Constants.OptionKeys.Writer] = (0 == exitCode) ? Console.Out : Console.Error;
            }


            // "assembly"

            if(!options.TryGetValue(Constants.OptionKeys.Assembly, out v))
            {
                options[Constants.OptionKeys.Assembly] = Assembly.GetEntryAssembly();
            }


            return options;
        }

        private static IDictionary<string, object> AddOption_(IDictionary<string, object> options, string key, object value)
        {
            if(null == options)
            {
                options = new Dictionary<string, object>();
            }

            options[key] = value;

            return options;
        }

        private static bool GetOptionOrDefault_(IDictionary<string, object> options, string key, bool defaultValue)
        {
            Debug.Assert(null != options);

            object v;

            if(options.TryGetValue(key, out v))
            {
                return ParseUtil.ParseBool(v.ToString());
            }

            return defaultValue;
        }

        private static object GetOptionOrDefault_(IDictionary<string, object> options, string key, object defaultValue)
        {
            Debug.Assert(null != options);

            object v;

            if(options.TryGetValue(key, out v))
            {
                return v;
            }

            return defaultValue;
        }

        private static T GetOptionOrDefault_<T>(IDictionary<string, object> options, string key, T defaultValue) where T : class
        {
            Debug.Assert(null != options);

            object v;

            if(options.TryGetValue(key, out v))
            {
                T r = v as T;

                if(null != r)
                {
                    return r;
                }
            }

            return defaultValue;
        }
        #endregion
    }
}
