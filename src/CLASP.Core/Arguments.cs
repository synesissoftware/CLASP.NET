
// Created: 17th July 2009
// Updated: 14th July 2019

namespace Clasp
{
    using global::Clasp.Exceptions;
    using global::Clasp.Interfaces;
    using global::Clasp.Internal;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    ///  This class, the main API class for the library, represents a parsed
    ///  set of command-line arguments.
    /// </summary>
    public sealed class Arguments
    {
        #region constants

        private static readonly char[]  InvalidCharacters   =   Path.GetInvalidPathChars();
        private static readonly char[]  WildcardCharacters  =   new char[] { '*', '?' };
        private static readonly char[]  PathNameSeparators  =   new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
        private static readonly bool    PlatformIsWindows   =   PlatformIsWindows_;

        private static bool PlatformIsWindows_
        {
            get
            {
                switch(Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        return true;
                    case PlatformID.Unix:
                    case PlatformID.Xbox:
                    case PlatformID.MacOSX:
                        return false;
                    default:
                        Trace.WriteLine("unknown platform; id=" + Environment.OSVersion.Platform + ": assuming it is NOT a Windows platform; contact Synesis Software");
                        return false;
                }
            }
        }
        #endregion

        #region fields

        readonly ICollection<Specification> specifications  =   null;
        readonly List<IArgument>            arguments       =   new List<IArgument>();
        readonly List<IArgument>            flags           =   new List<IArgument>();
        readonly List<IArgument>            options         =   new List<IArgument>();
        readonly List<IArgument>            flagsAndOptions =   new List<IArgument>();
        readonly List<IArgument>            values          =   new List<IArgument>();
        readonly ParseOptions               m_parseOptions;
        readonly FailureOptions             m_failureOptions;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an <see cref="Clasp.Arguments"/> collection from the given
        ///  program arguments
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        public Arguments(string[] argv)
            : this(argv, null, ParseOptions.None, FailureOptions.Default)
        {}
        /// <summary>
        ///  Constructs an <see cref="Clasp.Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="options">
        ///  A combination of <see cref="Clasp.ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        public Arguments(string[] argv, ParseOptions options)
            : this(argv, null, options, FailureOptions.Default)
        {}
        /// <summary>
        ///  Constructs an <see cref="Clasp.Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="specifications"/>
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments
        /// </param>
        public Arguments(string[] argv, ICollection<Specification> specifications)
            : this(argv, specifications, ParseOptions.None, FailureOptions.Default)
        {}

