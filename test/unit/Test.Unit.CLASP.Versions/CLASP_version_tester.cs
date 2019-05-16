
namespace Test.Unit.CLASP.Versions
{
    using global::Clasp;
    using global::Clasp.Interfaces;

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
                internal const int  VersionMinor    =   20;
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

