using NSubstitute;
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
        private IPlane CorrectPlane;
        private DateTime CorrectDateTime;

        [SetUp]
        public void init()
        {
            uut = new Decoder();
            CorrectDateTime = new DateTime( 2000, 01, 01, 12, 30, 30, 500 );

            //Create fake Plane with Invalid Direction
            CorrectPlane = Substitute.For<IPlane>();
            CorrectPlane.Tag.Returns( "TEST123" );
            CorrectPlane.XCoordinate.Returns( 10000 );
            CorrectPlane.YCoordinate.Returns( 10000 );
            CorrectPlane.Altitude.Returns( 10000 );
            CorrectPlane.LastUpdate.Returns( CorrectDateTime );
        }

        [TestCase( "TEST123;10000;10000;10000;20000101123030500" )]
        public void ReceiveValidInput_SetDecoderList( string input )
        {
            var inputList = new List<string> { input };
            uut.Decode( inputList );

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
            var inputList = new List<string> { input1 };
            uut.Decode( inputList );

            //Second input
            inputList.Add( input2 );
            uut.Decode( inputList );

            //Planes are in same position.
            uut.Decode( inputList );

            Assert.That( uut.OldPlaneList.Count, Is.EqualTo( 2 ) );
        }

        [TestCase]
        public void Receive2Inputs_Update_SpeedAndDirectionIsNaN()
        {
            //Create fake Plane with Invalid 
            IPlane invalidSpeedPlane = Substitute.For<IPlane>();
            invalidSpeedPlane.Tag.Returns( "TEST123" );
            invalidSpeedPlane.XCoordinate.Returns( 10000 );
            invalidSpeedPlane.YCoordinate.Returns( 10000 );
            invalidSpeedPlane.LastUpdate.Returns( CorrectDateTime );

            invalidSpeedPlane.Speed.Returns( Double.NaN );

            //Create fake Plane with Invalid Direction
            IPlane invalidDirectionPlane = Substitute.For<IPlane>();
            invalidDirectionPlane.Tag.Returns( "TEST321" );
            invalidDirectionPlane.XCoordinate.Returns( 10000 );
            invalidDirectionPlane.YCoordinate.Returns( 10000 );
            invalidDirectionPlane.LastUpdate.Returns( CorrectDateTime );

            invalidDirectionPlane.Direction.Returns( Double.NaN );
            
            List<IPlane> planeWithNaNSpeed = new List<IPlane> { invalidSpeedPlane, invalidDirectionPlane };

            //No planes were added to EmptyPlaneList because all planes that was to be added were invalid.
            List<IPlane> emptyPlaneList = uut.GetCompletePlanes( planeWithNaNSpeed );
            Assert.IsEmpty( emptyPlaneList );
        }

        //[TestCase]
        //public void Receive2Inputs_Update_DirectionIsNaN()
        //{
        //    //Create fake Plane with Invalid Direction
        //    IPlane invalidDirectionPlane = Substitute.For<IPlane>();
        //    invalidDirectionPlane.Tag.Returns("TEST321");
        //    invalidDirectionPlane.XCoordinate.Returns(10000);
        //    invalidDirectionPlane.YCoordinate.Returns(10000);
        //    invalidDirectionPlane.LastUpdate.Returns(CorrectDateTime);

        //    invalidDirectionPlane.Speed.Returns(Double.NaN);
        //    List<IPlane> planeWithNaNSpeed = new List<IPlane> { invalidSpeedPlane, invalidDirectionPlane };

        //    //No planes were added to EmptyPlaneList because all planes that was to be added were invalid.
        //    List<IPlane> emptyPlaneList = uut.GetCompletePlanes(planeWithNaNSpeed);
        //    Assert.IsEmpty(emptyPlaneList);
        //}

    }
}
