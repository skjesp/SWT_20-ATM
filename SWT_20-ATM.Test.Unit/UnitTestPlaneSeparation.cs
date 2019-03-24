using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    class UnitTestPlaneSeparation
    {
        private PlaneSeparation uut;

        [SetUp]
        public void init()
        {
            // Create a uut
            uut = new PlaneSeparation( 5000, 300 );
        }

        [TestCase( 1000, 1000, 500, 1000, 1000, 500 )]    //Planes are in the same posistion.
        [TestCase( 1000, 1000, 500, 5999, 1000, 500 )]    //Planes X-coordinates are just close enough to trig Separation.
        [TestCase( 1000, 1000, 500, 1000, 5999, 500 )]    //Planes Y-coordinates are just close enough to trig Separation.
        [TestCase( 1000, 1000, 500, 1000, 1000, 799 )]    //Planes Z-coordinates are just close enough to trig Separation.
        public void PlanesTooClose( int x1, int y1, int z1, int x2, int y2, int z2 )
        {
            // Create two fake planes with the given parameters
            IPlane plane1 = Substitute.For<IPlane>();
            plane1.Tag.Returns( "AAA" );
            plane1.XCoordinate.Returns( x1 );
            plane1.YCoordinate.Returns( y1 );
            plane1.Altitude.Returns( z1 );
            plane1.LastUpdate.Returns( DateTime.Today );

            IPlane plane2 = Substitute.For<IPlane>();
            plane2.Tag.Returns( "BBB" );
            plane2.XCoordinate.Returns( x2 );
            plane2.YCoordinate.Returns( y2 );
            plane2.Altitude.Returns( z2 );
            plane2.LastUpdate.Returns( DateTime.Today );

            // Add planes them to a list
            List<IPlane> testList = new List<IPlane> { plane1, plane2 };

            List<List<IPlane>> returnList = uut.CheckPlanes( testList );

            // Check if plane is close enough
            Assert.That( returnList[0][0].Tag, Is.EqualTo( testList[0].Tag ) );
        }

        [TestCase( 1000, 1000, 500, 6000, 1000, 500 )]    //Planes X-coordinates are just far enough to not trig Separation.
        [TestCase( 1000, 1000, 500, 1000, 6000, 500 )]    //Planes Y-coordinates are just far enough to not trig Separation.
        [TestCase( 1000, 1000, 500, 1000, 1000, 800 )]    //Planes Z-coordinates are just far enough to not trig Separation.
        public void PlanesNotTooClose( int x1, int y1, int z1, int x2, int y2, int z2 )
        {
            // Create two fake planes with the given parameters
            IPlane plane1 = Substitute.For<IPlane>();
            plane1.Tag.Returns( "AAA" );
            plane1.XCoordinate.Returns( x1 );
            plane1.YCoordinate.Returns( y1 );
            plane1.Altitude.Returns( z1 );
            plane1.LastUpdate.Returns( DateTime.Today );

            IPlane plane2 = Substitute.For<IPlane>();
            plane2.Tag.Returns( "AAA" );
            plane2.XCoordinate.Returns( x2 );
            plane2.YCoordinate.Returns( y2 );
            plane2.Altitude.Returns( z2 );
            plane2.LastUpdate.Returns( DateTime.Today );

            // Add planes to a list
            List<IPlane> testList = new List<IPlane> { plane1, plane2 };


            List<List<IPlane>> ReturnList = new List<List<IPlane>>();

            ReturnList = uut.CheckPlanes( testList );

            // If return list is empty that means no planes were violating the rule
            Assert.IsEmpty( ReturnList );
        }
    }
}
