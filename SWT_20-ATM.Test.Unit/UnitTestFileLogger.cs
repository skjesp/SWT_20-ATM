using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestFileLogger
    {
        [SetUp]
        public void init()
        {
            uut = new FileLogger();
        }

        [TestCase(@"C:\Users\")]
        void ReceivePath_Constructor(string x)
        {
            uut = new FileLogger(x);
            Assert.That(uut._filePath, IsEqualTo(x));
        }

        // Maybe not necessary.
        /*void NoPathSpec_Constructor()
        {
            uut = new FileLogger();
            Assert.That(uut._filePath, IsEqualTo("../SepLog.txt"));
        }*/

        [TestCase("Test String 1")]
        [TestCase("Test String 2")]
        void AddToLog_Test(string x)
        {
            uut.AddToLog(x);
            string text = File.ReadAllText("../SepLog.txt");
            Assert.That(text, IsEqualTo(x));
        }
    }
}
