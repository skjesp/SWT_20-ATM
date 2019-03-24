using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            CorrectDateTime = new DateTime( 2000, 01, 01, 12, 30, 30, 500 );
            CorrectPlane = new Plane( "TEST123", 10000, 10000, 10000, CorrectDateTime );
        }

        [TestCase( "TEST123;10000;10000;10000;20000101123030500" )]
        public void ReceiveValidInput_SetDecoderList( string input )
        {
            var InputList = new List<string> { input };
            uut.Decode( InputList );
            Assert.That( uut.OldPlaneList[0].XCoordinate, Is.EqualTo( CorrectPlane.XCoordinate ) );
            Assert.That( uut.OldPlaneList[0].YCoordinate, Is.EqualTo( CorrectPlane.YCoordinate ) );
            Assert.That( uut.OldPlaneList[0].LastUpdate, Is.EqualTo( CorrectPlane.LastUpdate ) );
            Assert.That( uut.OldPlaneList[0].Tag, Is.EqualTo( CorrectPlane.Tag ) );
            Assert.That( uut.OldPlaneList[0].Altitude, Is.EqualTo( CorrectPlane.Altitude ) );
            Assert.That( uut.OldPlaneList[0].Direction, Is.EqualTo( CorrectPlane.Direction ) );
            Assert.That( uut.OldPlaneList[0].Speed, Is.EqualTo( CorrectPlane.Speed ) );
        }

        [TestCase( "TEST123;10000;10000;10000;20000101123030500", "TEST123;10000;10500;10000;20000101123031500" )] //Plane travelled 500 units along y-axis.

        public void Receive2Inputs_UpdateSpeedAndDirectionForPlanes( string input1, string input2 )
        {
            //First input
            var InputList = new List<string> { input1 };
            uut.Decode( InputList );

            //Second input
            InputList.Clear();
            InputList.Add( input2 );
            uut.Decode( InputList );

            Assert.That( uut.OldPlaneList[0].Speed, Is.EqualTo( 500 ) );
        }

        [TestCase( "TEST123;10000;10000;10000;20000101123030500", "TEST321;10000;10500;10000;20000101123031500" )]
        public void Receive2Inputs_HaveMultiplePlanesInOldPlaneList( string input1, string input2 )
        {
            //First input
            var InputList = new List<string> { input1 };
            uut.Decode( InputList );

            //Second input
            InputList.Add( input2 );
            uut.Decode( InputList );

            //Planes are in same position.
            uut.Decode( InputList );

            Assert.That( uut.OldPlaneList.Count, Is.EqualTo( 2 ) );
        }

        [TestCase]
        public void Receive2Inputs_Update_SpeedAndDirectionIsNaN()
        {
            //Create Plane with Invalid Speed
            Plane InvalidSpeedPlane = new Plane( "TEST123", 10000, 10000, 10000, CorrectDateTime );
            InvalidSpeedPlane.Speed = Double.NaN;

            //Create Plane with Invalid Direction
            Plane InvalidDirectionPlane = new Plane( "TEST321", 10000, 10000, 10000, CorrectDateTime );
            InvalidDirectionPlane.Direction = Double.NaN;

            List<Plane> PlaneWithNaNSpeed = new List<Plane> { InvalidSpeedPlane, InvalidDirectionPlane };

            //No planes were added to EmptyPlaneList because all planes that was to be added were invalid.
            List<Plane> EmptyPlaneList = uut.GetCompletePlanes( PlaneWithNaNSpeed );
            Assert.IsEmpty( EmptyPlaneList );
        }

    }
}
