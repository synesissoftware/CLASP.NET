
// Created: 17th July 2009
// Updated: 15th July 2019

namespace Clasp
{
    using global::Clasp.Internal;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///  Class that provides <b>ExecuteAroundMethod</b> functionality for
    ///  implementing CLI entry points
    /// </summary>
    public static class Invoker
    {
        #region types

        private class RetVal<T> where T : new()
        {
            #region fields

            public bool                     Succeeded;
            public int                      ExitCode;
            private readonly object         m_boundArguments;
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

            /// <summary>
            ///  (Copy of) the bound arguments
            /// </summary>
            public T BoundArguments
            {
                get
                {
                    return Util.ReflectionUtil.CastTo<T>(m_boundArguments);
                }
            }

            /// <summary>
            ///  This is required for internal assignment (via
            ///  <see cref="System.Reflection.FieldInfo.SetValue(object, object)"/>) to avoid
            ///  boxing-related failure (to assign bound results to the final
            ///  returned element).
            /// </summary>
            internal object BindingArgument
            {
                get
                {
                    return m_boundArguments;
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
            public const FailureOptions             FailureOptions_Default  =   FailureOptions.Default;

            /// <summary>
            ///  The default binding options
            /// </summary>
            public const ArgumentBindingOptions     BindingOptions_Default  =   ArgumentBindingOptions.Default;

            /// <summary>
            ///  The default parse options
            /// </summary>
            public const ParseOptions               ParseOptions_Default    =   ParseOptions.Default;

            /// <summary>
            ///  Exit-code indicating success
            /// </summary>
            public const int                        ExitCode_Success        =   0;
            /// <summary>
            ///  Exit-code indicating failure
            /// </summary>
            public const int                        ExitCode_Failure        =   1;


            /// <summary>
            ///  [INTERNAL]
            /// </summary>
            internal const string                   StandardUsagePrompt     =   @"; use --help for usage";
        }
        #endregion

        #region fields

        private static readonly IDictionary<Binding.BoundNumberConstraints, string> sm_bnc_names;
        private static readonly Specification[]                                     sm_noSpecifications;
        #endregion

        #region construction

        static Invoker()
        {
            sm_bnc_names = new Dictionary<Binding.BoundNumberConstraints, string>();

            sm_bnc_names[Binding.BoundNumberConstraints.MustBeNegative]     =   "must be negative";
            sm_bnc_names[Binding.BoundNumberConstraints.MustBePositive]     =   "must be positive";
            sm_bnc_names[Binding.BoundNumberConstraints.MustBeNonNegative]  =   "must not be negative";
            sm_bnc_names[Binding.BoundNumberConstraints.MustBeNonPositive]  =   "must not be positive";

            sm_noSpecifications = new Specification[0];
        }
        #endregion

        #region properties

        internal static Specification[] NoSpecifications
        {
            get
            {
                return sm_noSpecifications;
            }
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

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/> combined with the help-related
        ///  attributes (if any) of the bound type's fields,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type
        /// </typeparam>
        /// <param name="argv">
        ///  The program arguments
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of
        ///  the arguments, which will be supplemented by the help-related
        ///  attributes (if any) of the bound type's fields
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int ParseAndInvokeMain<T>(string[] argv, Specification[] specifications, ToolMain toolMain)
        {
            ParseOptions effectiveParseOptions = DeriveEffectiveParseOptions_<T>(null);

            return ParseAndInvokeMain<T>(argv, specifications, toolMain, ParseOptions.None, Constants.FailureOptions_Default);
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

            Arguments arguments = new Arguments(argv, specifications, parseOptions, failureOptions);

            return Do_ParseAndInvokeMain_IA_(toolMain, arguments, failureOptions);
        }

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/> combined with the help-related
        ///  attributes (if any) of the bound type's fields,
        ///  and then invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>.
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type
        /// </typeparam>
        /// <param name="argv">
        ///  The program arguments, as obtained from <c>Main()</c>
        /// </param>
        /// <param name="specifications">
        ///  Zero or more specifications that control the interpretation of
        ///  the arguments, which will be supplemented by the help-related
        ///  attributes (if any) of the bound type's fields
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
        public static int ParseAndInvokeMain<T>(string[] argv, Specification[] specifications, ToolMain toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Specification[] mergedSpecs = Invoker.MergeSpecificationsForBoundType<T>(specifications);

            Arguments arguments = new Arguments(argv, mergedSpecs, parseOptions, failureOptions);

            return Do_ParseAndInvokeMain_IA_(toolMain, arguments, failureOptions);
        }

        #region obsolete operations

        /// <summary>
        ///  [DEPRECATED]
        /// </summary>
        [Obsolete("Use ParseAndInvokeMain instead")]
        public static int InvokeMain(string[] argv, Specification[] specifications, ToolMain toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions, failureOptions);

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

            Arguments arguments = new Arguments(argv, specifications, parseOptions, failureOptions);

            Do_ParseAndInvokeMain_VA_(toolMain, arguments, failureOptions);
        }

