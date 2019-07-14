
// Created: 17th July 2009
// Updated: 13th July 2019

#pragma warning disable 1607

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("CLASP.Core")]
[assembly: AssemblyDescription("CLASP.NET core API")]

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.24.156.0")]
[assembly: AssemblyFileVersion("0.24.156.0")]

#if DEBUG

[assembly: InternalsVisibleTo("Test.Unit.OrderedDictionary")]
#endif

/* ///////////////////////////// end of file //////////////////////////// */

