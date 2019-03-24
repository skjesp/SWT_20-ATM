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
            var uut = new FileLogger();
        }

        [TestCase("C:\Users\Mads\Documents")]
        void ReceiveString_Constructor(string x)
        {

        }
    }
}
