
// Created: 17th July 2009
// Updated: 9th June 2015

namespace SynesisSoftware.SystemTools.Clasp
{
    using SynesisSoftware.SystemTools.Clasp.Exceptions;
    using SynesisSoftware.SystemTools.Clasp.Interfaces;
    using SynesisSoftware.SystemTools.Clasp.Internal;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    ///  This class, the main API class for the library, represents a parsed
    ///  set of command-line arguments.
    /// </summary>
    public sealed class Arguments
    {
        #region Constants
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

        #region Fields
        readonly ICollection<Alias> aliases         =   null;
        readonly List<IArgument>    arguments       =   new List<IArgument>();
        readonly List<IArgument>    flags           =   new List<IArgument>();
        readonly List<IArgument>    options         =   new List<IArgument>();
        readonly List<IArgument>    flagsAndOptions =   new List<IArgument>();
        readonly List<IArgument>    values          =   new List<IArgument>();
        #endregion

        #region Construction
        /// <summary>
        ///  Constructs an <see cref="Arguments"/> collection from the given
        ///  program arguments
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        public Arguments(string[] args)
            : this(args, null, ParseOptions.None)
        {}
        /// <summary>
        ///  Constructs an <see cref="Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        /// <param name="options">
        ///  A combination of <see cref="ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        public Arguments(string[] args, ParseOptions options)
            : this(args, null, options)
        {}
        /// <summary>
        ///  Constructs an <see cref="Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="aliases"/>
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        /// <param name="aliases">
        ///  Zero or more aliases that control the interpretation of the
        ///  arguments
        /// </param>
        public Arguments(string[] args, ICollection<Alias> aliases)
            : this(args, aliases, ParseOptions.None)
        {}
        /// <summary>
        ///  Constructs an <see cref="Arguments"/> collection from the given
        ///  program arguments, according to the given
        ///  <paramref name="aliases"/>
        ///  and
        ///  <paramref name="options"/>
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        /// <param name="aliases">
        ///  Zero or more aliases that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="options">
        ///  A combination of <see cref="ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        public Arguments(string[] args, ICollection<Alias> aliases, ParseOptions options)
        {
            bool            treatAllAsValues    =   false;
            Argument        lastOption          =   null;
            List<string>    wildargs            =   new List<string>();

            for(int i = 0; i != args.Length; ++i)
            {
                string  arg         =   TrimSingleQuotes(args[i]);
                int     numHyphens  =   Argument.CountHyphens(arg);

                // 1. Ignore null/empty arguments

                if(null == arg)
                {
                    continue;
                }
                if("" == arg)
                {
                    continue;
                }

                if(null != lastOption)
                {
                    Debug.Assert(null != lastOption);
                    lastOption.Value = arg;
                    lastOption = null;
                }
                else if(!treatAllAsValues && 0 != numHyphens)
                {
                    if(2 == numHyphens && 2 == arg.Length)
                    {
                        // "--"

                        treatAllAsValues = true;
                    }
                    else
                    {
                        // This is where the decision-making occurs:
                        //
                        // 1. If "-", then Option, else
                        // 2. If contains '=', then Option, checking alias, else
                        // 3. If an alias recognises the whole argument, then process it, else
                        // 4. If it has two (or more) hyphens, then Option, else
                        // 5. If have aliases, treat each character in a one-hyphen argument as a flag, and process its alias (if defined), else
                        // 6. Treat as flag

                        if("-" == arg)
                        {
                            // 1. If "-", then Option

                            AddOption(Argument.NewOption("-", i));
                        }
                        else
                        {
                            int equal = arg.IndexOf('=');

                            if(equal >= 0)
                            {
                                // 2. Contains '=', so Option

                                string name     =   arg.Substring(0, equal);
                                string value    =   arg.Substring(1 + equal);

                                Alias alias = FindAlias(aliases, name);

                                if(null != alias)
                                {
                                    AddOption(Argument.NewOption(alias.ResolvedName, name, value, i));
                                }
                                else
                                {
                                    AddOption(Argument.NewOption(name, name, value, i));
                                }
                            }
                            else
                            {
                                Alias alias = FindAlias(aliases, arg);

                                if(null != alias)
                                {
                                    equal = (null == alias.ResolvedName) ? -1 : alias.ResolvedName.IndexOf('=');

                                    if(equal >= 0)
                                    {
                                        // An option
                                        string name     =   alias.ResolvedName.Substring(0, equal);
                                        string value    =   alias.ResolvedName.Substring(1 + equal);

                                        AddOption(Argument.NewOption(arg, name, value, i));
                                    }
                                    else
                                    {
                                        if(ArgumentType.Option == alias.Type)
                                        {
                                            if(null == alias.ResolvedName)
                                            {
                                                lastOption = AddOption(Argument.NewOption(arg, i));
                                            }
                                            else
                                            {
                                                lastOption = AddOption(Argument.NewOption(arg, alias.ResolvedName, null, i));
                                            }
                                        }
                                        else
                                        if(ArgumentType.Flag == alias.Type)
                                        {
                                            if(null == alias.ResolvedName)
                                            {
                                                AddFlag(Argument.NewFlag(arg, i));
                                            }
                                            else
                                            {
                                                AddFlag(Argument.NewFlag(arg, alias.ResolvedName, i));
                                            }
                                        }
                                    }
                                }
                                else if(null != aliases && 1 == numHyphens)
                                {
                                    // 5. Treat each character in the argument as a flag, and process its alias (if defined)
                                    char[] flag = new char[2];

                                    flag[0] = '-';

                                    for(int j = 1; j != arg.Length; ++j)
                                    {
                                        flag[1] = arg[j];

                                        string arg2 = new string(flag);

                                        Alias alias2 = FindAlias(aliases, arg2);

                                        if(null != alias2)
                                        {
                                            int equal3 = alias2.ResolvedName.IndexOf('=');

                                            if(equal3 >= 0)
                                            {
                                                string name2    =   alias2.ResolvedName.Substring(0, equal);
                                                string value2   =   alias2.ResolvedName.Substring(1 + equal);

                                                AddFlag(Argument.NewOption(arg, name2, value2, i));
                                            }
                                            else
                                            {
                                                if(ArgumentType.Option == alias2.Type)
                                                {
                                                    lastOption = AddOption(Argument.NewOption(arg, alias2.ResolvedName, null, i));
                                                }
                                                else
                                                if(ArgumentType.Flag == alias2.Type)
                                                {
                                                    AddFlag(Argument.NewFlag(arg, alias2.ResolvedName, i));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AddFlag(Argument.NewFlag(arg, arg2, i));
                                        }
                                    }
                                }
                                else
                                {
                                    // 6. Treat as flag

                                    AddFlag(Argument.NewFlag(arg, i));
                                }
                            }
                        }
                    }
                }
                else
                {
                    wildargs.Clear();

                    if(0 == (options & ParseOptions.DontExpandWildcardsOnWindows))
                    {
                        if(PlatformIsWindows)
                        {
                            if(arg.IndexOfAny(WildcardCharacters) >= 0)
                            {
                                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);

                                FileSystemInfo[] matches = di.GetFileSystemInfos(arg);

                                foreach(FileSystemInfo info in matches)
                                {
                                    string  path        =   info.FullName;
#if NONE
                                    int     lastSlash   =   path.LastIndexOfAny(PathNameSeparators);
                                    string  file        =   (lastSlash < 0) ? path : path.Substring(lastSlash + 1);
#else
                                    string  file        =   path.Substring(di.FullName.Length + 1);
#endif

                                    wildargs.Add(file);
                                }
                            }
                        }
                    }

                    if(0 == wildargs.Count)
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

            this.aliases = aliases;
        }
        #endregion

        #region Operations

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="aliases"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        /// <param name="aliases">
        ///  Zero or more aliases that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <returns>
        ///  The return value from <code>toolMain</code>.
        /// </returns>
        public static int InvokeMain(string[] args, Alias[] aliases, ToolMain toolMain)
        {
            return InvokeMain(args, aliases, ParseOptions.None, toolMain);
        }

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="aliases"/>
        ///  and
        ///  <paramref name="options"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <param name="args">
        ///  The program arguments
        /// </param>
        /// <param name="aliases">
        ///  Zero or more aliases that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="options">
        ///  A combination of <see cref="ParseOptions">options</see> that
        ///  control the parsing behaviour
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <returns>
        ///  The return value from <code>toolMain</code>.
        /// </returns>
        public static int InvokeMain(string[] args, Alias[] aliases, ParseOptions options, ToolMain toolMain)
        {
            Debug.Assert(null != args);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(args, aliases);

            return toolMain(arguments);
        }

        /// <summary>
        ///  Searches the flags for the given argument name and, if found,
        ///  sets the given <paramref name="flag"/> value to the given
        ///  <paramref name="variable"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The flag type, which must a <code>struct</code>.
        /// </typeparam>
        /// <param name="resolvedName">
        ///  The resolved name of the flag.
        /// </param>
        /// <param name="flag">
        ///  The flag.
        /// </param>
        /// <param name="variable">
        ///  An in/out variable (of the same type as the <code>flag</code>
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
                if(arg.ResolvedName == resolvedName)
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
        public bool CheckOption(string resolvedName, out string value)
        {
            IArgument arg = FindOption_(resolvedName);

            if(null != arg)
            {
                value = arg.Value;

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
        /// <exception cref="System.FormatException">
        ///  Thrown if the given option's value cannot be converted to
        ///  <code>int</code>.
        /// </exception>
        public bool CheckOption(string resolvedName, out int value)
        {
            IArgument arg = FindOption_(resolvedName);

            if(null != arg)
            {
                if(String.IsNullOrEmpty(arg.Value))
                {
                    throw new MissingOptionValueException(arg, resolvedName);
                }

                if(!int.TryParse(arg.Value, out value))
                {
                    throw new InvalidOptionValueException(arg, resolvedName, typeof(int));
                }

                return true;
            }

            value = 0;

            return false;
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
                if(arg.ResolvedName == resolvedName)
                {
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
        public bool HasFlag(Flag flag)
        {
            return HasFlag(flag.ResolvedName);
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

        #region Properties
        /// <summary>
        ///  The aliases specified to the constructor, or <b>null</b> if
        ///  none were specified.
        /// </summary>
        public ICollection<Alias> Aliases
        {
            get { return aliases; }
        }
        /// <summary>
        ///  A collection of all parsed flags.
        /// </summary>
        public ReadOnlyCollection<IArgument> Flags
        {
            get { return new ReadOnlyCollection<IArgument>(flags); }
        }
        /// <summary>
        ///  A collection of all parsed options
        /// </summary>
        public ReadOnlyCollection<IArgument> Options
        {
            get { return new ReadOnlyCollection<IArgument>(options); }
        }
        /// <summary>
        ///  A collection of all parsed flags and options.
        /// </summary>
        public ReadOnlyCollection<IArgument> FlagsAndOptions
        {
            get { return new ReadOnlyCollection<IArgument>(flagsAndOptions); }
        }
        /// <summary>
        ///  A collection of all parsed values.
        /// </summary>
        public ReadOnlyCollection<IArgument> Values
        {
            get { return new ReadOnlyCollection<IArgument>(values); }
        }
        #endregion

        #region Implementation
        private IArgument FindOption_(string resolvedName)
        {
            foreach(IArgument arg in this.options)
            {
                if(arg.ResolvedName == resolvedName)
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
        private static Alias FindAlias(ICollection<Alias> aliases, string name)
        {
            Debug.Assert(null != name);

            if(null == aliases)
            {
                return null;
            }
            else
            {
                foreach(Alias alias in aliases)
                {
                    if(alias.GivenName == name)
                    {
                        return alias;
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
            if(!String.IsNullOrEmpty(s))
            {
                if(s.Length > 1)
                {
                    if('\'' == s[0] && '\'' == s[s.Length - 1])
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
