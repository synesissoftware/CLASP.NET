
namespace Test.Unit.OrderedDictionary
{
    using global::Clasp.Internal;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    [TestClass]
    public class OrderedDict_tester
    {
        [TestMethod]
        public void Test_Dictionary()
        {
            Dictionary<string, string> d = new Dictionary<string, string> ();

            d.Add("key-1", "value-1");
            d.Add("k2", "value-2");
            d.Add("Key-3", "value-3");
            d.Add("K4", "value-4");
            d["k2"] = "value-2";

            List<Tuple<string, string>> results = new List<Tuple<string, string>>(d.Count);

            foreach (var pair in d)
            {
                string k = pair.Key as string;
                string v = pair.Value as string;

                results.Add(Tuple.Create(k, v));
            }

            Assert.AreEqual(4, results.Count);

            Assert.AreEqual("key-1", results[0].Item1);
            Assert.AreEqual("value-1", results[0].Item2);

            Assert.AreEqual("k2", results[1].Item1);
            Assert.AreEqual("value-2", results[1].Item2);

            Assert.AreEqual("Key-3", results[2].Item1);
            Assert.AreEqual("value-3", results[2].Item2);

            Assert.AreEqual("K4", results[3].Item1);
            Assert.AreEqual("value-4", results[3].Item2);
        }

        [TestMethod]
        public void Test_OrderedDictionary()
        {
            OrderedDictionary d = new OrderedDictionary();

            d.Add("key-1", "value-1");
            d.Add("k2", "value-2");
            d.Add("Key-3", "value-3");
            d.Add("K4", "value-4");

            List<Tuple<string, string>> results = new List<Tuple<string, string>>(d.Count);

            foreach (DictionaryEntry pair in d)
            {
                string k = pair.Key as string;
                string v = pair.Value as string;

                results.Add(Tuple.Create(k, v));
            }

            Assert.AreEqual(4, results.Count);

            Assert.AreEqual("key-1", results[0].Item1);
            Assert.AreEqual("value-1", results[0].Item2);

            Assert.AreEqual("k2", results[1].Item1);
            Assert.AreEqual("value-2", results[1].Item2);

            Assert.AreEqual("Key-3", results[2].Item1);
            Assert.AreEqual("value-3", results[2].Item2);

            Assert.AreEqual("K4", results[3].Item1);
            Assert.AreEqual("value-4", results[3].Item2);
        }

#if DEBUG

        [TestMethod]
        public void Test_OrderedDict()
        {
            OrderedDict<string, string> d = new OrderedDict<string, string>();

            d.Add("key-1", "value-1");
            d.Add("k2", "value-2");
            d.Add("Key-3", "value-3");
            d.Add("K4", "value-4");

            List<Tuple<string, string>> results = new List<Tuple<string, string>>(d.Count);

            foreach (var pair in d)
            {
                string k = pair.Key as string;
                string v = pair.Value as string;

                results.Add(Tuple.Create(k, v));
            }

            Assert.AreEqual(4, results.Count);

            Assert.AreEqual("key-1", results[0].Item1);
            Assert.AreEqual("value-1", results[0].Item2);

            Assert.AreEqual("k2", results[1].Item1);
            Assert.AreEqual("value-2", results[1].Item2);

            Assert.AreEqual("Key-3", results[2].Item1);
            Assert.AreEqual("value-3", results[2].Item2);

            Assert.AreEqual("K4", results[3].Item1);
            Assert.AreEqual("value-4", results[3].Item2);
        }
#endif
    }
}
