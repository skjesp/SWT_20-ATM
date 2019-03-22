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
            Assert.That(uut.OldPlaneList[0].xCoordinate, Is.EqualTo(CorrectPlane.xCoordinate));
            Assert.That(uut.OldPlaneList[0].yCoordinate, Is.EqualTo(CorrectPlane.yCoordinate));
            Assert.That(uut.OldPlaneList[0].lastUpdate, Is.EqualTo(CorrectPlane.lastUpdate));
            Assert.That(uut.OldPlaneList[0].Tag, Is.EqualTo(CorrectPlane.Tag));
            Assert.That(uut.OldPlaneList[0].altitude, Is.EqualTo(CorrectPlane.altitude));
            Assert.That(uut.OldPlaneList[0].direction, Is.EqualTo(CorrectPlane.direction));
            Assert.That(uut.OldPlaneList[0].speed, Is.EqualTo(CorrectPlane.speed));
        }

        [TestCase("TEST123;10000;10000;10000;20000101123030500", "TEST123;10000;10500;10000;20000101123031500")] //Plane travelled 500 units along y-axis.
        public void Receive2Inputs_UpdateSpeedAndDirectionForPlanes(string input1, string input2)
        {
            //First input
            var InputList = new List<string> {input1};
            uut.Decode(InputList);

            //Second input
            InputList.Clear();
            InputList.Add(input2);
            uut.Decode(InputList);

            Assert.That(uut.OldPlaneList[0].speed,Is.EqualTo(500));
        }

    }
}