        /// <summary>
        ///  Constructs an <see cref="Clasp.Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  and
        ///  <paramref name="parseOptions"/>
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="parseOptions">
        ///  A combination of <see cref="Clasp.ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        /// <param name="failureOptions">
        ///  A combination of <see cref="Clasp.FailureOptions">options</see>
        /// </param>
        public Arguments(string[] argv, ICollection<Specification> specifications, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            bool            treatAllAsValues    =   false;
            Argument        lastOption          =   null;
            List<string>    wildargs            =   new List<string>();
            string          cwd                 =   Environment.CurrentDirectory;

            for (int i = 0; i != argv.Length; ++i)
            {
                string  arg         =   TrimSingleQuotes(argv[i]);
                int     numHyphens  =   Argument.CountHyphens(arg);

                // 1. Ignore null/empty arguments

                if (null == arg)
                {
                    continue;
                }
                if ("" == arg)
                {
                    continue;
                }

                if ("--" == arg)
                {
                    if (!treatAllAsValues)
                    {
                        treatAllAsValues = true;
                        continue;
                    }
                }

                if (null != lastOption)
                {
                    lastOption.Value = arg;
                    lastOption = null;
                }
                else if (!treatAllAsValues && 0 != numHyphens)
                {
#if NON_EXISTENT

                    if (2 == numHyphens && 2 == arg.Length)
                    {
                        // "--"

                        treatAllAsValues = true;
                    }
                    else
#endif
                    {
                        // This is where the decision-making occurs:
                        //
                        // 1. If "-", then OptionSpecification, else
                        // 2. If contains '=', then OptionSpecification, checking alias, else
                        // 3. If an alias recognises the whole argument, then process it, else
                        // 4. If it has two (or more) hyphens, then OptionSpecification, else
                        // 5. If have specifications, treat each character in a one-hyphen argument as a flag, and process its alias (if defined), else
                        // 6. Treat as flag

                        if ("-" == arg)
                        {
                            // 1. If "-", then OptionSpecification

                            if (0 != (ParseOptions.TreatSinglehyphenAsValue & parseOptions))
                            {
                                AddValue(Argument.NewValue("-", i));
                            }
                            else
                            {
                                AddFlag(Argument.NewFlag(null, "-", "-", i));
                            }
                        }
                        else
                        {
                            int equal = arg.IndexOf('=');

                            if (equal >= 0)
                            {
                                // 2. Contains '=', so OptionSpecification

                                string name     =   arg.Substring(0, equal);
                                string value    =   arg.Substring(1 + equal);

                                Specification spec = FindSpecification_(specifications, name);

                                if (null != spec)
                                {
                                    if (String.IsNullOrEmpty(value))
                                    {
                                        OptionSpecification optionSpec = spec as OptionSpecification;

                                        if (null != optionSpec)
                                        {
                                            if (!String.IsNullOrEmpty(optionSpec.DefaultValue))
                                            {
                                                value = optionSpec.DefaultValue;
                                            }
                                        }
                                    }

                                    AddOption(Argument.NewOption(spec, spec.ResolvedName, name, value, i));
                                }
                                else
                                {
                                    AddOption(Argument.NewOption(null, name, name, value, i));
                                }
                            }
                            else
                            {
                                Specification spec = FindSpecification_(specifications, arg);

                                if (null != spec)
                                {
                                    equal = (null == spec.ResolvedName) ? -1 : spec.ResolvedName.IndexOf('=');

                                    if (equal >= 0)
                                    {
                                        // An option
                                        string name     =   spec.ResolvedName.Substring(0, equal);
                                        string value    =   spec.ResolvedName.Substring(1 + equal);

                                        Specification spec2 = FindSpecification_(specifications, name);

                                        if (null != spec2)
                                        {
                                            spec = spec2;
                                        }

                                        AddOption(Argument.NewOption(spec, arg, name, value, i));
                                    }
                                    else
                                    {
                                        if (ArgumentType.Option == spec.Type)
                                        {
                                            if (null == spec.ResolvedName)
                                            {
                                                lastOption = AddOption(Argument.NewOption(spec, arg, arg, null, i));
                                            }
                                            else
                                            {
                                                lastOption = AddOption(Argument.NewOption(spec, arg, spec.ResolvedName, null, i));
                                            }
                                        }
                                        else
                                        if (ArgumentType.Flag == spec.Type)
                                        {
                                            if (null == spec.ResolvedName)
                                            {
                                                AddFlag(Argument.NewFlag(spec, arg, arg, i));
                                            }
                                            else
                                            {
                                                AddFlag(Argument.NewFlag(spec, arg, spec.ResolvedName, i));
                                            }
                                        }
                                    }
                                }
                                else if (null != specifications && 1 == numHyphens)
                                {
                                    // 5. Treat each character in the argument as a flag, and process its alias (if defined)
                                    char[] flag = new char[2];

                                    flag[0] = '-';

                                    for (int j = 1; j != arg.Length; ++j)
                                    {
                                        flag[1] = arg[j];

                                        string arg2 = new string(flag);

                                        Specification spec2 = FindSpecification_(specifications, arg2);

                                        if (null != spec2)
                                        {
                                            int equal3 = spec2.ResolvedName.IndexOf('=');

                                            if (equal3 >= 0)
                                            {
                                                string name2    =   spec2.ResolvedName.Substring(0, equal3);
                                                string value2   =   spec2.ResolvedName.Substring(1 + equal3);

                                                Specification spec3 = FindSpecification_(specifications, name2);

                                                if (null != spec3)
                                                {
                                                    spec2 = spec3;
                                                }

                                                AddOption(Argument.NewOption(spec2, arg, name2, value2, i));
                                            }
                                            else
                                            {
                                                if (ArgumentType.Option == spec2.Type)
                                                {
                                                    lastOption = AddOption(Argument.NewOption(spec2, arg, spec2.ResolvedName, null, i));
                                                }
                                                else
                                                if (ArgumentType.Flag == spec2.Type)
                                                {
                                                    AddFlag(Argument.NewFlag(spec2, arg, spec2.ResolvedName, i));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AddFlag(Argument.NewFlag(null, arg, arg2, i));
                                        }
                                    }
                                }
                                else
                                {
                                    // 6. Treat as flag

                                    AddFlag(Argument.NewFlag(null, arg, arg, i));
                                }
                            }
                        }
                    }
                }
                else
                {
                    wildargs.Clear();

                    if (0 == (parseOptions & ParseOptions.DontExpandWildcardsOnWindows))
                    {
                        if (PlatformIsWindows)
                        {
                            if (arg.IndexOfAny(WildcardCharacters) >= 0 && arg.IndexOfAny(InvalidCharacters) < 0)
                            {
                                // There's a "vulnerability" in DirectoryInfo.GetFileSystemInfos() insofar
                                // as it cannot accept a non-relative path - it throws ArgumentException -
                                // so we do some jiggery-pokery to make sure that can't happen

                                try
                                {
                                    string              fullPath    =   Path.Combine(cwd, arg);
                                    string              dirPath     =   Path.GetDirectoryName(fullPath);
                                    string              fileName    =   Path.GetFileName(fullPath);

                                    DirectoryInfo       di          =   new DirectoryInfo(dirPath);

                                    FileSystemInfo[]    matches     =   di.GetFileSystemInfos(fileName);

                                    foreach(FileSystemInfo info in matches)
                                    {
                                        string  path        =   info.FullName;

                                        if (0 == String.Compare(cwd, 0, path, 0, cwd.Length))
                                        {
                                            int len = cwd.Length;

                                            switch(cwd[len - 1])
                                            {
                                            case '\\':
                                            case '/':
                                                break;
                                            default:
                                                ++len;
                                                break;
                                            }

                                            path = path.Substring(len);
                                        }

                                        wildargs.Add(path);
                                    }
                                }
                                catch (System.IO.PathTooLongException)
                                {}
                                catch (System.ArgumentException)
                                {}
                                catch (System.NotSupportedException)
                                {}
                            }
                        }
                    }

                    if (0 == wildargs.Count)
                    {
                        AddValue(Argument.NewValue(arg, i));
                    }
                    else
                    {
                        foreach(string warg in wildargs)
                        {
                            AddValue(Argument.NewValue(warg, i));
                        }
                    }
                }
            }

            if (null != lastOption)
            {
                OptionSpecification optionSpec = lastOption.Specification as OptionSpecification;

                if (null != optionSpec)
                {
                    Debug.Assert(String.IsNullOrEmpty(lastOption.Value));

                    lastOption.Value = optionSpec.DefaultValue;
                }
            }

            this.specifications = specifications;

            this.m_parseOptions = parseOptions;
            this.m_failureOptions = failureOptions;
        }
        #endregion

        #region operations

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int InvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain)
        {
            return InvokeMain(argv, specifications, ParseOptions.None, toolMain);
        }

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  and
        ///  <paramref name="options"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="options">
        ///  A combination of <see cref="Clasp.ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int InvokeMain(string[] argv, Specification[] specifications, ParseOptions options, ToolMain toolMain)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications);

            return toolMain(arguments);
        }

