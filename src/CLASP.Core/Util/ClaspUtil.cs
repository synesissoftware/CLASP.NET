
// Created: 22nd June 2010
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Util
{
    using SynesisSoftware.SystemTools.Clasp.Exceptions;
    using SynesisSoftware.SystemTools.Clasp.Internal;
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    ///  Utility class for additional CLASP-related functionality.
    /// </summary>
    public static class ClaspUtil
    {
        #region Boolean parsing methods
        /// <summary>
        ///  Parses the given string for a <c>bool</c> value.
        /// </summary>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <returns>
        ///  The boolean value.
        /// </returns>
        /// <exception cref="System.FormatException">
        ///  Thrown if the string cannot be parsed as boolean.
        /// </exception>
        public static bool ParseBool(string s)
        {
            Debug.Assert(null != s);

            switch(s.ToLower())
            {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return bool.Parse(s);
            }
        }

        /// <summary>
        ///  Attemps to parse the given string for a <c>bool</c>
        ///  value.
        /// </summary>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <param name="value">
        ///  The boolean value.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the string can be parsed; <b>false</b>
        ///  otherwise.
        /// </returns>
        public static bool TryParseBool(string s, out bool value)
        {
            Debug.Assert(null != s);

            switch(s.ToLower())
            {
                case "yes":
                    value = true;
                    return true;
                case "no":
                    value = false;
                    return true;
                default:
                    return bool.TryParse(s, out value);
            }
        }
        #endregion

        #region Usage methods
        /// <summary>
        ///  Shows usage for the attached aliases and exits the process
        ///  with the given exit code.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowUsageAndQuit(Arguments args, int exitCode)
        {
            ShowUsageAndQuit(args.Aliases, exitCode);
        }

        /// <summary>
        ///  Shows usage for the aliases and exits the process
        ///  with the given exit code.
        /// </summary>
        /// <param name="aliases"></param>
        /// <param name="exitCode">
        ///  The code by which the process will be terminated.
        /// </param>
        public static void ShowUsageAndQuit(IEnumerable<Alias> aliases, int exitCode)
        {
            TextWriter writer = (0 == exitCode) ? Console.Out : Console.Error;

            ShowUsage(aliases, writer);

            Environment.Exit(exitCode);
        }

        /// <summary>
        ///  Writes the usage to the given writer.
        /// </summary>
        /// <param name="aliases"></param>
        /// <param name="writer"></param>
        public static void ShowUsage(IEnumerable<Alias> aliases, TextWriter writer)
        {
            if(null != aliases)
            {
                foreach(Alias alias in aliases)
                {
                    switch(alias.Type)
                    {
                        case ArgumentType.None:
                            writer.WriteLine("  {0}", alias.Description);
                            writer.WriteLine();
                            break;
                        case ArgumentType.Flag:
                            if(!String.IsNullOrEmpty(alias.GivenName))
                            {
                                writer.WriteLine("  {0}", alias.GivenName);
                            }
                            if(!String.IsNullOrEmpty(alias.ResolvedName))
                            {
                                writer.WriteLine("  {0}", alias.ResolvedName);
                            }
                            writer.WriteLine("    {0}", alias.Description);
                            writer.WriteLine();
                            break;
                        case ArgumentType.Option:
                            if(!String.IsNullOrEmpty(alias.GivenName))
                            {
                                writer.WriteLine("  {0}", alias.GivenName);
                            }
                            if(!String.IsNullOrEmpty(alias.ResolvedName))
                            {
                                writer.WriteLine("  {0}", alias.ResolvedName);
                            }
                            writer.WriteLine("    {0}", alias.Description);
                            writer.WriteLine();
                            break;
                    }
                }
            }
        }
        #endregion

        #region Flags and options operations
        /// <summary>
        ///  Determines whether the given flag is specified.
        /// </summary>
        /// <param name="args">
        /// </param>
        /// <param name="flagName">
        /// </param>
        /// <returns>
        ///
        /// </returns>
        public static bool FlagSpecified(Arguments args, string flagName)
        {
            foreach(Argument arg in args.Flags)
            {
                if(flagName == arg.ResolvedName)
                {
                    arg.Used = true;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static short CheckOption(Arguments args, string optionName, short defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => short.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static int CheckOption(Arguments args, string optionName, int defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => int.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static long CheckOption(Arguments args, string optionName, long defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => long.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static bool CheckOption(Arguments args, string optionName, bool defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => ParseBool(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static string CheckOption(Arguments args, string optionName, string defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => arg.Value);
        }

        //public static short RequireOption(Arguments args, string optionName)
        //{
        //    return SearchOption(args, optionName, (arg) => short.Parse(arg.Value));
        //}
        //public static int RequireOption(Arguments args, string optionName)
        //{
        //    return SearchOption(args, optionName, (arg) => int.Parse(arg.Value));
        //}
        //public static long RequireOption(Arguments args, string optionName)
        //{
        //    return SearchOption(args, optionName, (arg) => long.Parse(arg.Value));
        //}
        //public static string RequireOption(Arguments args, string optionName)
        //{
        //    return SearchOption(args, optionName, (arg) => arg.Value);
        //}

        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or throws an exception.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        /// <exception cref="MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static void RequireOption(Arguments args, string optionName, out short value)
        {
            value = SearchOption<short>(args, optionName, (arg) => short.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or throws an exception.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        /// <exception cref="MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static void RequireOption(Arguments args, string optionName, out int value)
        {
            value = SearchOption<int>(args, optionName, (arg) => int.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or throws an exception.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        /// <exception cref="MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static void RequireOption(Arguments args, string optionName, out long value)
        {
            value = SearchOption<long>(args, optionName, (arg) => long.Parse(arg.Value));
        }
        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or throws an exception.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        /// <exception cref="MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static void RequireOption(Arguments args, string optionName, out string value)
        {
            value = SearchOption<string>(args, optionName, (arg) => arg.Value);
        }

        /// <summary>
        ///  Verifies that all flags and options have been used, or throws
        ///  an exception.
        /// </summary>
        /// <param name="args">
        /// </param>
        /// <exception cref="UnusedArgumentException">
        ///  Thrown if any flags/options are not used.
        /// </exception>
        public static void VerifyAllOptionsUsed(Arguments args)
        {
            foreach(Argument arg in args.FlagsAndOptions)
            {
                if(!arg.Used)
                {
                    throw new UnusedArgumentException(arg.ResolvedName);
                }
            }
        }
        #endregion

        #region Implementation
        private delegate T Translate<T>(IArgument arg);

        private static T SearchOption<T>(Arguments args, string optionName, Translate<T> f)
        {
            foreach(Argument arg in args.Options)
            {
                if(optionName == arg.ResolvedName)
                {
                    arg.Used = true;

                    try
                    {
                        return f(arg);
                    }
                    catch(System.FormatException x)
                    {
                        throw new InvalidOptionValueException(arg, optionName, typeof(T), x);
                    }
                }
            }

            throw new MissingOptionException(optionName);
        }

        private static T SearchOptionOrDefault<T>(Arguments args, string optionName, T defaultValue, Translate<T> f)
        {
            foreach(Argument arg in args.Options)
            {
                if(optionName == arg.ResolvedName)
                {
                    arg.Used = true;

                    try
                    {
                        return f(arg);
                    }
                    catch(System.FormatException x)
                    {
                        throw new InvalidOptionValueException(arg, optionName, typeof(T), x);
                    }
                }
            }

            return defaultValue;
        }
        #endregion
    }
}
