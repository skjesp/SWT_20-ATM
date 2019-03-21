using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    class UnitTestDecoder
    {
        private Decoder uut;
        private Plane CorrectPlane;
        private DateTime CorrectDateTime;

        [SetUp]
        public void init()
        {
            uut = new Decoder();
            CorrectDateTime = new DateTime(2000,01,01,12,30,30,500);
            CorrectPlane = new Plane("TEST123",10000,10000,10000, CorrectDateTime);
        }

        [TestCase("TEST123;10000;10000;10000;20000101123030500")]
        public void ReceiveValidInput_SetDecoderList(string input)
        {
            var InputList = new List<string>{input};
            uut.Decode(InputList);
            Assert.That(uut.decoderList[0].Tag, Is.EqualTo(CorrectPlane.Tag));
            //NUnit.Framework.Assert.That(ReturnList[0][0].Tag, Is.EqualTo(testList[0].Tag));
        }

    }
}
