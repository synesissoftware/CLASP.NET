#  **CLASP.NET** Changes

## 0.24.156.0 - 14th July 2019

* ~ refactored the exception hierarchy

## 0.23.154.0 - 9th June 2019

* + added ``UsageUtil.ShowBoundUsageAndQuit()`` method (overloads)
* + added ``Invoker.ExecuteAroundArguments()`` method

## 0.22.152.1 - 8th June 2019

* ~ adjusted response to ``BoundTypeAttribute`` recognising ``AttributeOptionsHavePrecedence`` only for properties specified, rather than all defaults
* + added CLASP.Examples.Common, containing common code for several examples
* + added examples Example.ParseAndBind.cat, Example.ShowBoundUsage

## 0.22.151.0 - 8th June 2019

* + now supports binding of ``struct`` as well as ``class`` types
* + now a bound type's attributes can contain ``Alias``, ``HelpSection``, and ``HelpDescription``, which may be merged with the ``Specification[]`` list for parsing and for showing usage
* + added ``Invoker.ParseAndInvokeMain<T>()`` overloads, which now merges a bound type's attributes into the given specifications before parsing
* + added ``UsageUtil.ShowBoundUsage<>``, which invokes help in respect of a bound structure, and merges a bound type's attributes into the given specifications before displaying usage
* + added ``Specification.Alias()`` method, which defines aliases for flags or options

## 0.21.150.2 - 7th June 2019

* + added SolutionInfo.cs

## 0.21.149.1 - 6th June 2019

* ~ fix to unit-test

## 0.21.148.0 - 18th May 2019

* + added Clasp.Util assembly (formerly SynesisSoftware.SystemTools.Util)

## 0.20.147.1 - 17th May 2019

* + added ``Arguments.RequireValue()`` method (overloads)

## 0.20.146.0 - 17th May 2019

* + added ``Arguments.RequireOption()`` method (overloads)

## 0.19.145.1 - 6th May 2019

* [BREAKING CHANGE] ~ namespace ``SynesisSoftware.SystemTools.Clasp`` => ``Clasp``
* ~ change ``Alias`` to ``Specification``
* ~ rename ``Flag`` to ``FlagSpecification``, ``Option`` to ``OptionSpecification``

## 0.18.141.2 - 3rd May 2019

* ~ fixed missing range enforcement for floating-point fields

## 0.18.140.1 - 3rd May 2019

* + added support for ``Single`` and ``Double`` floating-point fields
* + added ``BoundNumberConstraints`` enumeration
* + added ``NumberTruncate`` enumeration
* + added ``OptionValueOutOfRangeException`` exception class
* ~ changed internal logic of ``BoundOptionAttribute`` to make defaults sensible
* + ``Invoker`` now enforces integer range constraints
* + added tests for above

## 0.18.139.0 - 2nd May 2019

* + added ``BoundEnumerationAttribute`` and ``BoundEnumeratorAttribute`` classes, supporting binding of command-line flags with enumerator values
* + added Example.BoundValues.cat

## 0.17.137.1 - 30th April 2019

* ~ full and proper handling of ArgumentBindingOptions.IgnoreMissingValues

## 0.17.136.0 - 30th April 2019

* + added ``ArgumentBindingOptions.IgnoreMissingValues`` and associated tests

## 0.16.136.2 - 1st May 2019

* ~ full and proper handling of ArgumentBindingOptions.IgnoreMissingValues

## 0.16.135.1 - 28th April 2019

* ~ clearing up some corner cases in default value support

## 0.16.134.0 - 28th April 2019

* + now supports option default values
* + added ``DefaultValue`` attribute for option specification
* + added ``DefaultIndicator`` field for ``UsageUtil.UsageParams`` structure
* ~ enhanced Usage_tester.cs adding cases testing default value behaviour

## 0.15.133.1 - 28th April 2019

* ~ fixed some subtle specification-finding defects

## 0.15.132.0 - 28th April 2019

* ~ substantial improvements to ``ShowUsage()`` (and ``ShowVersion()``)

## 0.14.129.2 - 23rd April 2019

* ~ now works with string[] for values

## 0.14.128.1 - 23rd April 2019

* + bunch more testing to validate bound parsing of flags and options (in preparation for fixing of known defect in bound parsing of values)

## 0.14.127.0 - 20th April 2019

* + added IArgument.Used property and IArgument.Use() method

## 0.13.126.1 - 20th April 2019

* ~ changed (almost) all Argument properties to be backed by private readonly field

## 0.13.125.0 - 20th April 2019

* + added IArgument.Specification property

## 0.12.124.0 - 20th April 2019

* ~ fixed default interpretation of "-" to be Flag, rather than Option
* + added missing response to ParseOptions.TreatSinglehyphenAsValue
* ~ more preparatory refactoring

## 0.11.123.2 - 20th April 2019

* ~ corrected Argument.ToString() for flags, options, values (and unknown)

## 0.11.122.1 - 20th April 2019

* ~ significant preparatory refactoring, changing parameter names and documentation, along with internal fields and methods, in readiness for breaking change to rename *Alias types/attributes/methods to *Specification

## 0.11.121.0 - 20th April 2019

* + ShowUsage() now lists an option's values (if specified)

## 0.10.120.1 - 6th April 2019

* ~ fixed erroneous processing of alias to option-with-value



## previous versions

T.B.C.


