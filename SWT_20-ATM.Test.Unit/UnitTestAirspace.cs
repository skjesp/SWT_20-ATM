using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SWT_20_ATM;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestAirspace
    {
 
        [TestCase]
        public void AirspaceWithoutShapes_containsPoint_returns_false()
        {
            Airspace uut = new Airspace();
            Assert.AreEqual(false, uut.IsWithinArea(1, 1, 1));
        }

        [TestCase(5, 5, 5, true)]           // Inside airspace
        [TestCase(15, 15, 15, false)]       // Outside airspace
        public void Points_check_in_AirspaceWithOneShape(int x, int y, int z, bool result)
        {
            Airspace uut = new Airspace();
            uut.AddShape(new Cuboid(0, 0, 0, 10, 10, 10));

            Assert.AreEqual(result, uut.IsWithinArea(x, y, z));
        }

        [TestCase(5, 5, 5, true)]          // Inside airspace  (shape 1)
        [TestCase(15, 15, 15, true)]       // Inside airspace  (shape 2)
        [TestCase(50, 50, 50, false)]      // Outside airspace 
        public void Points_check_in_AirspaceWithMultipleShapes(int x, int y, int z, bool result)
        {
            Airspace uut = new Airspace();
            uut.AddShape(new Cuboid(0, 0, 0, 10, 10, 10));      // Shape 1
            uut.AddShape(new Cuboid(10, 10, 10, 20, 20, 20));   // Shape 2

            Assert.AreEqual(result, uut.IsWithinArea(x, y, z));
        }
    }
}
