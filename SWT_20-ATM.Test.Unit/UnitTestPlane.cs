using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    class UnitTestPlane
    {
        private Plane uut;
        private DateTime uutDateTime;
        private DateTime validDateTime;

        [SetUp]
        public void init()
        {
            uutDateTime = new DateTime(2000,01,01,12,30,30);
            validDateTime = new DateTime(2000,01,01,12,30,31);
            uut = new Plane("UUT.tag", 1000, 1000, 500, uutDateTime);
        }
        
        [TestCase("wrongTag")]
        public void Update_WrongTag_ReturnFalse(string wrongTag)
        {
            Plane newPlane = new Plane(wrongTag, 2000, 2000, 500, validDateTime);
            
            Assert.IsFalse(uut.Update(newPlane));
        }

        [TestCase("UUT.tag")]
        public void Update_SameTag_ReturnTrue(string RightTag)
        {
            Plane newPlane = new Plane(RightTag, 2000, 2000, 500, validDateTime);

            Assert.IsTrue(uut.Update(newPlane));
        }

        [TestCase(29)]  //1 second behind UUT
        public void Update_OlderTimestamp_ReturnFalse(int time)
        {
            DateTime Wrongtime = new DateTime(2000,01,01,12,30,time);
            Plane newPlane = new Plane("UUT.tag", 1000, 1000, 500, Wrongtime);

            Assert.IsFalse(uut.Update(newPlane));
        }

        [TestCase(31)]  //1 second behind UUT
        public void Update_NewerTimestamp_ReturnTrue(int time)
        {
            DateTime Wrongtime = new DateTime(2000, 01, 01, 12, 30, time);
            Plane newPlane = new Plane("UUT.tag", 1000, 1000, 500, Wrongtime);

            Assert.IsTrue(uut.Update(newPlane));
        }

    }
}
