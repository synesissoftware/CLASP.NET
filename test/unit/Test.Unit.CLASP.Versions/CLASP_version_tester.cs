
// Created: 19th June 2017
// Updated: 20th April 2019

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
        internal static class Constants
        {
            internal static class Expectations
            {
                internal const int  VersionMajor    =   0;
                internal const int  VersionMinor    =   14;
            }
        }

        static Type forcedLoadReference = typeof(IArgument);

        [TestMethod]
        public void test_CLASP_Interfaces_version()
        {
            Assembly assembly = forcedLoadReference.Assembly;

            Version version = assembly.GetName().Version;

            Assert.AreEqual(Constants.Expectations.VersionMajor, version.Major);
            Assert.AreEqual(Constants.Expectations.VersionMinor, version.Minor);
        }

        [TestMethod]
        public void test_CLASP_Interfaces_file_version()
        {
            Assembly assembly = forcedLoadReference.Assembly;

            FileVersionInfo fversion = FileVersionInfo.GetVersionInfo(assembly.Location);

            Assert.AreEqual(Constants.Expectations.VersionMajor, fversion.FileMajorPart);
            Assert.AreEqual(Constants.Expectations.VersionMinor, fversion.FileMinorPart);
        }
    }
}

