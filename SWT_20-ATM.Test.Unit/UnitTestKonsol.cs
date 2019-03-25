using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Assert = NUnit.Framework.Assert;

namespace SWT_20_ATM.Test.Unit
{

    [TestFixture]
    class UnitTestKonsol
    {
        private Konsol uut;
        private StringWriter Writer;
        private IPlane Plane1;
        private IPlane Plane2;
        private List<IPlane> Planes;

        [SetUp]
        public void Init()
        {
            uut = new Konsol();
            Writer = new StringWriter();

            //Sets stdoutput for Console.WriteLine to Writer.
            Console.SetOut( Writer );

            // Fake Plane Creation
            //Plane 1 is still, Plane 2 is moving.
            Plane1 = Substitute.For<IPlane>();
            Plane2 = Substitute.For<IPlane>();

            Plane1.Tag.Returns( "Test1" );
            Plane1.XCoordinate.Returns( 10000 );
            Plane1.YCoordinate.Returns( 10000 );
            Plane1.Altitude.Returns( 10000 );
            Plane1.LastUpdate.Returns( DateTime.Now );

            Plane2.Tag.Returns( "Test2" );
            Plane2.XCoordinate.Returns( 10000 );
            Plane2.YCoordinate.Returns( 10000 );
            Plane2.Altitude.Returns( 10000 );
            Plane2.LastUpdate.Returns( DateTime.Now );
            Plane2.Speed.Returns( 500.0 );          // <- Indicate plane is moving

            Planes = new List<IPlane> { Plane1, Plane2 };
        }

        [TestCase]
        public void RenderPlanes()
        {
            uut.RenderPlanes( Planes );

            string teststring = Writer.ToString();
            string expectedOutput = "Test1: Coordinates x-y: 10000-10000 Altitude: 10000\r\nTest2: Coordinates x-y: 10000-10000 Altitude: 10000 Velocity: 500 Compass course: 0\r\n";
            Assert.AreEqual( teststring, expectedOutput );
        }


        [TestCase]
        public void Render2ViolatingPlanes()
        {
            uut.RenderViolations( Planes );

            string teststring = Writer.ToString();
            string expectedOutput = "The Planes Test1, Test2 has violated the Separation rule.\r\n";
            Assert.AreEqual( expectedOutput, teststring );
        }

        [TestCase]
        public void Render1ViolatingPlane()
        {
            Planes.RemoveAt( 1 );
            uut.RenderViolations( Planes );

            string teststring = Writer.ToString();
            string expectedOutput = "The Plane Test1 has violated the Separation rule.\r\n";
            Assert.AreEqual( expectedOutput, teststring );
        }
    }
}
