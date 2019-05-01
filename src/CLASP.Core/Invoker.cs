
// Created: 17th July 2009
// Updated: 23rd April 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    /// <summary>
    ///  Class that provides ExecuteAroundMethod functionality for
    ///  implementing CLI entry points
    /// </summary>
    public static class Invoker
    {
        #region types

        private class RetVal<T> where T : new()
        {
            #region fields

            public bool             Succeeded;
            public int              ExitCode;
            private readonly object m_boundArguments;
            #endregion

            #region construction

            public RetVal()
            {
                Succeeded           =   false;
                ExitCode            =   1;
                m_boundArguments    =   new T();
            }
            #endregion

            #region properties

            public object BoundArguments
            {
                get
                {
                    return m_boundArguments;
                    //return Util.ReflectionUtil.CastTo<T>(m_boundArguments);
                }
            }
            #endregion
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
            ///  The default failure options
            /// </summary>
            public const FailureOptions             FailureOptions_Default  =   FailureOptions.HandleClaspExceptions | FailureOptions.SetExitCodeForVV | FailureOptions.AppendStandardUsagePromptToContingentReport;

            /// <summary>
            ///  The default parse options
            /// </summary>
            public const ArgumentBindingOptions     BindingOptions_Default  =   ArgumentBindingOptions.None;

            /// <summary>
            ///  Exit-code indicating success
            /// </summary>
            public const int                        ExitCode_Success        =   0;
            /// <summary>
            ///  Exit-code indicating failure
            /// </summary>
            public const int                        ExitCode_Failure        =   1;


            internal const string                   StandardUsagePrompt     =   @"; use --help for usage";
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
        public static int ParseAndInvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain)
        {
            return ParseAndInvokeMain(argv, specifications, toolMain, ParseOptions.None, Constants.FailureOptions_Default);
        }

        #region obsolete operations

        /// Obsolete
        [Obsolete("Use ParseAndInvokeMain instead")]
        public static int InvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain)
        {
            return InvokeMain(argv, specifications, toolMain, ParseOptions.None, Constants.FailureOptions_Default);
        }
        #endregion

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
        public static void ParseAndInvokeMainVA(string[] argv, Specification[] specifications, ToolMainVA toolMain)
        {
            ParseAndInvokeMainVA(argv, specifications, toolMain, ParseOptions.None, Constants.FailureOptions_Default);
        }

        #region obsolete operations

        /// Obsolete
        [Obsolete("Use ParseAndInvokeMainVA instead")]
        public static void InvokeMainVA(string[] argv, Specification[] specifications, ToolMainVA toolMain)
        {
            InvokeMainVA(argv, specifications, toolMain, ParseOptions.None, Constants.FailureOptions_Default);
        }
        #endregion

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  and
        ///  <paramref name="parseOptions"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <param name="argv">
        ///  The program arguments, as obtained from <c>Main()</c>
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <param name="parseOptions">
        ///  A combination of <see cref="Clasp.ParseOptions">parseOptions</see>
        ///  that control the parsing behaviour
        /// </param>
        /// <param name="failureOptions">
        ///  Options that control behaviour in the event of
        ///  <paramref name="toolMain"/> throwing an exception
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int ParseAndInvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions);

            return Do_ParseAndInvokeMain_IA_(toolMain, arguments, failureOptions);
        }

        #region obsolete operations

        /// Obsolete
        [Obsolete("Use ParseAndInvokeMain instead")]
        public static int InvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions);

            return Do_ParseAndInvokeMain_IA_(toolMain, arguments, failureOptions);
        }
        #endregion

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  and
        ///  <paramref name="parseOptions"/>,
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
        /// <param name="parseOptions">
        ///  A combination of <see cref="Clasp.ParseOptions">parseOptions</see>
        ///  that control the parsing behaviour
        /// </param>
        /// <param name="failureOptions">
        ///  Options that control behaviour in the event of
        ///  <paramref name="toolMain"/> throwing an exception
        /// </param>
        public static void ParseAndInvokeMainVA(string[] argv, Specification[] specifications, ToolMainVA toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions);

            Do_ParseAndInvokeMain_VA_(toolMain, arguments, failureOptions);
        }

        #region obsolete operations

        /// Obsolete
        [Obsolete("Use ParseAndInvokeMainVA instead")]
        public static void InvokeMainVA(string[] argv, Specification[] specifications, ToolMainVA toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions);

            Do_ParseAndInvokeMain_VA_(toolMain, arguments, failureOptions);
        }
        #endregion

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argv"></param>
        /// <param name="specifications"></param>
        /// <param name="toolMain"></param>
        /// <returns></returns>
        /// <example>
        /// </example>
        public static int ParseAndInvokeMainWithBoundArgumentOfType<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(argv, specifications, toolMain, null, ParseOptions.None, Constants.FailureOptions_Default);
        }

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  into an instance of the bound argument type
        ///  <typeparamref name="T"/>,
        ///  according to the
        ///  <paramref name="bindingOptions"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type.
        /// </typeparam>
        /// <param name="argv">
        ///  The program arguments, as obtained from <c>Main()</c>.
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments.
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic.
        /// </param>
        /// <param name="bindingOptions">
        ///  A combination of <see cref="Clasp.ArgumentBindingOptions"/>
        ///  that control the binding behaviour.
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int ParseAndInvokeMainWithBoundArgumentOfType<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions bindingOptions) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(argv, specifications, toolMain, bindingOptions, ParseOptions.None, Constants.FailureOptions_Default);
        }

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>,
        ///  <paramref name="parseOptions"/>,
        ///  and
        ///  <paramref name="failureOptions"/>
        ///  into an instance of the bound argument type
        ///  <typeparamref name="T"/>,
        ///  according to the
        ///  <paramref name="bindingOptions"/>,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type.
        /// </typeparam>
        /// <param name="argv">
        ///  The program arguments, as obtained from <c>Main()</c>.
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of the
        ///  arguments.
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic.
        /// </param>
        /// <param name="bindingOptions">
        ///  A combination of <see cref="Clasp.ArgumentBindingOptions"/>
        ///  that control the binding behaviour.
        /// </param>
        /// <param name="parseOptions">
        ///  A combination of <see cref="Clasp.ParseOptions">parseOptions</see>
        ///  that control the parsing behaviour
        /// </param>
        /// <param name="failureOptions">
        ///  Options that control behaviour in the event of
        ///  <paramref name="toolMain"/> throwing an exception
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int ParseAndInvokeMainWithBoundArgumentOfType<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions bindingOptions, ParseOptions parseOptions, FailureOptions failureOptions) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(argv, specifications, toolMain, bindingOptions, parseOptions, failureOptions);
        }

        /// <summary>
        ///  .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <param name="toolMain"></param>
        /// <returns></returns>
        public static int InvokeMainWithBoundArgumentOfType<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(args, toolMain, null);
        }

        /// <summary>
        ///  .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <param name="toolMain"></param>
        /// <param name="bindingOptions"></param>
        /// <returns></returns>
        public static int InvokeMainWithBoundArgumentOfType<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions bindingOptions) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(args, toolMain, bindingOptions);
        }
        #endregion

        #region implementation

        private static int Do_ParseAndInvokeMain_IA_(ToolMain toolMain, Arguments arguments, FailureOptions failureOptions)
        {
            int r = do_invoke_(arguments, failureOptions, (Arguments args) => {

                return toolMain(args);
            });

            return r;
        }

        private static void Do_ParseAndInvokeMain_VA_(ToolMainVA toolMain, Arguments arguments, FailureOptions failureOptions)
        {
            int r = do_invoke_(arguments, failureOptions, (Arguments args) => {

                toolMain(args);

                return Constants.ExitCode_Success;
            });

            if(0 != (FailureOptions.SetExitCodeForVV & failureOptions))
            {
                Environment.ExitCode = r;
            }

            if(0 != (FailureOptions.InvokeExitForVV & failureOptions))
            {
                Environment.Exit(r);
            }
        }

        private static int InvokeMainAndParseBoundArgumentOfType_<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions? bindingOptions, ParseOptions parseOptions, FailureOptions failureOptions) where T : new()
        {
            return Invoker.ParseAndInvokeMain(argv, specifications, (Arguments args2) => {

                RetVal<T> r = ParseBoundArguments_<T>(args2, bindingOptions);

                if(!r.Succeeded)
                {
                    return r.ExitCode;
                }

                return toolMain(Util.ReflectionUtil.CastTo<T>(r.BoundArguments), args2);
            }, parseOptions, failureOptions);
        }

        private static int InvokeMainAndParseBoundArgumentOfType_<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions? bindingOptions) where T : new()
        {
            RetVal<T> r = ParseBoundArguments_<T>(args, bindingOptions);

            if(!r.Succeeded)
            {
                return r.ExitCode;
            }

            return toolMain(Util.ReflectionUtil.CastTo<T>(r.BoundArguments), args);
        }

        private static int do_invoke_(Arguments arguments, FailureOptions failureOptions, Func<Arguments, int> f)
        {
            if(FailureOptions.None == failureOptions)
            {
                return f(arguments);
            }
            else
            {
                try
                {
                    return f(arguments);
                }
                catch(OutOfMemoryException /* x */)
                {
                    if(0 != (FailureOptions.HandleMemoryExceptions & failureOptions))
                    {
                        Console.Error.WriteLine("{0}: out of memory", Arguments.ProgramName);

                        return Constants.ExitCode_Success;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exceptions.UnusedArgumentException x)
                {
                    if(0 == (FailureOptions.HandleClaspExceptions & failureOptions))
                    {
                        throw;
                    }

                    Trace.WriteLine(string.Format("exception ({0}): {1}", x.GetType().FullName, x.Message));

                    Console.Error.WriteLine("{0}: {1}", Arguments.ProgramName, x.Message);
                }
                catch(Exceptions.ClaspException x)
                {
                    if(0 == (FailureOptions.HandleClaspExceptions & failureOptions))
                    {
                        throw;
                    }

                    Trace.WriteLine(string.Format("exception ({0}): {1}", x.GetType().FullName, x.Message));

                    Console.Error.WriteLine("{0}: {1}{2}", Arguments.ProgramName, x.Message, 0 != (FailureOptions.AppendStandardUsagePromptToContingentReport & failureOptions) ? Constants.StandardUsagePrompt : String.Empty);
                }
                catch(Exception x)
                {
                    Type x_type = x.GetType();

                    switch(x_type.FullName)
                    {
                    case "NUnit.Framework.AssertionException":
                    case "Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException":

                        throw;
                    }

                    if(0 == (FailureOptions.HandleSystemExceptions & failureOptions))
                    {
                        throw;
                    }

                    string traceMessage = string.Format("exception ({0}): {1}", x.GetType().FullName, x.Message);

                    Trace.WriteLine(traceMessage);

                    Console.Error.WriteLine("{0}: {1}", Arguments.ProgramName, x.Message);
                }

                return Constants.ExitCode_Failure;
            }
        }

        private static RetVal<T> ParseBoundArguments_<T>(Arguments args, ArgumentBindingOptions? bindingOptions) where T : new()
        {
            RetVal<T>       retVal      =   new RetVal<T>();

            Type            type        =   typeof(T);
            FieldInfo[]     fields      =   Util.ReflectionUtil.GetTypeFields(type);
            PropertyInfo[]  properties  =   type.GetProperties();
            bool[]          usedValues  =   new bool[args.Values.Count];

            // /////////////////////
            // type & parse-options

            Binding.BoundTypeAttribute  typeAttribute               =   Util.ReflectionUtil.GetFirstAttributeOrNull<Binding.BoundTypeAttribute>(type, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

            ArgumentBindingOptions      effectiveParseOptions       =   bindingOptions ?? Constants.BindingOptions_Default;

            if(null != typeAttribute)
            {
                if(typeAttribute.AttributeOptionsHavePrecedence || !bindingOptions.HasValue)
                {
                    effectiveParseOptions = typeAttribute.BindingOptions;
                }
            }

            // ///////////////////////////////////
            // iterate the fields

            foreach(var fi in fields)
            {
                // since we can't prevent BoundFlagAttribute and
                // BoundOptionAttribute being applied to the same field, we
                // search for them in turn

                // /////////////////////
                // Flag

                Binding.BoundFlagAttribute  flagAttribute   =   Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundFlagAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if(null != flagAttribute)
                {
                    // field must be bool

                    if(typeof(bool) != fi.FieldType)
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of type bool (System.Boolean)", fi.Name, typeof(Binding.BoundFlagAttribute), fi.FieldType));
                        Debug.Assert(typeof(bool) == fi.FieldType);

                        continue;
                    }

                    bool flagSpecified = Util.ArgumentUtil.FlagSpecified(args, flagAttribute.ResolvedName);

                    fi.SetValue(retVal.BoundArguments, flagSpecified);

                    continue;
                }

                // /////////////////////
                // Option

                Binding.BoundOptionAttribute    optionAttribute    =   Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundOptionAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if(null != optionAttribute)
                {
                    bool                    alFra   =   optionAttribute.AllowFraction;
                    bool                    alNeg   =   optionAttribute.AllowNegative;
                    Interfaces.IArgument    option  =   Util.ArgumentUtil.FindOption(args, optionAttribute.ResolvedName);
                    object                  value   =   null;

                    if(null == option)
                    {
                        if(null == optionAttribute.DefaultValue)
                        {
                            throw new Exceptions.MissingOptionException(optionAttribute.ResolvedName);
                        }
                        else
                        {
                            value = optionAttribute.DefaultValue;
                        }
                    }
                    else
                    {
                        if(String.IsNullOrEmpty(option.Value))
                        {
                            if(optionAttribute.AllowEmpty)
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                throw new Exceptions.MissingOptionValueException(option);
                            }
                        }
                        else
                        {
                            value = option.Value;
                        }
                    }

                    // Now must do conversion(s)

                    BoundFieldType bft = ObtainBoundFieldType(fi.FieldType);

                    switch(bft)
                    {
                    case BoundFieldType.Boolean:
                        value = Util.ParseUtil.ParseBool(value.ToString());
                        break;
                    case BoundFieldType.String:
                        // Nothing to do
                        break;
                    case BoundFieldType.SignedInt32:
                        value = int.Parse(value.ToString());
                        break;
                    case BoundFieldType.SignedInt64:
                        value = long.Parse(value.ToString());
                        break;
                    case BoundFieldType.UnsignedInt32:
                        value = uint.Parse(value.ToString());
                        break;
                    case BoundFieldType.UnsignedInt64:
                        value = ulong.Parse(value.ToString());
                        break;
                    default:
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' but its type is not supported by the current version of the library", fi.Name, typeof(Binding.BoundOptionAttribute), fi.FieldType));

                        continue;
                    }

                    fi.SetValue(retVal.BoundArguments, value);

                    continue;
                }

                // /////////////////////
                // Value

                Binding.BoundValueAttribute valueAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundValueAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if(null != valueAttribute)
                {
                    if(typeof(string) != fi.FieldType)
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of type string (System.String)", fi.Name, typeof(Binding.BoundValueAttribute), fi.FieldType));

                        continue;
                    }

                    object value;

                    if(valueAttribute.ValueIndex < args.Values.Count)
                    {
                        value = args.Values[valueAttribute.ValueIndex].Value;

                        usedValues[valueAttribute.ValueIndex] = true;
                    }
                    else
                    {
                        if(null == valueAttribute.DefaultValue)
                        {
                            if(0 != (ArgumentBindingOptions.IgnoreMissingValues & effectiveParseOptions))
                            {
                                continue;
                            }

                            if(String.IsNullOrWhiteSpace(valueAttribute.UsageLabel))
                            {
                                throw new Exceptions.MissingValueException(valueAttribute.ValueIndex);
                            }
                            else
                            {
                                string message = String.Format(@"value {0} not specified", valueAttribute.UsageLabel);

                                throw new Exceptions.MissingValueException(valueAttribute.ValueIndex, message);
                            }
                        }
                        else
                        {
                            value = valueAttribute.DefaultValue;
                        }
                    }

                    fi.SetValue(retVal.BoundArguments, value);
                }

                // /////////////////////
                // Values

                Binding.BoundValuesAttribute valuesAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundValuesAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if(null != valuesAttribute)
                {
                    bool is_string_array = false;

                    Type typeOfCollectionOfStrings = typeof(ICollection<string>);

                    if(fi.FieldType.FullName == "System.String[]")
                    {
                        is_string_array = true;
                    }
                    else
                    if(!typeOfCollectionOfStrings.IsAssignableFrom(fi.FieldType))
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of types that are assignable from ICollection<string> (System.Collections.Generic<System.String>)", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));

                        continue;
                    }

                    // check that we can satisfy the minimum required

                    if(0 != valuesAttribute.Minimum)
                    {
                        int required = valuesAttribute.Base + valuesAttribute.Minimum;

                        if(required > args.Values.Count)
                        {
                            if(0 == (ArgumentBindingOptions.IgnoreMissingValues & effectiveParseOptions))
                            {
                                throw new Exceptions.MissingValueException(required);
                            }
                        }
                    }

                    // now deal with the fact that it might be null

                    object              collection_ =   fi.GetValue(retVal.BoundArguments);

                    ICollection<string> collection  =   Util.ReflectionUtil.CastTo<ICollection<string>>(collection_);

                    if(is_string_array)
                    {
                        if(null != collection)
                        {
                            Trace.Write(String.Format("The field '{0}' of type '{2}' (that is marked with the attribute '{1}') already has a value, which will be replaced", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));
                        }

                        collection = new List<string>(valuesAttribute.Minimum);
                    }
                    else
                    if(null == collection)
                    {
                        if(fi.FieldType.IsAbstract)
                        {
                            collection = new List<string>(valuesAttribute.Minimum);
                        }
                        else
                        {
                            ConstructorInfo ciDefault = fi.FieldType.GetConstructor(new Type[] {});

                            if(null == ciDefault)
                            {
                                Trace.Write(String.Format("The field '{0}' of type '{2}' (that is marked with the attribute '{1}') does not have an accessible default constructor, and so field binding is skipped", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));

                                continue;
                            }

                            object  instance = ciDefault.Invoke(new object[] {});

                            collection = Util.ReflectionUtil.CastTo<ICollection<string>>(instance);
                        }
                    }

                    for(int i = 0; i != valuesAttribute.Maximum; ++i)
                    {
                        int index = valuesAttribute.Base + i;

                        if(index >= args.Values.Count)
                        {
                            break;
                        }

                        collection.Add(args.Values[index].Value);

                        usedValues[index] = true;
                    }

                    if(is_string_array)
                    {
                        string[] ar = new string[collection.Count];

                        collection.CopyTo(ar, 0);

                        fi.SetValue(retVal.BoundArguments, ar);
                    }
                    else
                    if(null == collection_)
                    {
                        fi.SetValue(retVal.BoundArguments, collection);
                    }
                }
            }

            // /////////////////////
            // type & options

            if(0 == (ArgumentBindingOptions.IgnoreOtherFlags & effectiveParseOptions))
            {
                Util.ArgumentUtil.VerifyAllFlagsUsed(args, @"unrecognised flag");
            }

            if(0 == (ArgumentBindingOptions.IgnoreOtherOptions & effectiveParseOptions))
            {
                Util.ArgumentUtil.VerifyAllOptionsUsed(args, @"unrecognised option");
            }

            if(0 == (ArgumentBindingOptions.IgnoreExtraValues & effectiveParseOptions))
            {
                for(int i = 0; i != usedValues.Length; ++i)
                {
                    if(!usedValues[i])
                    {
                        throw new Exceptions.UnusedArgumentException(args.Values[i], @"too many values specified");
                    }
                }
            }

            retVal.Succeeded    =   true;

            return retVal;
        }

        private static BoundFieldType ObtainBoundFieldType(Type type)
        {
            if(type == typeof(int))
            {
                return BoundFieldType.SignedInt32;
            }

            if(type == typeof(long))
            {
                return BoundFieldType.SignedInt64;
            }

            if(type == typeof(uint))
            {
                return BoundFieldType.UnsignedInt32;
            }

            if(type == typeof(ulong))
            {
                return BoundFieldType.UnsignedInt64;
            }

            if(type == typeof(bool))
            {
                return BoundFieldType.Boolean;
            }

            if(type == typeof(string))
            {
                return BoundFieldType.String;
            }

            return BoundFieldType.Unknown;
        }
        #endregion
    }
}
