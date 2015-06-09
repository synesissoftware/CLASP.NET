
namespace Test.Unit.CLASP._1
{
    using global::SynesisSoftware.SystemTools.Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class Alias_tester
    {
        [TestMethod]
        public void test_Alias_type_exists()
        {
            Assert.IsNotNull(typeof(Alias));
        }

        [TestMethod]
        public void test_Alias_construction_without_options()
        {
            ArgumentType[]  types = { ArgumentType.None, ArgumentType.Flag, ArgumentType.Option, ArgumentType.Value };
            string[]        names = { "-a|--argument|some argument", "--long-arg|--long-arg|a long argument with", "-x||a short argument without a long-name explicitly given" };

            foreach(ArgumentType argType in types)
            {
                foreach(string name in names)
                {
                    string[] splits = name.Split('|');

                    string  givenName       =   splits[0];
                    string  resolvedName    =   splits[1];
                    string  description     =   splits[2];

                    givenName       =   String.IsNullOrEmpty(givenName) ? null : givenName;
                    resolvedName    =   String.IsNullOrEmpty(resolvedName) ? null : resolvedName;
                    description     =   String.IsNullOrEmpty(description) ? null : description;

                    Alias alias = null;

                    switch(argType)
                    {
                        case ArgumentType.Flag:
                            alias = Alias.Flag(givenName, resolvedName, description);
                            break;
                        case ArgumentType.Option:
                            alias = Alias.Option(givenName, resolvedName, description);
                            break;
                        case ArgumentType.Value:
                        case ArgumentType.None:
                            continue;
                    }

                    Assert.AreEqual(argType, alias.Type);
                    Assert.AreEqual(givenName, alias.GivenName);
                    Assert.AreEqual(resolvedName, alias.ResolvedName);
                    Assert.AreEqual(description, alias.Description);
                }
            }
        }
    }
}