        #region obsolete operations

        /// Obsolete
        [Obsolete("Use ParseAndInvokeMainVA instead")]
        public static void InvokeMainVA(string[] argv, Specification[] specifications, ToolMainVA toolMain, ParseOptions parseOptions, FailureOptions failureOptions)
        {
            Debug.Assert(null != argv);
            Debug.Assert(null != toolMain);

            Arguments arguments = new Arguments(argv, specifications, parseOptions, failureOptions);

            Do_ParseAndInvokeMain_VA_(toolMain, arguments, failureOptions);
        }
        #endregion

        /// <summary>
        ///  Parses the given program arguments, according to the given
        ///  <paramref name="specifications"/>
        ///  into an instance of the bound argument type
        ///  <typeparamref name="T"/>,
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
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int ParseAndInvokeMainWithBoundArgumentOfType<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(argv, specifications, toolMain, null, null, Constants.FailureOptions_Default);
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
            return InvokeMainAndParseBoundArgumentOfType_<T>(argv, specifications, toolMain, bindingOptions, null, Constants.FailureOptions_Default);
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
        ///  Invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>
        ///  in respect of the previously-parsed
        ///  <paramref name="args"/>
        ///  as obtained from
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain, ParseOptions, FailureOptions)"/>
        ///  (or
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain)"/>)
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type.
        /// </typeparam>
        /// <param name="args">
        ///  The Clasp arguments obtained from
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain, ParseOptions, FailureOptions)"/>
        ///  (or
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain)"/>)
        /// </param>
        /// <param name="toolMain">
        ///  The entry point to the main program logic.
        /// </param>
        /// <returns>
        ///  The return value from <c>toolMain</c>.
        /// </returns>
        public static int InvokeMainWithBoundArgumentOfType<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(args, toolMain, null);
        }

        /// <summary>
        ///  Invokes the program main entry point specified by
        ///  <paramref name="toolMain"/>
        ///  in respect of the previously-parsed
        ///  <paramref name="args"/>
        ///  as obtained from
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain, ParseOptions, FailureOptions)"/>
        ///  (or
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain)"/>),
        ///  according to the
        ///  <paramref name="bindingOptions"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The bound type.
        /// </typeparam>
        /// <param name="args">
        ///  The Clasp arguments obtained from
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain, ParseOptions, FailureOptions)"/>
        ///  (or
        ///  <see cref="Clasp.Invoker.ParseAndInvokeMain(string[], Specification[], ToolMain)"/>)
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
        public static int InvokeMainWithBoundArgumentOfType<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions bindingOptions) where T : new()
        {
            return InvokeMainAndParseBoundArgumentOfType_<T>(args, toolMain, bindingOptions);
        }

        /// <summary>
        ///  Allows for wrapping an operation that may fail, according to
        ///  the given <paramref name="failureOptions"/>
        /// </summary>
        /// <param name="arguments">
        ///  Arguments parameter to be passed to as argument to
        ///  <paramref name="f"/>
        /// </param>
        /// <param name="failureOptions">
        ///  Flags that control the handling of any exceptions
        /// </param>
        /// <param name="f">
        ///  The function to be implemented
        /// </param>
        /// <returns>
        ///  <see cref="Constants.ExitCode_Failure"/> otherwise if any
        ///  exceptions are thrown; the return value from
        ///  <paramref name="f"/> otherwise
        /// </returns>
        public static int ExecuteAroundArguments(Arguments arguments, FailureOptions failureOptions, Func<Arguments, int> f)
        {
            return do_invoke_(arguments, failureOptions, f);
        }

        #region internal facilities

        private static IList<Tuple<Specification, string>> SpecificationSectionPairsForBoundType<T>(IEnumerable<Specification> specifications)
        {
            List<Tuple<Specification, string>> results = new List<Tuple<Specification,string>>();

            Type                type        =   typeof(T);
            FieldInfo[]         fields      =   Util.ReflectionUtil.GetTypeFields(type);

            foreach(FieldInfo fi in fields)
            {
                Binding.BoundEnumeratorAttribute[] enumeratorAttributes = Util.ReflectionUtil.GetAttributes<Binding.BoundEnumeratorAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeOnly);

                foreach(var ea in enumeratorAttributes)
                {
                    Specification   specification   =   Specification.Flag(ea.Alias, ea.ResolvedName, ea.HelpDescription);
                    string          sectionName     =   ea.HelpSection;

                    if (String.IsNullOrWhiteSpace(sectionName))
                    {
                        sectionName = "";
                    }

                    results.Add(Tuple.Create(specification, sectionName));
                }

                Binding.BoundFlagAttribute flagAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundFlagAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeOnly);

                if (null != flagAttribute)
                {
                    Specification   specification   =   Specification.Option(flagAttribute.Alias, flagAttribute.ResolvedName, flagAttribute.HelpDescription);
                    string          sectionName     =   flagAttribute.HelpSection;

                    if (!String.IsNullOrWhiteSpace(sectionName))
                    {
                        results.Add(Tuple.Create(specification, sectionName));
                    }
                }

                Binding.BoundOptionAttribute optionAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundOptionAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeOnly);

                if (null != optionAttribute)
                {
                    Specification   specification   =   Specification.Option(optionAttribute.Alias, optionAttribute.ResolvedName, optionAttribute.HelpDescription);
                    string          sectionName     =   optionAttribute.HelpSection;

                    if (!String.IsNullOrWhiteSpace(sectionName))
                    {
                        results.Add(Tuple.Create(specification, sectionName));
                    }
                }
            }

            return results;
        }

        private static OrderedDict<string, IList<Specification>> StructureFromSpecifications(IEnumerable<Specification> specs)
        {
            var                 structure       =   new OrderedDict<string, IList<Specification>>();

            string              sectionName     =   null;

            foreach(Specification spec in specs)
            {
                if (spec.IsSection)
                {
                    if (!structure.Contains(spec.Description))
                    {
                        structure.Add(spec.Description, new List<Specification>());
                    }

                    sectionName = spec.Description;
                }
                else
                {
                    if (!structure.Contains(sectionName))
                    {
                        structure.Add(sectionName, new List<Specification>());
                    }

                    structure[sectionName].Add(spec);
                }
            }

            return structure;
        }

        private static Tuple<IList<Specification>, int> LookupSpecificationInStructure(OrderedDict<string, IList<Specification>> structure, Specification spec)
        {
            foreach(var pair in structure)
            {
                var k = pair.Key;
                var v = pair.Value;

                for (int i = 0; v.Count != i; ++i)
                {
                    Specification sp = v[i];

                    if (spec.ResolvedName == sp.ResolvedName)
                    {
                        return Tuple.Create(v, i);
                    }
                }
            }

            return null;
        }

        internal static Specification[] MergeSpecificationsForBoundType<T>(IEnumerable<Specification> specifications)
        {
            if (null == specifications)
            {
                specifications = (Specification[])NoSpecifications.Clone();
            }

            if (specifications.Any((spec) => spec.IsSection))
            {
                var                     structure   =   StructureFromSpecifications(specifications);
                var                     pairs       =   SpecificationSectionPairsForBoundType<T>(specifications);
                IList<Specification>    firstSpecs  =   new List<Specification>();

                foreach(var pair in pairs)
                {
                    Specification                       tspec   =   pair.Item1;
                    Tuple<IList<Specification>, int>    tuple   =   LookupSpecificationInStructure(structure, tspec);

                    // If the type's specification already exists anyway within the given specifications ...
                    if (null != tuple)
                    {
                        // ... then see whether should update
                        bool            changed     =   false;
                        Specification   sspec       =   tuple.Item1[tuple.Item2];
                        string          alias       =   sspec.GivenName;
                        string          desc        =   sspec.Description;
                        string          resolved    =   sspec.ResolvedName;

                        if (String.IsNullOrWhiteSpace(alias) && !String.IsNullOrWhiteSpace(tspec.GivenName))
                        {
                            alias   =   tspec.GivenName;
                            changed =   true;
                        }

                        if (String.IsNullOrWhiteSpace(desc) && !String.IsNullOrWhiteSpace(tspec.Description))
                        {
                            desc    =   tspec.Description;
                            changed =   true;
                        }

                        if (changed)
                        {
                            switch(sspec.Type)
                            {
                            case ArgumentType.Flag:

                                tuple.Item1[tuple.Item2] = Specification.Flag(alias, resolved, desc);
                                break;
                            case ArgumentType.Option:

                                OptionSpecification sspec_o = (OptionSpecification)sspec;

                                tuple.Item1[tuple.Item2] = Specification.Option(alias, resolved, desc, sspec_o.ValidValues);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // The type's specification does not exist in the given specifications

                        string                  sectionName     =   pair.Item2;
                        IList<Specification>    sectionSpecs;

                        if (!structure.TryGetValue(sectionName, out sectionSpecs))
                        {
                            structure.Add(sectionName, new List<Specification>());
                        }

                        structure[sectionName].Add(tspec);
                    }
                }

                // Now rebuild, taking the blanks first

                List<Specification> namedSpecs  =   new List<Specification>();
                List<Specification> emptySpecs  =   new List<Specification>();

                foreach(var pair0 in structure)
                {
                    string                  sectionName     =   pair0.Key;
                    IList<Specification>    sectionSpecs    =   pair0.Value;

                    IList<Specification>    targetSpecs;

                    if (String.IsNullOrWhiteSpace(sectionName))
                    {
                        targetSpecs = emptySpecs;
                    }
                    else
                    {
                        targetSpecs = namedSpecs;

                        targetSpecs.Add(Specification.Section(sectionName));
                    }

                    foreach(Specification spec in sectionSpecs)
                    {
                        targetSpecs.Add(spec);
                    }
                }

                List<Specification> results = new List<Specification>(emptySpecs.Count + namedSpecs.Count);

                results.AddRange(emptySpecs);
                results.AddRange(namedSpecs);

                return results.ToArray();
            }
            else
            {
                List<Specification> currSpecs   =   new List<Specification>(specifications);
                List<Specification> newSpecs    =   new List<Specification>();
                List<Specification> lastSpecs   =   new List<Specification>(2);

                var                 pairs       =   SpecificationSectionPairsForBoundType<T>(specifications);

                foreach(var pair in pairs)
                {
                    newSpecs.Add(pair.Item1);
                }

                int? lastHelpIndex      =   null;
                int? lastVersionIndex   =   null;
                int? lastSectionIndex   =   null;

                for (int i = 0; currSpecs.Count != i; ++i)
                {
                    var cs = currSpecs[i];

                    switch(cs.ResolvedName)
                    {
                    case "--help":

                        lastHelpIndex = i;
                        break;
                    case "--version":

                        lastVersionIndex = i;
                        break;
                    default:

                        if (cs.IsSection)
                        {
                            lastSectionIndex = i;
                        }
                        break;
                    }
                }

                if ((lastHelpIndex.HasValue && lastHelpIndex >= currSpecs.Count - 2) && (lastVersionIndex.HasValue && lastVersionIndex >= currSpecs.Count - 2))
                {
                    // If "--help" and "--version" were the last two ...

                    lastSpecs.Insert(0, currSpecs[currSpecs.Count - 1]);
                    currSpecs.RemoveAt(currSpecs.Count - 1);

                    lastSpecs.Insert(0, currSpecs[currSpecs.Count - 1]);
                    currSpecs.RemoveAt(currSpecs.Count - 1);

                    // ... and the preceding item was a section ...

                    if (lastSectionIndex.HasValue && lastSectionIndex == currSpecs.Count - 1 && (lastSectionIndex < lastHelpIndex || lastSectionIndex < lastVersionIndex))
                    {
                        // ... then move it to the start of the last-specs

                        lastSpecs.Insert(0, currSpecs[currSpecs.Count - 1]);
                        currSpecs.RemoveAt(currSpecs.Count - 1);
                    }
                }

                foreach(var ns in newSpecs)
                {
                    int ix = currSpecs.FindIndex((cs) => cs.ResolvedName == ns.ResolvedName);

                    if (ix >= 0)
                    {
                        var cs = currSpecs[ix];

                        Specification s = null;

                        if (!String.IsNullOrWhiteSpace(ns.Description))
                        {
                            switch(cs.Type)
                            {
                            case ArgumentType.Flag:

                                s = Specification.Flag(cs.GivenName, cs.ResolvedName, ns.Description);
                                break;
                            case ArgumentType.Option:

                                OptionSpecification cs_o = (OptionSpecification)cs;

                                s = Specification.Option(cs.GivenName, cs.ResolvedName, ns.Description, cs_o.ValidValues);
                                break;
                            }
                        }

                        if (null != s)
                        {
                            currSpecs[ix] = s;
                        }
                    }
                    else
                    {
                        currSpecs.Add(ns);
                    }
                }

                currSpecs.AddRange(lastSpecs);

                return currSpecs.ToArray();
            }
        }
        #endregion
        #endregion

        #region implementation

        private static ArgumentBindingOptions DeriveEffectiveBindingOptions_<T>(ArgumentBindingOptions? bindingOptions)
        {
            Type                        type                    =   typeof(T);
            Binding.BoundTypeAttribute  typeAttribute           =   Util.ReflectionUtil.GetFirstAttributeOrNull<Binding.BoundTypeAttribute>(type, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

            ArgumentBindingOptions       effectiveBindingOptions   =   bindingOptions ?? Constants.BindingOptions_Default;

            if (null != typeAttribute)
            {
                if (typeAttribute.AttributeOptionsHavePrecedence || !bindingOptions.HasValue)
                {
                    if (typeAttribute.GivenBindingOptions.HasValue)
                    {
                        effectiveBindingOptions = typeAttribute.GivenBindingOptions.Value;
                    }
                }
            }

            return effectiveBindingOptions;
        }

        private static ParseOptions DeriveEffectiveParseOptions_<T>(ParseOptions? parsingOptions)
        {
            Type                        type                    =   typeof(T);
            Binding.BoundTypeAttribute  typeAttribute           =   Util.ReflectionUtil.GetFirstAttributeOrNull<Binding.BoundTypeAttribute>(type, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

            ParseOptions                effectiveParseOptions   =   parsingOptions ?? Constants.ParseOptions_Default;

            if (null != typeAttribute)
            {
                if (typeAttribute.AttributeOptionsHavePrecedence || !parsingOptions.HasValue)
                {
                    if (typeAttribute.GivenParsingOptions.HasValue)
                    {
                        effectiveParseOptions = typeAttribute.GivenParsingOptions.Value;
                    }
                }
            }

            return effectiveParseOptions;
        }

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

            if (0 != (FailureOptions.SetExitCodeForVV & failureOptions))
            {
                Environment.ExitCode = r;
            }

            if (0 != (FailureOptions.InvokeExitForVV & failureOptions))
            {
                Environment.Exit(r);
            }
        }

        private static int InvokeMainAndParseBoundArgumentOfType_<T>(string[] argv, Specification[] specifications, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions? bindingOptions, ParseOptions? parseOptions, FailureOptions failureOptions) where T : new()
        {
            ParseOptions effectiveParseOptions = DeriveEffectiveParseOptions_<T>(parseOptions);

            return Invoker.ParseAndInvokeMain<T>(argv, specifications, (Arguments args2) => {

                RetVal<T> r = ParseBoundArguments_<T>(args2, bindingOptions);

                if (!r.Succeeded)
                {
                    return r.ExitCode;
                }

                return toolMain(r.BoundArguments, args2);
            }
            , effectiveParseOptions
            , failureOptions
            );
        }

        private static int InvokeMainAndParseBoundArgumentOfType_<T>(Arguments args, ToolMainWithBoundArguments<T> toolMain, ArgumentBindingOptions? bindingOptions) where T : new()
        {
            RetVal<T> r = ParseBoundArguments_<T>(args, bindingOptions);

            if (!r.Succeeded)
            {
                return r.ExitCode;
            }

            return toolMain(r.BoundArguments, args);
        }

        private static int do_invoke_(Arguments arguments, FailureOptions failureOptions, Func<Arguments, int> f)
        {
            if (FailureOptions.None == failureOptions)
            {
                return f(arguments);
            }
            else
            {
                try
                {
                    return f(arguments);
                }
                catch (OutOfMemoryException /* x */)
                {
                    if (0 != (FailureOptions.HandleMemoryExceptions & failureOptions))
                    {
                        Console.Error.WriteLine("{0}: out of memory", Arguments.ProgramName);

                        return Constants.ExitCode_Success;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exceptions.UnusedArgumentException x)
                {
                    if (0 == (FailureOptions.HandleClaspExceptions & failureOptions))
                    {
                        throw;
                    }

                    Trace.WriteLine(string.Format("exception ({0}): {1}", x.GetType().FullName, x.Message));

                    Console.Error.WriteLine("{0}: {1}", Arguments.ProgramName, x.Message);
                }
                catch (Exceptions.ClaspException x)
                {
                    if (0 == (FailureOptions.HandleClaspExceptions & failureOptions))
                    {
                        throw;
                    }

                    Trace.WriteLine(string.Format("exception ({0}): {1}", x.GetType().FullName, x.Message));

                    Console.Error.WriteLine("{0}: {1}{2}", Arguments.ProgramName, x.Message, 0 != (FailureOptions.AppendStandardUsagePromptToContingentReport & failureOptions) ? Constants.StandardUsagePrompt : String.Empty);
                }
                catch (Exception x)
                {
                    Type x_type = x.GetType();

                    switch(x_type.FullName)
                    {
                    case "NUnit.Framework.AssertionException":
                    case "Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException":

                        throw;
                    }

                    if (0 == (FailureOptions.HandleSystemExceptions & failureOptions))
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

            ArgumentBindingOptions  effectiveBindingOptions =   DeriveEffectiveBindingOptions_<T>(bindingOptions);

            // ///////////////////////////////////
            // iterate the fields

            foreach(var fi in fields)
            {
                // since we can't prevent BoundFlagAttribute and
                // BoundOptionAttribute being applied to the same field, we
                // search for them in turn

                // /////////////////////
                // Enum

                Binding.BoundEnumerationAttribute  enumerationAttribute   =   Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundEnumerationAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if (null != enumerationAttribute)
                {
                    // field must be same as enumeration type

                    if (enumerationAttribute.Type != fi.FieldType)
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{1}' is marked with the attribute '{1}' of incompatible bound type '{3}' : binding is not performed for this field", fi.Name, fi.FieldType, typeof(Binding.BoundEnumerationAttribute), enumerationAttribute.Type));
                        Debug.Assert(enumerationAttribute.Type != fi.FieldType);

                        continue;
                    }

                    // Now find all the enumerator attributes
                    Binding.BoundEnumeratorAttribute[] enumeratorAttributes = Util.ReflectionUtil.GetAttributes<Binding.BoundEnumeratorAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeOnly);

                    Debug.Assert(null != enumeratorAttributes);

                    if (0 == enumeratorAttributes.Length)
                    {
                        // TODO: warn
                    }
                    else
                    {
                        int value = 0;

                        foreach(Binding.BoundEnumeratorAttribute attr in enumeratorAttributes)
                        {
                            string  flagResolvedName    =   attr.ResolvedName;
                            int     enumValue           =   attr.EnumeratorValue;

                            if (args.HasFlag(flagResolvedName))
                            {
                                value |= enumValue;
                            }
                        }

                        fi.SetValue(retVal.BindingArgument, Enum.ToObject(enumerationAttribute.Type, value));
                    }

                    continue;
                }

                // /////////////////////
                // FlagSpecification

                Binding.BoundFlagAttribute  flagAttribute   =   Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundFlagAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if (null != flagAttribute)
                {
                    // field must be bool

                    if (typeof(bool) != fi.FieldType)
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of type bool (System.Boolean)", fi.Name, typeof(Binding.BoundFlagAttribute), fi.FieldType));
                        Debug.Assert(typeof(bool) == fi.FieldType);

                        continue;
                    }

                    bool flagSpecified = Util.ArgumentUtil.FlagSpecified(args, flagAttribute.ResolvedName);

                    fi.SetValue(retVal.BindingArgument, flagSpecified);

                    continue;
                }

                // /////////////////////
                // OptionSpecification

                Binding.BoundOptionAttribute    optionAttribute    =   Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundOptionAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if (null != optionAttribute)
                {
                    Binding.BoundNumberConstraints  bnc     =   optionAttribute.EffectiveBoundNumberConstraints;
                    Binding.NumberTruncate          nt      =   optionAttribute.NumberTruncate;

                    Interfaces.IArgument            option  =   Util.ArgumentUtil.FindOption(args, optionAttribute.ResolvedName);
                    object                          value   =   null;

                    if (null == option)
                    {
                        if (null == optionAttribute.DefaultValue)
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
                        if (String.IsNullOrEmpty(option.Value))
                        {
                            if (optionAttribute.AllowEmpty)
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
                        value = ParseInt32_(option, value.ToString(), bnc, nt);
                        break;
                    case BoundFieldType.SignedInt64:
                        value = ParseInt64_(option, value.ToString(), bnc, nt);
                        break;
                    case BoundFieldType.UnsignedInt32:
                        value = ParseUInt32_(option, value.ToString(), bnc, nt);
                        break;
                    case BoundFieldType.UnsignedInt64:
                        value = ParseUInt64_(option, value.ToString(), bnc, nt);
                        break;
                    case BoundFieldType.Single:
                        value = ParseSingle_(option, value.ToString(), bnc, nt);
                        break;
                    case BoundFieldType.Double:
                        value = ParseDouble_(option, value.ToString(), bnc, nt);
                        break;
                    default:

                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' but its type is not supported by the current version of the library", fi.Name, typeof(Binding.BoundOptionAttribute), fi.FieldType));

                        continue;
                    }

                    fi.SetValue(retVal.BindingArgument, value);

                    continue;
                }

                // /////////////////////
                // Value

                Binding.BoundValueAttribute valueAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundValueAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if (null != valueAttribute)
                {
                    if (typeof(string) != fi.FieldType)
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of type string (System.String)", fi.Name, typeof(Binding.BoundValueAttribute), fi.FieldType));

                        continue;
                    }

                    object value;

                    if (valueAttribute.ValueIndex < args.Values.Count)
                    {
                        value = args.Values[valueAttribute.ValueIndex].Value;

                        usedValues[valueAttribute.ValueIndex] = true;
                    }
                    else
                    {
                        if (null == valueAttribute.DefaultValue)
                        {
                            if (0 != (ArgumentBindingOptions.IgnoreMissingValues & effectiveBindingOptions))
                            {
                                continue;
                            }

                            if (String.IsNullOrWhiteSpace(valueAttribute.UsageLabel))
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

                    fi.SetValue(retVal.BindingArgument, value);
                }

                // /////////////////////
                // Values

                Binding.BoundValuesAttribute valuesAttribute = Util.ReflectionUtil.GetOnlyAttributeOrNull<Binding.BoundValuesAttribute>(fi, Util.ReflectionLookup.FromQueriedTypeAndAncestors);

                if (null != valuesAttribute)
                {
                    bool is_string_array = false;

                    Type typeOfCollectionOfStrings = typeof(ICollection<string>);

                    if (fi.FieldType.FullName == "System.String[]")
                    {
                        is_string_array = true;
                    }
                    else
                    if (!typeOfCollectionOfStrings.IsAssignableFrom(fi.FieldType))
                    {
                        Trace.Write(String.Format("The field '{0}' of type '{2}' is marked with the attribute '{1}' which may only be applied to fields and properties of types that are assignable from ICollection<string> (System.Collections.Generic<System.String>)", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));

                        continue;
                    }

                    // check that we can satisfy the minimum required

                    if (0 != valuesAttribute.Minimum)
                    {
                        int required = valuesAttribute.Base + valuesAttribute.Minimum;

                        if (required > args.Values.Count)
                        {
                            if (0 == (ArgumentBindingOptions.IgnoreMissingValues & effectiveBindingOptions))
                            {
                                throw new Exceptions.MissingValueException(required);
                            }
                        }
                    }

                    // now deal with the fact that it might be null

                    object              collection_ =   fi.GetValue(retVal.BindingArgument);

                    ICollection<string> collection  =   Util.ReflectionUtil.CastTo<ICollection<string>>(collection_);

                    if (is_string_array)
                    {
                        if (null != collection)
                        {
                            Trace.Write(String.Format("The field '{0}' of type '{2}' (that is marked with the attribute '{1}') already has a value, which will be replaced", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));
                        }

                        collection = new List<string>(valuesAttribute.Minimum);
                    }
                    else
                    if (null == collection)
                    {
                        if (fi.FieldType.IsAbstract)
                        {
                            collection = new List<string>(valuesAttribute.Minimum);
                        }
                        else
                        {
                            ConstructorInfo ciDefault = fi.FieldType.GetConstructor(new Type[] {});

                            if (null == ciDefault)
                            {
                                Trace.Write(String.Format("The field '{0}' of type '{2}' (that is marked with the attribute '{1}') does not have an accessible default constructor, and so field binding is skipped", fi.Name, typeof(Binding.BoundValuesAttribute), fi.FieldType));

                                continue;
                            }

                            object  instance = ciDefault.Invoke(new object[] {});

                            collection = Util.ReflectionUtil.CastTo<ICollection<string>>(instance);
                        }
                    }

                    for (int i = 0; i != valuesAttribute.Maximum; ++i)
                    {
                        int index = valuesAttribute.Base + i;

                        if (index >= args.Values.Count)
                        {
                            break;
                        }

                        collection.Add(args.Values[index].Value);

                        usedValues[index] = true;
                    }

                    if (is_string_array)
                    {
                        string[] ar = new string[collection.Count];

                        collection.CopyTo(ar, 0);

                        fi.SetValue(retVal.BindingArgument, ar);
                    }
                    else
                    if (null == collection_)
                    {
                        fi.SetValue(retVal.BindingArgument, collection);
                    }
                }
            }

            // /////////////////////
            // type & options

            if (0 == (ArgumentBindingOptions.IgnoreOtherFlags & effectiveBindingOptions))
            {
                Util.ArgumentUtil.VerifyAllFlagsUsed(args);
            }

            if (0 == (ArgumentBindingOptions.IgnoreOtherOptions & effectiveBindingOptions))
            {
                Util.ArgumentUtil.VerifyAllOptionsUsed(args);
            }

            if (0 == (ArgumentBindingOptions.IgnoreExtraValues & effectiveBindingOptions))
            {
                for (int i = 0; i != usedValues.Length; ++i)
                {
                    if (!usedValues[i])
                    {
                        throw new Exceptions.UnusedValueException(args.Values[i], @"too many values specified");
                    }
                }
            }

            retVal.Succeeded    =   true;

            return retVal;
        }

        private static BoundFieldType ObtainBoundFieldType(Type type)
        {
            if (type == typeof(int))
            {
                return BoundFieldType.SignedInt32;
            }

            if (type == typeof(long))
            {
                return BoundFieldType.SignedInt64;
            }

            if (type == typeof(uint))
            {
                return BoundFieldType.UnsignedInt32;
            }

            if (type == typeof(ulong))
            {
                return BoundFieldType.UnsignedInt64;
            }

            if (type == typeof(bool))
            {
                return BoundFieldType.Boolean;
            }

            if (type == typeof(string))
            {
                return BoundFieldType.String;
            }

            if (type == typeof(float))
            {
                return BoundFieldType.Single;
            }

            if (type == typeof(double))
            {
                return BoundFieldType.Double;
            }

            return BoundFieldType.Unknown;
        }

        private delegate object ParseFunc(string s);

        private delegate T CastFunc<T>(object o);

        private static Int32 ParseInt32_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseInteger_T_(option, value, typeof(Int32), bnc, nt, (s) => Int32.Parse(s), (d) => Convert.ToInt32(d));
        }

        private static UInt32 ParseUInt32_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseInteger_T_(option, value, typeof(UInt32), bnc, nt, (s) => UInt32.Parse(s), (n) => Convert.ToUInt32(n));
        }

        private static Int64 ParseInt64_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseInteger_T_(option, value, typeof(Int64), bnc, nt, (s) => Int64.Parse(s), (n) => Convert.ToInt64(n));
        }

        private static UInt64 ParseUInt64_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseInteger_T_(option, value, typeof(UInt64), bnc, nt, (s) => UInt64.Parse(s), (n) => Convert.ToUInt64(n));
        }

        private static Single ParseSingle_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseReal_T_(option, value, typeof(Single), bnc, nt, (s) => Single.Parse(s), (n) => Convert.ToSingle(n));
        }

        private static Double ParseDouble_(Interfaces.IArgument option, string value, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt)
        {
            return ParseReal_T_(option, value, typeof(Double), bnc, nt, (s) => Double.Parse(s), (n) => Convert.ToDouble(n));
        }

        private static bool IsNegative_(object o)
        {
            Debug.Assert(null != o);

            Type type = o.GetType();

            if (false)
            {
                ;
            }
            else if (typeof(Int32) == type)
            {
                return (Int32)o < 0;
            }
            else if (typeof(Int64) == type)
            {
                return (Int64)o < 0;
            }
            else if (typeof(Single) == type)
            {
                return (Single)o < 0;
            }
            else if (typeof(Double) == type)
            {
                return (Double)o < 0;
            }

            return false;
        }

        private static bool IsPositive_(object o)
        {
            Debug.Assert(null != o);

            Type type = o.GetType();

            if (false)
            {
                ;
            }
            else if (typeof(Int32) == type)
            {
                return (Int32)o > 0;
            }
            else if (typeof(UInt32) == type)
            {
                return (UInt32)o > 0;
            }
            else if (typeof(Int64) == type)
            {
                return (Int64)o > 0;
            }
            else if (typeof(UInt64) == type)
            {
                return (UInt64)o > 0;
            }
            else if (typeof(Single) == type)
            {
                return (Single)o > 0;
            }
            else if (typeof(Double) == type)
            {
                return (Double)o > 0;
            }

            return false;
        }

        private static bool IsNonNegative_(object o)
        {
            Debug.Assert(null != o);

            Type type = o.GetType();

            if (false)
            {
                ;
            }
            else if (typeof(Int32) == type)
            {
                return (Int32)o >= 0;
            }
            else if (typeof(Int64) == type)
            {
                return (Int64)o >= 0;
            }
            else if (typeof(Single) == type)
            {
                return (Single)o >= 0;
            }
            else if (typeof(Double) == type)
            {
                return (Double)o >= 0;
            }

            return true;
        }

        private static bool IsNonPositive_(object o)
        {
            Debug.Assert(null != o);

            Type type = o.GetType();

            if (false)
            {
                ;
            }
            else if (typeof(Int32) == type)
            {
                return (Int32)o < 1;
            }
            else if (typeof(UInt32) == type)
            {
                return (UInt32)o < 1;
            }
            else if (typeof(Int64) == type)
            {
                return (Int64)o < 1;
            }
            else if (typeof(UInt64) == type)
            {
                return (UInt64)o < 1;
            }
            else if (typeof(Single) == type)
            {
                return (Single)o < 1;
            }
            else if (typeof(Double) == type)
            {
                return (Double)o < 1;
            }

            return false;
        }

        private static T ParseInteger_T_<T>(Interfaces.IArgument option, string value, Type type, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt, ParseFunc pf, CastFunc<T> cf)
        {
            if (null == value)
            {
                value = option.Value;
            }

            bnc &= Binding.BoundNumberConstraints.RangeMask;

            try
            {
                object r = null;

                try
                {
                    r = pf(value);
                }
                catch (System.FormatException /* x */)
                {
                    double d;

                    if (!Double.TryParse(value, out d))
                    {
                        throw;
                    }
                    else
                    {
                        switch(nt)
                        {
                        default:

                            Debug.Assert(false, "unexpected");
                            break;
                        case Binding.NumberTruncate.None:

                            throw new Exceptions.InvalidOptionValueException(option, type, null, "whole number required");
                        case Binding.NumberTruncate.ToCeiling:

                            d = Math.Ceiling(d);
                            break;
                        case Binding.NumberTruncate.ToFloor:

                            d = Math.Floor(d);
                            break;
                        case Binding.NumberTruncate.ToNearest:

                            d = Math.Round(d);
                            break;
                        case Binding.NumberTruncate.ToZero:

                            d = Math.Truncate(d);
                            break;
                        }

                        r = d;
                    }
                }

                switch(bnc & Binding.BoundNumberConstraints.RangeMask)
                {
                default:

                    Debug.Assert(false, "unexpected");
                    break;
                case Binding.BoundNumberConstraints.None:

                    break;
                case Binding.BoundNumberConstraints.MustBeNegative:

                    if (!IsNegative_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBePositive:

                    if (!IsPositive_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBeNonNegative:

                    if (!IsNonNegative_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBeNonPositive:

                    if (!IsNonPositive_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                }

                return cf(r);
            }
            catch (System.FormatException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
            catch (System.InvalidCastException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
            catch (System.OverflowException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
        }

        private static T ParseReal_T_<T>(Interfaces.IArgument option, string value, Type type, Binding.BoundNumberConstraints bnc, Binding.NumberTruncate nt, ParseFunc pf, CastFunc<T> cf)
        {
            if (null == value)
            {
                value = option.Value;
            }

            bnc &= Binding.BoundNumberConstraints.RangeMask;

            try
            {
                object r = pf(value);

                switch(bnc & Binding.BoundNumberConstraints.RangeMask)
                {
                default:

                    Debug.Assert(false, "unexpected");
                    break;
                case Binding.BoundNumberConstraints.None:

                    break;
                case Binding.BoundNumberConstraints.MustBeNegative:

                    if (!IsNegative_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBePositive:

                    if (!IsPositive_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBeNonNegative:

                    if (!IsNonNegative_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                case Binding.BoundNumberConstraints.MustBeNonPositive:

                    if (!IsNonPositive_(r))
                    {
                        throw new Exceptions.OptionValueOutOfRangeException(sm_bnc_names[bnc], option, typeof(Int32));
                    }
                    break;
                }

                return cf(r);
            }
            catch (System.FormatException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
            catch (System.InvalidCastException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
            catch (System.OverflowException x)
            {
                throw new Exceptions.InvalidOptionValueException(option, type, x);
            }
        }
        #endregion
    }
}
