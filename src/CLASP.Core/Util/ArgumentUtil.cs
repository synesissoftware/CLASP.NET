
// Created: 22nd June 2010
// Updated: 13th July 2019

namespace Clasp.Util
{
    using global::Clasp.Exceptions;
    using global::Clasp.Internal;
    using global::Clasp.Interfaces;

    /// <summary>
    ///  Utility class for additional CLASP-related functionality.
    /// </summary>
    public static class ArgumentUtil
    {
        #region flags and options operations

        /// <summary>
        ///  Determines whether the given flag is specified.
        /// </summary>
        /// <param name="args">
        ///  The arguments
        /// </param>
        /// <param name="flagName">
        ///  The name of the flag to find
        /// </param>
        /// <returns>
        ///  <c>true</c> if the flag is found; <c>false</c> otherwise
        /// </returns>
        /// <remarks>
        ///  If found, the flag is marked as used
        /// </remarks>
        public static bool FlagSpecified(Arguments args, string flagName)
        {
            foreach(Argument arg in args.Flags)
            {
                if (flagName == arg.ResolvedName)
                {
                    arg.Used = true;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Finds an option with the given name
        /// </summary>
        /// <param name="args">
        ///  The arguments
        /// </param>
        /// <param name="optionName">
        ///  The name of the option to find
        /// </param>
        /// <returns>
        ///  The option, if found; <c>null</c> otherwise
        /// </returns>
        /// <remarks>
        ///  If found, the option is marked as used
        /// </remarks>
        public static IArgument FindOption(Arguments args, string optionName)
        {
            foreach(Argument arg in args.Options)
            {
                if (optionName == arg.ResolvedName)
                {
                    arg.Used = true;

                    return arg;
                }
            }

            return null;
        }

        /// <see cref="Clasp.Util.ArgumentUtil.FindOption(Arguments, string)"/>
        public static IArgument FindOption(Arguments args, OptionSpecification option)
        {
            return FindOption(args, option.ResolvedName);
        }

        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
        ///  Thrown if the value cannot be converted to the required type.
        /// </exception>
        public static bool CheckOption(Arguments args, string optionName, bool defaultValue)
        {
            return SearchOptionOrDefault(args, optionName, defaultValue, (arg) => ParseUtil.ParseBool(arg.Value));
        }

        /// <summary>
        ///  Checks for the presence, and obtains the value, of the given
        ///  option, or returns the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        /// <exception cref="Clasp.Exceptions.MissingOptionException">
        ///  Thrown if the given option does not exist.
        /// </exception>
        /// <exception cref="Clasp.Exceptions.InvalidOptionValueException">
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
        ///  The arguments object
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any flags/options are not used.
        /// </exception>
        public static void VerifyAllFlagsAndOptionsUsed(Arguments args)
        {
            foreach(Argument arg in args.FlagsAndOptions)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg);
                }
            }
        }

        /// <summary>
        ///  Verifies that all flags and options have been used, or throws
        ///  an exception carrying the custom <paramref name="message"/>
        /// </summary>
        /// <param name="args">
        ///  The arguments object
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception if thrown
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any flags/options are not used.
        /// </exception>
        public static void VerifyAllFlagsAndOptionsUsed(Arguments args, string message)
        {
            foreach(Argument arg in args.FlagsAndOptions)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg, message);
                }
            }
        }

        /// <summary>
        ///  Verifies that all flags have been used, or throws an exception
        /// </summary>
        /// <param name="args">
        ///  The arguments object
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any flags are not used.
        /// </exception>
        public static void VerifyAllFlagsUsed(Arguments args)
        {
            foreach(Argument arg in args.Flags)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg);
                }
            }
        }

        /// <summary>
        ///  Verifies that all flags have been used, or throws an exception
        ///  carrying the custom <paramref name="message"/>
        /// </summary>
        /// <param name="args">
        ///  The arguments object
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception if thrown
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any flags are not used.
        /// </exception>
        public static void VerifyAllFlagsUsed(Arguments args, string message)
        {
            foreach(Argument arg in args.Flags)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg, message);
                }
            }
        }

        /// <summary>
        ///  Verifies that all options have been used, or throws an exception
        /// </summary>
        /// <param name="args">
        ///  The arguments object
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any options are not used.
        /// </exception>
        public static void VerifyAllOptionsUsed(Arguments args)
        {
            foreach(Argument arg in args.Options)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg);
                }
            }
        }

        /// <summary>
        ///  Verifies that all options have been used, or throws an exception
        ///  carrying the custom <paramref name="message"/>
        /// </summary>
        /// <param name="args">
        ///  The arguments object
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception if thrown
        /// </param>
        /// <exception cref="Clasp.Exceptions.UnusedArgumentException">
        ///  Thrown if any options are not used.
        /// </exception>
        public static void VerifyAllOptionsUsed(Arguments args, string message)
        {
            foreach(Argument arg in args.Options)
            {
                if (!arg.Used)
                {
                    throw new UnusedArgumentException(arg, message);
                }
            }
        }
        #endregion

        #region implementation

        private delegate T Translate<T>(IArgument arg);

        private static T SearchOption<T>(Arguments args, string optionName, Translate<T> f)
        {
            foreach(Argument arg in args.Options)
            {
                if (optionName == arg.ResolvedName)
                {
                    arg.Used = true;

                    try
                    {
                        return f(arg);
                    }
                    catch (System.FormatException x)
                    {
                        throw new InvalidOptionValueException(arg, typeof(T), x);
                    }
                }
            }

            throw new MissingOptionException(optionName);
        }

        private static T SearchOptionOrDefault<T>(Arguments args, string optionName, T defaultValue, Translate<T> f)
        {
            foreach(Argument arg in args.Options)
            {
                if (optionName == arg.ResolvedName)
                {
                    arg.Used = true;

                    try
                    {
                        return f(arg);
                    }
                    catch (System.FormatException x)
                    {
                        throw new InvalidOptionValueException(arg, typeof(T), x);
                    }
                }
            }

            return defaultValue;
        }
        #endregion
    }
}
