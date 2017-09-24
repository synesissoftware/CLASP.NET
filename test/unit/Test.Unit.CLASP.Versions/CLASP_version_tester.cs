
// Created: 19th June 2017
// Updated: 19th June 2017

namespace Test.Unit.CLASP.Versions
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Diagnostics;
    using System.Reflection;

    [TestClass]
    public class CLASP_version_tester
    {
        static Type forcedLoadReference = typeof(IArgument);

        [TestMethod]
        public void test_Pantheios_Interfaces_version()
        {
            Assembly assembly = forcedLoadReference.Assembly;

            Version version = assembly.GetName().Version;

            Assert.AreEqual(0, version.Major);
            Assert.AreEqual(8, version.Minor);
        }

        [TestMethod]
        public void test_Pantheios_Interfaces_file_version()
        {
            Assembly assembly = forcedLoadReference.Assembly;

            FileVersionInfo fversion = FileVersionInfo.GetVersionInfo(assembly.Location);

            Assert.AreEqual(0, fversion.FileMajorPart);
            Assert.AreEqual(8, fversion.FileMinorPart);
        }
    }
}

