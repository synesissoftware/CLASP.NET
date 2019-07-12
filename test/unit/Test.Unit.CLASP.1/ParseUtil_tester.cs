
namespace Test.Unit.CLASP.ns_1
{
    using global::Clasp.Util;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class ParseUtil_tester
    {
        [TestMethod]
        public void test_ParseUtil_type_exists()
        {
            Assert.IsNotNull(typeof(ParseUtil));
        }

        #region ParseBool() tests

        [TestMethod]
        public void test_ParseBool_valid_forms()
        {
            Assert.IsTrue(ParseUtil.ParseBool("true"));
            Assert.IsTrue(ParseUtil.ParseBool("True"));
            Assert.IsTrue(ParseUtil.ParseBool("TRUE"));

            Assert.IsFalse(ParseUtil.ParseBool("false"));
            Assert.IsFalse(ParseUtil.ParseBool("False"));
            Assert.IsFalse(ParseUtil.ParseBool("FALSE"));

            Assert.IsTrue(ParseUtil.ParseBool("yes"));
            Assert.IsTrue(ParseUtil.ParseBool("Yes"));
            Assert.IsTrue(ParseUtil.ParseBool("YES"));

            Assert.IsFalse(ParseUtil.ParseBool("no"));
            Assert.IsFalse(ParseUtil.ParseBool("No"));
            Assert.IsFalse(ParseUtil.ParseBool("NO"));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void test_ParseBool_invalid_form_1()
        {
            ParseUtil.ParseBool("yel|low");

            Assert.Fail("should not get here");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void test_ParseBool_invalid_form_2()
        {
            ParseUtil.ParseBool("-13");

            Assert.Fail("should not get here");
        }

        [TestMethod]
        public void test_ParseBool_invalid_form_3()
        {
            try
            {
                ParseUtil.ParseBool("-13");

                Assert.Fail("should not get here");
            }
            catch (FormatException x)
            {
                Assert.IsTrue(x.ToString().Contains("valid"));

                Assert.IsTrue(0 == x.Data.Count);

                var v2 = x.HelpLink;

                Assert.IsNull(x.InnerException);

                Assert.IsTrue(x.Message.Contains("String was not recog"));
                Assert.IsTrue(x.Message.Contains("as a valid Boolean"));

                var v4 = x.Source;

                Assert.IsTrue(x.StackTrace.Contains("ParseUtil.cs"));

                var v6 = x.TargetSite;

                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void test_TryParseBool_standard_valid_forms()
        {
            bool    v;

            // True / False

            Assert.IsTrue(ParseUtil.TryParseBool("true", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("True", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("tRUE", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("TRUE", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("false", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("False", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("fALSE", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("FALSE", out v));
            Assert.IsFalse(v);

            // Yes / No

            Assert.IsTrue(ParseUtil.TryParseBool("yes", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("Yes", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("yES", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("YES", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool("no", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("No", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("nO", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool("NO", out v));
            Assert.IsFalse(v);
        }

        [TestMethod]
        public void test_TryParseBool_standard_invalid_forms()
        {
            bool    v;

            // Yup / Nope

            Assert.IsFalse(ParseUtil.TryParseBool("yup", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("Yup", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("yUP", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("YUP", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("nope", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("Nope", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("nOPE", out v));

            Assert.IsFalse(ParseUtil.TryParseBool("NOPE", out v));
        }

        [TestMethod]
        public void test_TryParseBool_custom_()
        {
            string[] false_forms =
            {
                "negative",
                "nope",
            };

            string[] true_forms =
            {
                "affirmative"
            };

            bool    v;


            Assert.IsFalse(ParseUtil.TryParseBool(true_forms, false_forms, "true", out v));

            Assert.IsFalse(ParseUtil.TryParseBool(true_forms, false_forms, "True", out v));

            Assert.IsFalse(ParseUtil.TryParseBool(true_forms, false_forms, "TRUE", out v));


            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "negative", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "Negative", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "nEGATIVE", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "NEGATIVE", out v));
            Assert.IsFalse(v);


            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "nope", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "Nope", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "nOPE", out v));
            Assert.IsFalse(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "NOPE", out v));
            Assert.IsFalse(v);


            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "affirmative", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "Affirmative", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "aFFIRMATIVE", out v));
            Assert.IsTrue(v);

            Assert.IsTrue(ParseUtil.TryParseBool(true_forms, false_forms, "AFFIRMATIVE", out v));
            Assert.IsTrue(v);
        }
        #endregion
    }
}
