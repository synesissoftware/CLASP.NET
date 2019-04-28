#  **CLASP.NET** Changes

## 0.15.131.0 - 27th April 2019

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


