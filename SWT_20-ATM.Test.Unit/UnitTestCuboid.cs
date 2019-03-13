using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SWT_20_ATM;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture(0,  0,  0,  10, 10, 10)]
    [TestFixture(10, 10, 10, 0,  0,  0)]
    public class UnitTestCuboid
    {
        private int x1;
        private int y1;
        private int z1;

        private int x2;
        private int y2;
        private int z2;

        private Cuboid uut;

        [SetUp]
        public void Init()
        {
            uut = new Cuboid(x1,y1,z1, x2,y2,z2);
        }

        [TestCase(0, 0, 0, false)]
        public void TestMethod1(int x, int y, int z, bool result)
        {
            //Assert.Equals(result, Is.EqualTo(uut.ContainsPoint(x, y, z)));
        }
        
    }
}
