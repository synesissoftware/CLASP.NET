
namespace test_component_CLASP_1
{
    using global::Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Recls;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    static class TracedInvoker
    {
        public static void Invoke<T0>(Action<T0> action, T0 arg0)
        {
            try
            {
                action(arg0);
            }
            catch(Exception x)
            {
                string function = null;

                if(null != action.Target)
                {
                    function = String.Format("{0}.{1}", action.Target.GetType(), action.Method);
                }
                else
                {
                    function = action.Method.ToString();
                }

                Console.Error.WriteLine("{{{0}({1})}} failed: {2}", function, arg0, x.Message);

                throw;
            }
        }
    }

    [TestClass]
    public class test_component_CLASP_tester
    {
        private string cwd;
        private string dir;

        class MethodRecorder
            : IDisposable
        {
            private readonly string m_methodName;

            public MethodRecorder(string methodName)
            {
                m_methodName = methodName;

                Console.WriteLine("{0}(): [thread: {1}]", methodName, Thread.CurrentThread.ManagedThreadId);
            }

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Console.WriteLine("~{0}(): [thread: {1}]", m_methodName, Thread.CurrentThread.ManagedThreadId);
            }
            #endregion
        }

        private static void ClearDirectory(string path)
        {
            // 1. delete files

            foreach(IEntry entry in FileSearcher.Search(path, null, SearchOptions.Files | SearchOptions.DoNotLockDirectory))
            {
                Console.WriteLine("deleting file: {0}", entry.SearchRelativePath);

                File.Delete(entry.Path);
            }

            // 2. delete directories

            List<IEntry> directories = new List<IEntry>();
            foreach(IEntry entry in FileSearcher.DepthFirst.Search(path, null, SearchOptions.Directories | SearchOptions.DoNotLockDirectory))
            {
                //Console.WriteLine("directory: {0}", entry.SearchRelativePath);

                directories.Add(entry);
            }
            //directories.Sort(new Comparison<string>(delegate(string x, string y) { return x.Length > y.Length; }));
            directories.Sort((x, y) => y.Path.Length - x.Path.Length);
            foreach(IEntry entry in directories)
            {
                Console.WriteLine("deleting directory: {0}", entry.SearchRelativePath);
                Directory.Delete(entry.Path);
            }
        }

        private static void CreateFile(string dir, string file, string contents)
        {
            string path = Path.Combine(dir, file);

            using(FileStream stm = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                using(StreamWriter writer = new StreamWriter(stm))
                {
                    writer.Write(contents);
                }
            }
        }

        [TestInitialize]
        public void Setup()
        {
            using(new MethodRecorder("Setup"))
            {
                cwd = Environment.CurrentDirectory;
                dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"SynesisSoftware.SystemTools.Clasp\test_component_Clasp_tester");

                Directory.CreateDirectory(dir);
                Environment.CurrentDirectory = dir;

                TracedInvoker.Invoke(ClearDirectory, dir);

                Directory.CreateDirectory(@"dir1");
                Directory.CreateDirectory(@"dir1\subdir1");
                Directory.CreateDirectory(@"dir1\subdir2");
                Directory.CreateDirectory(@"dir2");

                CreateFile(dir, @"abc.txt", "abc\ndef\nghi");
                CreateFile(dir, @"x.txt", "x\nxx\nxxx\nxxxx");
                CreateFile(dir, @"-", "-\n--\n---\n----");
                CreateFile(dir, @"dir2\def.txt", "abc\ndef\nghi");
                CreateFile(dir, @"dir2\y.txt", "x\nxx\nxxx\nxxxx");
                CreateFile(dir, @"dir2\-", "-\n--\n---\n----");
                CreateFile(dir, @"dir1\subdir2\ghi.txt", "abc\ndef\nghi");
                CreateFile(dir, @"dir1\subdir2\z.txt", "x\nxx\nxxx\nxxxx");
                CreateFile(dir, @"dir1\subdir2\-", "-\n--\n---\n----");
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            using(new MethodRecorder("Teardown"))
            {
                //System.Threading.Thread.Sleep(15000);

                Environment.CurrentDirectory = cwd;

                TracedInvoker.Invoke(ClearDirectory, dir);

                TracedInvoker.Invoke(Directory.Delete, dir);
            }
        }

        [TestMethod]
        public void Test_Wildcards_1()
        {
            using(new MethodRecorder("Test_Wildcards_1"))
            {
                Arguments args = new Arguments(new string[] { "abc.txt", "x.txt" });

                Assert.AreEqual(2, args.Values.Count);
                Assert.AreEqual("abc.txt", args.Values[0].Value);
                Assert.AreEqual("x.txt", args.Values[1].Value);
            }
        }

        [TestMethod]
        public void Test_Wildcards_2()
        {
            using(new MethodRecorder("Test_Wildcards_1"))
            {
                Arguments args = new Arguments(new string[] { "*.txt" }, ParseOptions.DontExpandWildcardsOnWindows);

                Assert.AreEqual(1, args.Values.Count);
                Assert.AreEqual("*.txt", args.Values[0].Value);
            }
        }

        [TestMethod]
        public void Test_Wildcards_3()
        {
            using(new MethodRecorder("Test_Wildcards_1"))
            {
                Arguments args = new Arguments(new string[] { "*.txt" });

                Assert.AreEqual(2, args.Values.Count);
                Assert.AreEqual("abc.txt", args.Values[0].Value);
                Assert.AreEqual("x.txt", args.Values[1].Value);
            }
        }

        [TestMethod]
        public void Test_Wildcards_4()
        {
            using(new MethodRecorder("Test_Wildcards_1"))
            {
                Arguments args = new Arguments(new string[] { @"dir2\*.txt" });

                Assert.AreEqual(2, args.Values.Count);
                Assert.AreEqual(@"dir2\def.txt", args.Values[0].Value);
                Assert.AreEqual(@"dir2\y.txt", args.Values[1].Value);
            }
        }
    }
}