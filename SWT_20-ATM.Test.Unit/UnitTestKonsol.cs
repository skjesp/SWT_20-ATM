using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{

    [TestFixture]
    class UnitTestKonsol
    {
        private Konsol uut;
        private StringWriter Writer;
        private Plane Plane1;
        private Plane Plane2;
        private List<Plane> Planes;

        [SetUp]
        public void Init()
        {
            uut = new Konsol();
            Writer = new StringWriter();

            //Sets stdoutput for Console.WriteLine to Writer.
            Console.SetOut( Writer );

            //Plane Creation
            //Plane 1 is still, Plane 2 is moving.
            Plane1 = new Plane( "Test1", 10000, 10000, 10000, DateTime.Now );
            Plane2 = new Plane( "Test2", 10000, 10000, 10000, DateTime.Now );
            Plane2.speed = 500.0;
            Planes = new List<Plane> { Plane1, Plane2 };
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
            Assert.AreEqual( teststring, expectedOutput );
        }

        [TestCase]
        public void Render1ViolatingPlane()
        {
            Planes.RemoveAt( 1 );
            uut.RenderViolations( Planes );

            string teststring = Writer.ToString();
            string expectedOutput = "The Plane Test1 has violated the Separation rule.\r\n";
            Assert.AreEqual( teststring, expectedOutput );
        }
    }
}
