
namespace Test.Unit.CLASP.ns_1
{
    using global::Clasp;
    using global::Clasp.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Reflection;

    [TestClass]
    public class IArgument_tester
    {
        [TestMethod]
        public void test_IArgument_interface_exists()
        {
            Assert.IsNotNull(typeof(IArgument));
        }

        [TestMethod]
        public void test_IArgument_Type_property()
        {
            Type typeofILogService = typeof(IArgument);

            PropertyInfo pi = typeofILogService.GetProperty("Type");

            Assert.IsNotNull(pi);

            MethodInfo[] mis = pi.GetAccessors();

            Assert.AreEqual(1, mis.Length);

            Assert.IsTrue(mis[0].IsAbstract);
            Assert.IsFalse(mis[0].IsConstructor);
            Assert.IsTrue(mis[0].IsPublic);
            Assert.IsFalse(mis[0].IsStatic);
            Assert.AreEqual("get_Type", mis[0].Name);
            Assert.AreEqual(typeof(ArgumentType), mis[0].ReturnType);
            Assert.AreEqual(0, mis[0].GetParameters().Length);
        }

        [TestMethod]
        public void test_IArgument_ResolvedName_property()
        {
            Type typeofILogService = typeof(IArgument);

            PropertyInfo pi = typeofILogService.GetProperty("ResolvedName");

            Assert.IsNotNull(pi);

            MethodInfo[] mis = pi.GetAccessors();

            Assert.AreEqual(1, mis.Length);

            Assert.IsTrue(mis[0].IsAbstract);
            Assert.IsFalse(mis[0].IsConstructor);
            Assert.IsTrue(mis[0].IsPublic);
            Assert.IsFalse(mis[0].IsStatic);
            Assert.AreEqual("get_ResolvedName", mis[0].Name);
            Assert.AreEqual(typeof(string), mis[0].ReturnType);
            Assert.AreEqual(0, mis[0].GetParameters().Length);
        }

        [TestMethod]
        public void test_IArgument_GivenName_property()
        {
            Type typeofILogService = typeof(IArgument);

            PropertyInfo pi = typeofILogService.GetProperty("GivenName");

            Assert.IsNotNull(pi);

            MethodInfo[] mis = pi.GetAccessors();

            Assert.AreEqual(1, mis.Length);

            Assert.IsTrue(mis[0].IsAbstract);
            Assert.IsFalse(mis[0].IsConstructor);
            Assert.IsTrue(mis[0].IsPublic);
            Assert.IsFalse(mis[0].IsStatic);
            Assert.AreEqual("get_GivenName", mis[0].Name);
            Assert.AreEqual(typeof(string), mis[0].ReturnType);
            Assert.AreEqual(0, mis[0].GetParameters().Length);
        }

        [TestMethod]
        public void test_IArgument_Value_property()
        {
            Type typeofILogService = typeof(IArgument);

            PropertyInfo pi = typeofILogService.GetProperty("Value");

            Assert.IsNotNull(pi);

            MethodInfo[] mis = pi.GetAccessors();

            Assert.AreEqual(1, mis.Length);

            Assert.IsTrue(mis[0].IsAbstract);
            Assert.IsFalse(mis[0].IsConstructor);
            Assert.IsTrue(mis[0].IsPublic);
            Assert.IsFalse(mis[0].IsStatic);
            Assert.AreEqual("get_Value", mis[0].Name);
            Assert.AreEqual(typeof(string), mis[0].ReturnType);
            Assert.AreEqual(0, mis[0].GetParameters().Length);
        }

        [TestMethod]
        public void test_IArgument_Index_property()
        {
            Type typeofILogService = typeof(IArgument);

            PropertyInfo pi = typeofILogService.GetProperty("Index");

            Assert.IsNotNull(pi);

            MethodInfo[] mis = pi.GetAccessors();

            Assert.AreEqual(1, mis.Length);

            Assert.IsTrue(mis[0].IsAbstract);
            Assert.IsFalse(mis[0].IsConstructor);
            Assert.IsTrue(mis[0].IsPublic);
            Assert.IsFalse(mis[0].IsStatic);
            Assert.AreEqual("get_Index", mis[0].Name);
            Assert.AreEqual(typeof(int), mis[0].ReturnType);
            Assert.AreEqual(0, mis[0].GetParameters().Length);
        }

    }
}
