
// Created: 22nd June 2010
// Updated: 15th July 2019

namespace Clasp.Util
{
    using global::Clasp;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///  Utility class for additional CLASP-related functionality.
    /// </summary>
    public static class UsageUtil
    {
        #region types

        /// <summary>
        ///  Aggregate of ShowUsage() parameters
        /// </summary>
        public struct UsageParams
        {
            /// <summary>
            ///  The info-lines, which may contain empty/<c>null</c> elements,
            ///  and the specific value <c>":version:"</c>.
            /// </summary>
            public string[]         InfoLines;
            /// <summary>
            ///  The flags and options string. If null or empty, the default
            ///  is used; if a whitespace-only string, the string is elided
            ///  from the usage
            /// </summary>
            public string           FlagsAndOptionsString;
            /// <summary>
            ///  The values string, e.g. <c>"&lt;input-path> &lt;output-path>"</c>
            /// </summary>
            public string           ValuesString;
            /// <summary>
            ///  An array of names for the values. <b>NOT CURRENTLY USED</b>
            /// </summary>
            public string[]         ValueNames;
            /// <summary>
            ///  A string placed after the matching value in the listing of an
            ///  option's range of values. If <c>null</c>, the default value
            ///  of <c>"(default)"</c> is used; if the empty string, no default is
            ///  used
            /// </summary>
            public string           DefaultIndicator;
        }

        private struct ShowUsageParams
        {
            internal IEnumerable<Specification> Specifications;
            internal UsageParams                UsageParams;
            internal TextWriter                 Stream;
            internal Assembly                   Assembly;
            internal string                     ProgramName;
            internal string                     VersionFormat;
            internal bool                       UseProductVersion;
            internal int?                       ExitCode;
            internal string                     Separator;
        }
        #endregion

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
            ///  The default flags-and-options-string
            /// </summary>
            public static string    FlagsAndOptionsString_Default   =   "[ ... flags and options ... ]";

            /// <summary>
            ///  OptionSpecification keys
            /// </summary>
            public static class OptionKeys
            {
                /// <summary>
                ///  OptionSpecification-key for specifying the assembly (of type
                ///  <see cref="System.Reflection.Assembly"/>) from which
                ///  the program version information will be elicited; if
                ///  not specified then the entry assembly will be used
                /// </summary>
                public const string Assembly                    =   "assembly";
                /// <summary>
                ///  OptionSpecification-key for specifying the program-name (of type
                ///  <see cref="System.String"/>); if not specified then the
                ///  program name will be inferred (via
                ///  <see cref="Clasp.Arguments.ProgramName"/>
                ///  property)
                /// </summary>
                public const string ProgramName                 =   "program-name";
                /// <summary>
                ///  OptionSpecification-key for specifying the separator (of type
                ///  <see cref="System.String"/>); defaults to
                ///  <see cref="Clasp.Util.UsageUtil.Constants.UsageSeparator_Default"/>
                /// </summary>
                public const string Separator                   =   "separator";
                /// <summary>
                ///  OptionSpecification-key for specifying the writer (of type
                ///  <see cref="System.IO.TextWriter"/>); if not
                ///  specified then the writer will be inferred as either
                ///  <see cref="System.Console.Out"/> or
                ///  <see cref="System.Console.Error"/>, depending on the
                ///  exit code
                /// </summary>
                public const string Writer                      =   "writer";
                /// <summary>
                ///  OptionSpecification-key for specifying the usage version format
                ///  string (of type <see cref="System.String"/>); defaults to
                ///  <see cref="Clasp.Util.UsageUtil.Constants.UsageVersionFormat_Default"/>
                /// </summary>
                public const string VersionFormat               =   "version-format";
                /// <summary>
                ///  OptionSpecification-key for requesting (of type
                ///  <see cref="System.Boolean"/>)
                ///  to cause product-version, rather than file-version, to be
                ///  used when eliciting version information
                /// </summary>
                public const string UseProductVersion           =   "use-product-version";
            }

            /// <summary>
            ///  Constants associated with the standard specifications.
            /// </summary>
            /// <seealso cref="Clasp.Util.UsageUtil.Help"/>
            /// <seealso cref="Clasp.Util.UsageUtil.Version"/>
            public static class StandardSpecifications
            {
                /// <summary>
                ///  The resolved name of
                ///  <see cref="Clasp.Util.UsageUtil.Help"/>.
                /// </summary>
                public const string Help_ResolvedName       =   @"--help";
                /// <summary>
                ///  The description of
                ///  <see cref="Clasp.Util.UsageUtil.Help"/>.
                /// </summary>
                public const string Help_Description        =   @"shows this help and terminates";

