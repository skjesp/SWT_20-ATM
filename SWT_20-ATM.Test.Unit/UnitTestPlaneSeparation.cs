using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using NUnit.Framework;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    class UnitTestPlaneSeparation
    {
        [TestCase(1000, 1000, 500, 1000, 1000, 500)]    //Planes are in the same posistion.
        [TestCase(1000, 1000, 500, 5999, 1000, 500)]    //Planes X-coordinates are just close enough to trig Separation.
        [TestCase(1000, 1000, 500, 1000, 5999, 500)]    //Planes Y-coordinates are just close enough to trig Separation.
        [TestCase(1000, 1000, 500, 1000, 1000, 799)]    //Planes Z-coordinates are just close enough to trig Separation.
        public void PlanesTooClose(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            Plane plane1 = new Plane("AAA", x1, y1, z1, DateTime.Today);
            Plane plane2 = new Plane("BBB", x2, y2, z2, DateTime.Today);
            
            List<Plane> testList = new List<Plane>();
            testList.Add(plane1);
            testList.Add(plane2);
            
            PlaneSeparation uut = new PlaneSeparation(5000, 300);

            List<List<Plane>> ReturnList = new List<List<Plane>>();
            ReturnList = uut.CheckPlanes(testList);
            NUnit.Framework.Assert.That(ReturnList[0][0].Tag, Is.EqualTo(testList[0].Tag));

            //CollectionAssert.AreEquivalent(ReturnList[0], testList);
        }

        public void PlanesNotTooClose(Plane x, Plane y)
        {

        }
    }
}
