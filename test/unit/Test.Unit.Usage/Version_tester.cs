
namespace Test.Unit.Usage
{
    using Clasp = global::SynesisSoftware.SystemTools.Clasp;
    using UsageUtil = global::SynesisSoftware.SystemTools.Clasp.Util.UsageUtil;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    [TestClass]
    public class Version_tester
    {
        #region fields

        private Assembly    assembly;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        [TestMethod]
        public void Test_ShowVersion_from_inferred_assembly()
        {
        }

        [TestMethod]
        public void Test_ShowVersion_from_specific_assembly_with_default_format()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowVersion((Clasp.Arguments)null, options);
            });

            s = s.TrimEnd();

            Assert.AreEqual("myprog version 0.1.2 (build 3)", s);
        }

        [TestMethod]
        public void Test_ShowVersion_from_specific_assembly_with_simple_custom_format()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";
                options[UsageUtil.Constants.OptionKeys.VersionFormat] = "{0} {1}.{2}.{3}.{4}";

                UsageUtil.ShowVersion((Clasp.Arguments)null, options);
            });

            s = s.TrimEnd();

            Assert.AreEqual("myprog 0.1.2.3", s);
        }
    }
}