                /// <summary>
                ///  The resolved name of
                ///  <see cref="Clasp.Util.UsageUtil.Version"/>.
                /// </summary>
                public const string Version_ResolvedName    =   @"--version";
                /// <summary>
                ///  The description of
                ///  <see cref="Clasp.Util.UsageUtil.Version"/>.
                /// </summary>
                public const string Version_Description     =   @"shows version information and terminates";
            }
        }

        private static readonly IDictionary<string, object> EmptyOptions = new Dictionary<string, object>();
        #endregion

        #region properties

        /// <summary>
        ///  An instance of
        ///  <see cref="Clasp.FlagSpecification"/>
        ///  that provides default '--help' information.
        /// </summary>
        public static FlagSpecification Help = new FlagSpecification(null, Constants.StandardSpecifications.Help_ResolvedName, Constants.StandardSpecifications.Help_Description);

        /// <summary>
        ///  An instance of
        ///  <see cref="Clasp.FlagSpecification"/>
        ///  that provides default '--version' information.
        /// </summary>
        public static FlagSpecification Version = new FlagSpecification(null, Constants.StandardSpecifications.Version_ResolvedName, Constants.StandardSpecifications.Version_Description);
        #endregion

        #region usage methods

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code.
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode)
        {
            Debug.Assert(null != args);

            ShowUsage_(args.Specifications, exitCode, null, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code.
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowBoundUsageAndQuit<T>(Arguments args, int exitCode)
        {
            Debug.Assert(null != args);

            ShowBoundUsage_<T>(args.Specifications, exitCode, null, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowUsage_(args.Specifications, exitCode, null, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        public static void ShowBoundUsageAndQuit<T>(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowBoundUsage_<T>(args.Specifications, exitCode, null, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="usageParams"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="usageParams">
        ///  An instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode, UsageParams usageParams)
        {
            Debug.Assert(null != args);

            ShowUsage_(args.Specifications, exitCode, usageParams, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="usageParams"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="usageParams">
        ///  An instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        public static void ShowBoundUsageAndQuit<T>(Arguments args, int exitCode, UsageParams usageParams)
        {
            Debug.Assert(null != args);

            ShowBoundUsage_<T>(args.Specifications, exitCode, usageParams, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="usageParams"/>
        ///  and
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode, UsageParams usageParams, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowUsage_(args.Specifications, exitCode, usageParams, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications and exits the process
        ///  with the given exit code, according to the given
        ///  <paramref name="usageParams"/>
        ///  and
        ///  <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        public static void ShowBoundUsageAndQuit<T>(Arguments args, int exitCode, UsageParams usageParams, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowBoundUsage_<T>(args.Specifications, exitCode, usageParams, options);
        }

        /// <summary>
        ///  Shows usage for the specifications and exits the process
        ///  with the given exit code
        /// </summary>
        /// <param name="specifications">
        ///  The specifications used to list the usage
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated
        /// </param>
        [Obsolete("This method is obsolete. Use ShowUsageAndQuit(Arguments, int, UsageParams, IDictionary<string, options>) instead")]
        public static void ShowUsageAndQuit(IEnumerable<Specification> specifications, int exitCode)
        {
            ShowUsage_(specifications, exitCode, null, null);
        }

        /// <summary>
        ///  Writes the usage to the given writer.
        /// </summary>
        /// <param name="specifications">
        ///  The specifications used to list the usage
        /// </param>
        /// <param name="writer">
        ///  Output stream
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        [Obsolete("This method is obsolete. Use ShowUsage(Arguments, UsageParams, IDictionary<string, options>) instead")]
        public static int ShowUsage(IEnumerable<Specification> specifications, TextWriter writer)
        {
            if (null == writer)
            {
                return ShowUsage_(specifications, null, null, null);
            }
            else
            {
                IDictionary<string, object> options = AddOption_(null, Constants.OptionKeys.Writer, writer);

                return ShowUsage_(specifications, null, null, options);
            }
        }

        /// <summary>
        ///  Shows usage, based on the given <paramref name="specifications"/>m
        ///  to the given <paramref name="writer"/>, according to the
        ///  given <paramref name="options"/>
        /// </summary>
        /// <param name="specifications">
        ///  The specifications used to list the usage
        /// </param>
        /// <param name="writer">
        ///  Output stream
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        [Obsolete("This method is obsolete. Use ShowUsage(Arguments, UsageParams, IDictionary<string, options>) instead")]
        public static int ShowUsage(IEnumerable<Specification> specifications, TextWriter writer, IDictionary<string, object> options)
        {
            if (null != options)
            {
                options = new Dictionary<string, object>(options);
            }

            options = AddOption_(options, Constants.OptionKeys.Writer, writer);

            return ShowUsage_(specifications, null, null, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowUsage(Arguments args, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            return ShowUsage_(args.Specifications, null, null, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="usageParams"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowUsage(Arguments args, UsageParams usageParams)
        {
            Debug.Assert(null != args);

            return ShowUsage_(args.Specifications, null, usageParams, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="usageParams"/>
        ///  and
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowUsage(Arguments args, UsageParams usageParams, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            return ShowUsage_(args.Specifications, null, usageParams, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowBoundUsage<T>(Arguments args, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            return ShowBoundUsage_<T>(args.Specifications, null, null, options);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="usageParams"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowBoundUsage<T>(Arguments args, UsageParams usageParams)
        {
            Debug.Assert(null != args);

            return ShowBoundUsage_<T>(args.Specifications, null, usageParams, null);
        }

        /// <summary>
        ///  Shows usage for the attached specifications, according to the given
        ///  <paramref name="usageParams"/>
        ///  and
        ///  <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type, whose help elements may be merged into the given
        ///  <paramref name="args"/>' specifications
        /// </typeparam>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="usageParams">
        ///  A instance of the <see cref="Clasp.Util.UsageUtil.UsageParams"/> structure containing
        ///  elements that will be used to constitute the full usage
        ///  output
        /// </param>
        /// <param name="options">
        ///  Options by which the operation's behaviour will be modified
        /// </param>
        /// <returns>
        ///  The appropriate exit code for the process, which can be returned
        ///  from <c>Main()</c>
        /// </returns>
        public static int ShowBoundUsage<T>(Arguments args, UsageParams usageParams, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            return ShowBoundUsage_<T>(args.Specifications, null, usageParams, options);
        }
        #endregion

        #region version methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowVersionAndQuit(Arguments args, int exitCode)
        {
            Debug.Assert(null != args);

            ShowVersion_(exitCode, null);
        }

        /// <summary>
        ///  Shows the version and terminates the process
        /// </summary>
        /// <param name="args">
        ///  Parsed program arguments. May not be <c>null</c>
        /// </param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        /// <param name="options">
        ///  Options to modify behaviour
        /// (<seealso cref="Clasp.Util.UsageUtil.Constants.OptionKeys"/>)
        /// </param>
        public static void ShowVersionAndQuit(Arguments args, int exitCode, IDictionary<string, object> options)
        {
            Debug.Assert(null != args);

            ShowVersion_(exitCode, options);
        }

        /// <summary>
        ///  [DEPRECATED]
        /// </summary>
        /// <param name="specifications">
        /// </param>
        /// <param name="options">
        /// </param>
        [Obsolete("This method is obsolete. Use ShowVersion(Arguments, IDictionary<string, object>) instead")]
        public static void ShowVersion(IEnumerable<Specification> specifications, IDictionary<string, object> options)
        {
            ShowVersion_(null, options);
        }

        /// <summary>
        ///  Shows version
        /// </summary>
        /// <param name="args">
        ///  Program arguments obtained from CLASP parsing
        /// </param>
        /// <param name="options">
        ///  Options to modify behaviour
        ///  (<see cref="Clasp.Util.UsageUtil.Constants.OptionKeys"/>)
        /// </param>
        public static int ShowVersion(Arguments args, IDictionary<string, object> options)
        {
            return ShowVersion_(null, options);
        }

        /// <summary>
        ///  Shows version with all default behaviour
        /// </summary>
        public static int ShowVersion()
        {
            return ShowVersion_(null, null);
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
            if (null == assembly)
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

        private static int ShowBoundUsage_<T>(IEnumerable<Specification> specs, int? exitCode, UsageParams? usageParams, IDictionary<string, object> options)
        {
            Specification[] mergedSpecs = Invoker.MergeSpecificationsForBoundType<T>(specs);

            UsageParams?                mergedUsageParams;
            IDictionary<string, object> mergedOptions;

            Invoker.MergeUsageParamsAndOptionsForBoundType<T>(usageParams, options, out mergedUsageParams, out mergedOptions);

            return ShowUsage_(mergedSpecs, exitCode, mergedUsageParams, mergedOptions);
        }

        private static int ShowUsage_(IEnumerable<Specification> specs, int? exitCode, UsageParams? usageParams, IDictionary<string, object> options)
        {
            ShowUsageParams sups    =   new ShowUsageParams();

            sups.Specifications     =   specs;
            sups.ExitCode           =   InferExitCode_(exitCode, options);
            sups.Stream             =   InferWriter_(sups.ExitCode, options);

            sups.Assembly           =   GetOptionOrDefault_<Assembly>(options, Constants.OptionKeys.Assembly, Assembly.GetEntryAssembly());
            sups.ProgramName        =   GetOptionOrDefault_(options, Constants.OptionKeys.ProgramName, InferProgramName(sups.Assembly));
            sups.Separator          =   GetOptionOrDefault_(options, Constants.OptionKeys.Separator, Constants.UsageSeparator_Default);
            sups.UsageParams        =   usageParams.HasValue ? usageParams.Value : new UsageParams { FlagsAndOptionsString = Constants.FlagsAndOptionsString_Default };
            sups.UseProductVersion  =   GetOptionOrDefault_(options, Constants.OptionKeys.UseProductVersion, false);
            sups.VersionFormat      =   GetOptionOrDefault_(options, Constants.OptionKeys.VersionFormat, Constants.UsageVersionFormat_Default);

            ShowUsage_(sups);

            if (exitCode.HasValue)
            {
                Environment.Exit(exitCode.Value);
            }

            return Invoker.Constants.ExitCode_Success;
        }

        private static void ShowUsage_(ShowUsageParams sups)
        {
            Debug.Assert(null != sups.Stream);

            string[]    infoLines   =   (null != sups.UsageParams.InfoLines) ? sups.UsageParams.InfoLines : new string[0];
            string      separator   =   (null != sups.Separator) ? sups.Separator : "\t";

            foreach (string infoLine in infoLines)
            {
                switch(infoLine)
                {
                case ":version":
                case ":version:":

                    ShowVersion_(sups);
                    break;
                default:

                    sups.Stream.WriteLine(infoLine);
                    break;
                }
            }

            string  flagsAndOptions =   sups.UsageParams.FlagsAndOptionsString;
            string  usageValues     =   sups.UsageParams.ValuesString;
            bool    hasSpecs        =   (null == sups.Specifications) ? false : sups.Specifications.GetEnumerator().MoveNext();

            if (String.IsNullOrEmpty(flagsAndOptions))
            {
                if (null != sups.Specifications && hasSpecs)
                {
                    flagsAndOptions = "[ ... flags and options ... ]";
                }
                else
                {
                    flagsAndOptions = "";
                }
            }

            if (0 != flagsAndOptions.Trim().Length)
            {
                flagsAndOptions = " " + flagsAndOptions;
            }

            if (null == usageValues)
            {
                usageValues = "";
            }
            if (0 != usageValues.Trim().Length)
            {
                usageValues = " " + usageValues;
            }

            sups.Stream.WriteLine("USAGE: {0}{1}{2}", sups.ProgramName, flagsAndOptions, usageValues);
            sups.Stream.WriteLine();

            if (null != sups.Specifications)
            {
                string defaultIndicator = sups.UsageParams.DefaultIndicator;
                if (null == defaultIndicator)
                {
                    defaultIndicator = "(default)";
                }
                if (0 == defaultIndicator.Length)
                {
                    defaultIndicator = null;
                }

                if (hasSpecs)
                {
                    sups.Stream.WriteLine("flags/options:");
                    sups.Stream.WriteLine();

                    foreach (Specification specification in sups.Specifications)
                    {
                        switch (specification.Type)
                        {
                        case ArgumentType.None:
                            sups.Stream.WriteLine("{1}{0}", specification.Description, separator);
                            sups.Stream.WriteLine();
                            break;
                        case ArgumentType.Flag:
                            if (!String.IsNullOrEmpty(specification.GivenName))
                            {
                                sups.Stream.WriteLine("{1}{0}", specification.GivenName, separator);
                            }
                            if (!String.IsNullOrEmpty(specification.ResolvedName))
                            {
                                sups.Stream.WriteLine("{1}{0}", specification.ResolvedName, separator);
                            }
                            sups.Stream.WriteLine("{1}{1}{0}", specification.Description, separator);
                            sups.Stream.WriteLine();
                            break;
                        case ArgumentType.Option:
                            if (!String.IsNullOrEmpty(specification.GivenName))
                            {
                                sups.Stream.WriteLine("{1}{0} <value>", specification.GivenName, separator);
                            }
                            if (!String.IsNullOrEmpty(specification.ResolvedName))
                            {
                                sups.Stream.WriteLine("{1}{0}=<value>", specification.ResolvedName, separator);
                            }
                            sups.Stream.WriteLine("{1}{1}{0}", specification.Description, separator);

                            OptionSpecification optionSpecification = (OptionSpecification)specification;
                            if (0 != optionSpecification.ValidValues.Length)
                            {
                                sups.Stream.WriteLine("{0}{0}where <value> one of:", separator);
                                foreach (string value in optionSpecification.ValidValues)
                                {
                                    if (null != defaultIndicator && value == optionSpecification.DefaultValue)
                                    {
                                        sups.Stream.WriteLine("{1}{1}{1}{0}{1}{2}", value, separator, defaultIndicator);
                                    }
                                    else
                                    {
                                        sups.Stream.WriteLine("{1}{1}{1}{0}", value, separator);
                                    }
                                }
                            }
                            sups.Stream.WriteLine();
                            break;
                        }
                    }
                }
            }
            sups.Stream.Flush();
        }

        private static int ShowVersion_(int? exitCode, IDictionary<string, object> options)
        {
            ShowUsageParams sups    =   new ShowUsageParams();

            sups.Stream             =   InferWriter_(exitCode, options);
            sups.Assembly           =   GetOptionOrDefault_<Assembly>(options, Constants.OptionKeys.Assembly, Assembly.GetEntryAssembly());
            sups.ProgramName        =   GetOptionOrDefault_(options, Constants.OptionKeys.ProgramName, InferProgramName(sups.Assembly));
            sups.VersionFormat      =   GetOptionOrDefault_(options, Constants.OptionKeys.VersionFormat, Constants.UsageVersionFormat_Default);
            sups.UseProductVersion  =   GetOptionOrDefault_(options, Constants.OptionKeys.UseProductVersion, false);
            sups.ExitCode           =   exitCode;

            ShowVersion_(sups);

            if (exitCode.HasValue)
            {
                Environment.Exit(exitCode.Value);
            }

            return Invoker.Constants.ExitCode_Success;
        }

        private static void ShowVersion_(ShowUsageParams sups)
        {
            FileVersionInfo fvi         =   FileVersionInfo.GetVersionInfo(sups.Assembly.Location);

            if (null != sups.VersionFormat)
            {
                int             verMajor    =   sups.UseProductVersion ? fvi.ProductMajorPart : fvi.FileMajorPart;
                int             verMinor    =   sups.UseProductVersion ? fvi.ProductMinorPart : fvi.FileMinorPart;
                int             verRevision =   sups.UseProductVersion ? fvi.ProductPrivatePart : fvi.FilePrivatePart;
                int             verBuild    =   sups.UseProductVersion ? fvi.ProductBuildPart : fvi.FileBuildPart;

                sups.Stream.WriteLine(sups.VersionFormat, sups.ProgramName, verMajor, verMinor, verRevision, verBuild);
            }
            else
            {
                string          version     =   sups.UseProductVersion ? fvi.ProductVersion : fvi.FileVersion;

                sups.Stream.WriteLine("{0} {1}\n", sups.ProgramName, version);
            }

            sups.Stream.Flush();
        }

        private static int? InferExitCode_(int? exitCode, IDictionary<string, object> options)
        {
            if (exitCode.HasValue)
            {
                return exitCode;
            }

            return null;
        }

        private static TextWriter InferWriter_(int? exitCode, IDictionary<string, object> options)
        {
            if (null != options)
            {
                object v;

                if (options.TryGetValue(Constants.OptionKeys.Writer, out v))
                {
                    TextWriter writer = v as TextWriter;

                    if (null != writer)
                    {
                        return writer;
                    }
                }
            }

            if (exitCode.HasValue)
            {
                return (0 == exitCode) ? Console.Out : Console.Error;
            }

            return Console.Out;
        }

        private static IDictionary<string, object> AddOption_(IDictionary<string, object> options, string key, object value)
        {
            if (null == options)
            {
                options = new Dictionary<string, object>();
            }

            options[key] = value;

            return options;
        }

        private static bool GetOptionOrDefault_(IDictionary<string, object> options, string key, bool defaultValue)
        {
            if (null != options)
            {
                object v;

                if (options.TryGetValue(key, out v))
                {
                    return ParseUtil.ParseBool(v.ToString());
                }
            }

            return defaultValue;
        }

        private static object GetOptionOrDefault_(IDictionary<string, object> options, string key, object defaultValue)
        {
            if (null != options)
            {
                object v;

                if (options.TryGetValue(key, out v))
                {
                    return v;
                }
            }

            return defaultValue;
        }

        private static T GetOptionOrDefault_<T>(IDictionary<string, object> options, string key, T defaultValue) where T : class
        {
            if (null != options)
            {
                object v;

                if (options.TryGetValue(key, out v))
                {
                    T r = v as T;

                    if (null != r)
                    {
                        return r;
                    }
                }
            }

            return defaultValue;
        }
        #endregion
    }
}
