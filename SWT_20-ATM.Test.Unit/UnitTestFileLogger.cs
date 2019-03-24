using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestFileLogger
    {
        [SetUp]
        public void init()
        {
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

        void AddToLog_Test()
        {

        }
    }
}
