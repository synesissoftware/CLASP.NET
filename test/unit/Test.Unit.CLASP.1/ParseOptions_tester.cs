
namespace Test.Unit.CLASP._1
{
	using global::SynesisSoftware.SystemTools.Clasp;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;

	[TestClass]
	public class ParseOptions_tester
	{
		[TestMethod]
		public void test_ParseOptions_type_exists()
		{
			Assert.IsNotNull(typeof(ParseOptions));
		}

		[TestMethod]
		public void test_ParseOptions_enumerators_differ()
		{
			Assert.AreNotEqual(ParseOptions.None, ParseOptions.DoExpandWildcardsInAposquotesOnWindows);
			Assert.AreNotEqual(ParseOptions.None, ParseOptions.DontExpandWildcardsOnWindows);
			Assert.AreNotEqual(ParseOptions.None, ParseOptions.DontRecogniseDoublehyphenToStartValues);
			Assert.AreNotEqual(ParseOptions.None, ParseOptions.TreatSinglehyphenAsValue);

			Assert.AreNotEqual(ParseOptions.DoExpandWildcardsInAposquotesOnWindows, ParseOptions.DontExpandWildcardsOnWindows);
			Assert.AreNotEqual(ParseOptions.DoExpandWildcardsInAposquotesOnWindows, ParseOptions.DontRecogniseDoublehyphenToStartValues);
			Assert.AreNotEqual(ParseOptions.DoExpandWildcardsInAposquotesOnWindows, ParseOptions.TreatSinglehyphenAsValue);

			Assert.AreNotEqual(ParseOptions.DontExpandWildcardsOnWindows, ParseOptions.DontRecogniseDoublehyphenToStartValues);
			Assert.AreNotEqual(ParseOptions.DontExpandWildcardsOnWindows, ParseOptions.TreatSinglehyphenAsValue);

			Assert.AreNotEqual(ParseOptions.DontRecogniseDoublehyphenToStartValues, ParseOptions.TreatSinglehyphenAsValue);
		}
	}
}
