
// Created: 17th July 2009
// Updated: 14th August 2019

#pragma warning disable 1607

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("CLASP.Core")]
[assembly: AssemblyDescription("CLASP.NET core API")]

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.25.161.1")]
[assembly: AssemblyFileVersion("0.25.161.1")]

#if DEBUG

[assembly: InternalsVisibleTo("Test.Unit.OrderedDictionary")]
#endif

/* ///////////////////////////// end of file //////////////////////////// */

