
// Created: 
// Updated: 3rd February 2014

namespace Test.Unit.CLASP._1
{
	using global::SynesisSoftware.SystemTools.Clasp.Util;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;

	[TestClass]
	public class ClaspUtil_tester
	{
		[TestMethod]
		public void test_ClaspUtil_type_exists()
		{
			Assert.IsNotNull(typeof(ClaspUtil));
		}

		#region ParseBool() tests

		[TestMethod]
		public void test_ClaspUtil_ParseBool_valid_forms()
		{
			Assert.IsTrue(ClaspUtil.ParseBool("true"));
			Assert.IsTrue(ClaspUtil.ParseBool("True"));
			Assert.IsTrue(ClaspUtil.ParseBool("TRUE"));

			Assert.IsFalse(ClaspUtil.ParseBool("false"));
			Assert.IsFalse(ClaspUtil.ParseBool("False"));
			Assert.IsFalse(ClaspUtil.ParseBool("FALSE"));
		
			Assert.IsTrue(ClaspUtil.ParseBool("yes"));
			Assert.IsTrue(ClaspUtil.ParseBool("Yes"));
			Assert.IsTrue(ClaspUtil.ParseBool("YES"));

			Assert.IsFalse(ClaspUtil.ParseBool("no"));
			Assert.IsFalse(ClaspUtil.ParseBool("No"));
			Assert.IsFalse(ClaspUtil.ParseBool("NO"));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void test_ClaspUtil_ParseBool_invalid_form_1()
		{
			ClaspUtil.ParseBool("yel|low");

			Assert.Fail("should not get here");
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void test_ClaspUtil_ParseBool_invalid_form_2()
		{
			ClaspUtil.ParseBool("-13");

			Assert.Fail("should not get here");
		}

		#endregion

	}
}