        /// <summary>
        ///  Searches the flags for the given argument name and, if found,
        ///  sets the given <paramref name="flag"/> value to the given
        ///  <paramref name="variable"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The flag type, which must a <c>struct</c>.
        /// </typeparam>
        /// <param name="resolvedName">
        ///  The resolved name of the flag.
        /// </param>
        /// <param name="flag">
        ///  The flag.
        /// </param>
        /// <param name="variable">
        ///  An in/out variable (of the same type as the <c>flag</c>
        ///  variable) whose value will be modified in respect of the
        ///  presence of the flag.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the named flag is present in the command-line
        ///  arguments; <b>false</b> otherwise.
        /// </returns>
        public bool CheckFlag<T>(string resolvedName, T flag, ref T variable) where T : struct
        {
            Debug.Assert(typeof(T).IsEnum, "flag and variable arguments must both be of enumeration type!");

            foreach(IArgument arg in this.flags)
            {
                if (arg.ResolvedName == resolvedName)
                {
                    int v0  =   FromEnum(flag);
                    int v1  =   FromEnum(variable);

                    Debug.Assert(0 != v0, "flag should not be 0");

                    v1 |= v0;

                    variable = CastTo<T>(v1);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Checks whether a given option is present, and obtain its
        ///  value as a string.
        /// </summary>
        /// <param name="resolvedName">
        ///  The resolved name of the option.
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the named option is present in the command-line
        ///  arguments; <b>false</b> otherwise.
        /// </returns>
        /// <remarks>
        ///  Marks the argument as <see cref="Clasp.Interfaces.IArgument.Used"/> if found
        /// </remarks>
        public bool CheckOption(string resolvedName, out string value)
        {
            IArgument arg = FindOption_(resolvedName);

            if (null != arg)
            {
                value = arg.Value;

                arg.Use();

                return true;
            }

            value = null;

            return false;
        }

        /// <summary>
        ///  Checks whether a given option is present, and obtain its
        ///  value as an integer.
        /// </summary>
        /// <param name="resolvedName">
        ///  The resolved name of the option.
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the named option is present in the command-line
        ///  arguments; <b>false</b> otherwise.
        /// </returns>
        /// <exception cref="Clasp.Exceptions.MissingOptionValueException">
        ///  Thrown if the given option's value is missing
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
        ///  Thrown if the given option's value cannot be converted to
        ///  <c>int</c>.
        /// </exception>
        /// <remarks>
        ///  Marks the argument as <see cref="Clasp.Interfaces.IArgument.Used"/> if found and
        ///  the result is obtained
        /// </remarks>
        public bool CheckOption(string resolvedName, out int value)
        {
            IArgument arg = FindOption_(resolvedName);

            if (null != arg)
            {
                if (String.IsNullOrEmpty(arg.Value))
                {
                    throw new MissingOptionValueException(arg);
                }

                if (!int.TryParse(arg.Value, out value))
                {
                    string n_msg;

                    if (Regex.IsMatch(arg.Value, @"^[-+]?\d+$"))
                    {
                        n_msg = "is not a number, or is too large";
                    }
                    else
                    {
                        n_msg = "is not a number";
                    }

                    throw new InvalidOptionValueException(arg, typeof(int), null, String.Format("'{0}' {1}", arg.Value, n_msg));
                }

                arg.Use();

                return true;
            }

            value = 0;

            return false;
        }

        /// <summary>
        ///  Obtains a required option
        /// </summary>
        /// <param name="resolvedName">
        ///  The name of the option
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <returns>
        ///  The option value
        /// </returns>
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option has not been specified
        /// </exception>
        public void RequireOption(string resolvedName, out string value)
        {
            if (!CheckOption(resolvedName, out value))
            {
                throw new MissingOptionException(resolvedName);
            }
        }

        /// <summary>
        ///  Obtains a required option
        /// </summary>
        /// <param name="resolvedName">
        ///  The name of the option
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <returns>
        ///  The option value
        /// </returns>
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option has not been specified
        /// </exception>
        /// <exception cref="Clasp.Exceptions.MissingOptionValueException">
        ///  Thrown if the given option's value is missing
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
        ///  Thrown if the given option's value cannot be converted to
        ///  <c>int</c>.
        /// </exception>
        public void RequireOption(string resolvedName, out int value)
        {
            if (!CheckOption(resolvedName, out value))
            {
                throw new MissingOptionException(resolvedName);
            }
        }

        /// <summary>
        ///  Checks whether the given flag is present.
        /// </summary>
        /// <param name="resolvedName">
        ///  The resolved name of the flag to search for.
        /// </param>
        /// <returns>
        ///  <b>true</b> if a flag of that name is found; <b>false</b>
        ///  otherwise.
        /// </returns>
        public bool HasFlag(string resolvedName)
        {
            foreach(IArgument arg in this.flags)
            {
                if (arg.ResolvedName == resolvedName)
                {
                    arg.Use();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Checks whether the given flag is present.
        /// </summary>
        /// <param name="flag">
        ///  The flag to search for.
        /// </param>
        /// <returns>
        ///  <b>true</b> if a flag of that name is found; <b>false</b>
        ///  otherwise.
        /// </returns>
        public bool HasFlag(FlagSpecification flag)
        {
            return HasFlag(flag.ResolvedName);
        }

        /// <summary>
        ///  Obtains a required value
        /// </summary>
        /// <param name="index">
        ///  The index of the value
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///  Thrown if the given index is invalid
        /// </exception>
        /// <exception cref="Clasp.Exceptions.MissingValueException">
        ///  Thrown if no value is available at the required index
        /// </exception>
        public void RequireValue(int index, out string value)
        {
            if (index < 0)
            {
                throw new System.ArgumentOutOfRangeException("index", index, "index must be non-negative");
            }

            if (!(index < Values.Count))
            {
                throw new MissingValueException(index);
            }

            value = Values[index].Value;
        }

        /// <summary>
        ///  Obtains a required value
        /// </summary>
        /// <param name="index">
        ///  The index of the value
        /// </param>
        /// <param name="value">
        ///  Variable into which the specified option's value is written.
        /// </param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///  Thrown if the given index is invalid
        /// </exception>
        /// <exception cref="Clasp.Exceptions.MissingValueException">
        ///  Thrown if no value is available at the required index
        /// </exception>
        public void RequireValue(int index, out int value)
        {
            string s;

            RequireValue(index, out s);

            try
            {
                value = int.Parse(s);
            }
            catch (System.FormatException x)
            {
                throw new InvalidOptionValueException(Values[index], typeof(int), x);
            }
        }

        /// <summary>
        ///  Provides a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{{#options: {0}; #flags: {1}; #values: {2}}}", Options.Count, Flags.Count, Values.Count);
        }
        #endregion

        #region properties

        /// <summary>
        ///  The inferred program name
        /// </summary>
        public static string ProgramName
        {
            get
            {
                return Process.GetCurrentProcess().ProcessName;
            }
        }

        /// <summary>
        ///  Failure options
        /// </summary>
        public FailureOptions FailureOptions
        {
            get
            {
                return m_failureOptions;
            }
        }

        /// <summary>
        ///  Parse options
        /// </summary>
        public ParseOptions ParseOptions
        {
            get
            {
                return m_parseOptions;
            }
        }

        /// <summary>
        ///  The specifications specified to the constructor, or <b>null</b> if
        ///  none were specified.
        /// </summary>
        public ICollection<Specification> Specifications
        {
            get
            {
                return specifications;
            }
        }

        /// <summary>
        ///  [DEPRECATED] Use <see cref="Clasp.Arguments.Specifications"/> instead
        /// </summary>
        [Obsolete("Use Specifications instead")]
        public ICollection<Specification> Aliases
        {
            get
            {
                return this.Specifications;
            }
        }

        /// <summary>
        ///  A collection of all parsed flags.
        /// </summary>
        public ReadOnlyCollection<IArgument> Flags
        {
            get
            {
                return new ReadOnlyCollection<IArgument>(flags);
            }
        }
        /// <summary>
        ///  A collection of all parsed options
        /// </summary>
        public ReadOnlyCollection<IArgument> Options
        {
            get
            {
                return new ReadOnlyCollection<IArgument>(options);
            }
        }
        /// <summary>
        ///  A collection of all parsed flags and options.
        /// </summary>
        public ReadOnlyCollection<IArgument> FlagsAndOptions
        {
            get
            {
                return new ReadOnlyCollection<IArgument>(flagsAndOptions);
            }
        }
        /// <summary>
        ///  A collection of all parsed values.
        /// </summary>
        public ReadOnlyCollection<IArgument> Values
        {
            get
            {
                return new ReadOnlyCollection<IArgument>(values);
            }
        }
        #endregion

        #region implementation

        private IArgument FindOption_(string resolvedName)
        {
            foreach(IArgument arg in this.options)
            {
                if (arg.ResolvedName == resolvedName)
                {
                    return arg;
                }
            }

            return null;
        }

        private void AddValue(Argument arg)
        {
            Debug.Assert(arg.Type == ArgumentType.Value);

            arguments.Add(arg);
            values.Add(arg);
        }

        private void AddFlag(Argument arg)
        {
            Debug.Assert(arg.Type == ArgumentType.Flag);

            arguments.Add(arg);
            flagsAndOptions.Add(arg);
            flags.Add(arg);
        }

        private Argument AddOption(Argument arg)
        {
            Debug.Assert(arg.Type == ArgumentType.Option);

            arguments.Add(arg);
            flagsAndOptions.Add(arg);
            options.Add(arg);
            return arg;
        }

        private static Specification FindSpecification_(ICollection<Specification> specifications, string name)
        {
            Debug.Assert(null != name);

            if (null == specifications)
            {
                return null;
            }
            else
            {
                foreach(Specification specification in specifications)
                {
                    if (specification.GivenName == name)
                    {
                        return specification;
                    }
                }

                foreach(Specification specification in specifications)
                {
                    if (specification.ResolvedName == name)
                    {
                        return specification;
                    }
                }
            }

            return null;
        }

        private static int FromEnum<T>(T value) where T : struct
        {
            Debug.Assert(typeof(T).IsEnum);

            return CastTo<int>(value);
        }

        private static T CastTo<T>(object o)
        {
            return (T)o;
        }

        private string TrimSingleQuotes(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                if (s.Length > 1)
                {
                    if ('\'' == s[0] && '\'' == s[s.Length - 1])
                    {
                        return s.Substring(1, s.Length - 2);
                    }
                }
            }

            return s;
        }
        #endregion
    }
}
