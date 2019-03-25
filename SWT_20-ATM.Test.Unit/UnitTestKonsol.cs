using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Assert = NUnit.Framework.Assert;

namespace SWT_20_ATM.Test.Unit
{

    [TestFixture]
    internal class UnitTestKonsol
    {
        private Konsol _uut;
        private StringWriter _writer;
        private IPlane _plane1;
        private IPlane _plane2;
        private List<IPlane> _planes;
        private List<List<IPlane>> _planesViolating;

        [SetUp]
        public void Init()
        {
            _uut = new Konsol();
            _writer = new StringWriter();

            //Sets stdoutput for Console.WriteLine to Writer.
            Console.SetOut( _writer );

            // Fake Plane Creation
            //Plane 1 is still, Plane 2 is moving.
            _plane1 = Substitute.For<IPlane>();
            _plane2 = Substitute.For<IPlane>();

            _plane1.Tag.Returns( "Test1" );
            _plane1.XCoordinate.Returns( 10000 );
            _plane1.YCoordinate.Returns( 10000 );
            _plane1.Altitude.Returns( 10000 );
            _plane1.LastUpdate.Returns( DateTime.Now );

            _plane2.Tag.Returns( "Test2" );
            _plane2.XCoordinate.Returns( 10000 );
            _plane2.YCoordinate.Returns( 10000 );
            _plane2.Altitude.Returns( 10000 );
            _plane2.LastUpdate.Returns( DateTime.Now );
            _plane2.Speed.Returns( 500.0 );          // <- Indicate plane is moving



            _planes = new List<IPlane> { _plane1, _plane2 };

            List<IPlane> planePair = new List<IPlane>{_plane1, _plane2};
            _planesViolating = new List<List<IPlane>> { planePair };


        }

        [TestCase]
        public void RenderPlanes()
        {
            _uut.RenderPlanes( _planes );

            string teststring = _writer.ToString();
            string expectedOutput = "Test1: Coordinates x-y: 10000-10000 Altitude: 10000\r\nTest2: Coordinates x-y: 10000-10000 Altitude: 10000 Velocity: 500 Compass course: 0\r\n";
            Assert.AreEqual( teststring, expectedOutput );
        }


        [TestCase]
        public void Render2ViolatingPlanes()
        {
            _uut.RenderViolations( _planesViolating );

            string teststring = _writer.ToString();
            string expectedOutput = "The Planes Test1, Test2 has violated the Separation rule.\r\n";
            Assert.AreEqual( expectedOutput, teststring );
        }

        [TestCase]
        public void Render1ViolatingPlane()
        {
            _planesViolating[ 0 ].RemoveAt( 1 );
            _uut.RenderViolations( _planesViolating );

            string teststring = _writer.ToString();
            string expectedOutput = "The Plane Test1 has violated the Separation rule.\r\n";
            Assert.AreEqual( expectedOutput, teststring );
        }
    }
}
