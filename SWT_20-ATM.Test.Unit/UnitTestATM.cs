using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestClass]
    public class UnitTestATM
    {
        private ATM uut;
        private ILogger _logger;
        private IAirspace _airspace;
        private IPlaneSeparation _planeSeparation;

        [SetUp]
        public void init()
        {
            _logger = NSubstitute.Substitute.For<ILogger>();
            _airspace = NSubstitute.Substitute.For<IAirspace>();
            _planeSeparation = Substitute.For<IPlaneSeparation>();


            // Air Traffic Monitor
            uut = new ATM( _airspace, _planeSeparation, _logger );
        }


        #region UpdatePlaneList_UnitTests
        [TestCase()]
        public void test_UpdatePlaneList_calls_airspaceWithCorrectArgs()
        {
            List<IPlane> planeList = new List<IPlane>();

            IPlane plane1 = GetDummyIPlane();

            // override return values
            plane1.XCoordinate.Returns( 1 );
            plane1.YCoordinate.Returns( 2 );
            plane1.Altitude.Returns( 3 );

            planeList.Add( plane1 );

            uut.UpdatePlaneList( planeList );

            _airspace.Received( 1 ).IsWithinArea( Arg.Is<int>( x => x == 1 ),
                                                   Arg.Is<int>( x => x == 2 ),
                                                   Arg.Is<int>( x => x == 3 )
                                                  );
        }

        [TestCase( 0 )]
        [TestCase( 1 )]
        [TestCase( 5 )]
        [TestCase( 10 )]
        public void test_UpdatePlaneList_calls_airspaceCorrectNumberOfTimes( int count )
        {
            List<IPlane> planeList = new List<IPlane>();


            for ( int i = 0; i < count; i++ )
            {
                IPlane plane = GetDummyIPlane();

                planeList.Add( plane );
            }

            uut.UpdatePlaneList( planeList );

            // Check if function was called correct number of times
            _airspace.Received( count ).IsWithinArea( Arg.Any<int>(),
                                                    Arg.Any<int>(),
                                                    Arg.Any<int>()
                                                );
        }

        [TestCase( 0 )]
        [TestCase( 1 )]
        [TestCase( 5 )]
        [TestCase( 10 )]
        public void test_UpdatePlaneList_Adds_CorrectAmountOfPlanesToList( int count )
        {
            var planeList = new List<IPlane>();

            // Create X amount of planes
            for ( int i = 0; i < count; i++ )
            {
                IPlane newPlane = GetDummyIPlane();
                planeList.Add( newPlane );
            }

            // Make Airspace always return true, so all planes will be added
            _airspace.IsWithinArea( Arg.Any<int>(),
                                    Arg.Any<int>(),
                                    Arg.Any<int>()
                                ).Returns( true );

            // Update uut planeList
            uut.UpdatePlaneList( planeList );


            Assert.AreEqual( count, uut.PlaneList.Count );
        }

        [TestCase]
        public void test_UpdatePlaneList_AddsPlaneIfInAirspace()
        {
            var planeList = new List<IPlane>();


            IPlane newPlane = GetDummyIPlane();
            planeList.Add( newPlane );

            _airspace.IsWithinArea( Arg.Any<int>(), // <- Only return true if x is less or equal to 10
                                    Arg.Any<int>(),
                                    Arg.Any<int>()
                                ).Returns( true );

            // Update uut planeList
            uut.UpdatePlaneList( planeList );

            Assert.AreEqual( true, uut.PlaneList.Contains( newPlane ) );
        }

        [TestCase]
        public void test_UpdatePlaneList_DoesNotAddPlaneIfNotInAirspace()
        {
            var planeList = new List<IPlane>();


            IPlane newPlane = GetDummyIPlane();
            planeList.Add( newPlane );

            _airspace.IsWithinArea( Arg.Any<int>(), // <- Only return true if x is less or equal to 10
                Arg.Any<int>(),
                Arg.Any<int>()
            ).Returns( false );

            // Update uut planeList
            uut.UpdatePlaneList( planeList );

            Assert.AreEqual( false, uut.PlaneList.Contains( newPlane ) );
        }

        #endregion

        #region UpdateViolatingPlanes_UnitTest

        public void UpdateViolatingPlanes_findsCorrectPairs()
        {

        }


        #endregion

        #region Utility_Functions
        /// <summary>
        /// Creates a dummy IPlane with random values.
        /// </summary>
        /// <returns>IPlane With random atribute values</returns>
        private IPlane GetDummyIPlane()
        {
            IPlane dummyPlane = Substitute.For<IPlane>();
            dummyPlane.Tag.Returns( "DummyPlane" );
            dummyPlane.XCoordinate.Returns( 1 );
            dummyPlane.YCoordinate.Returns( 1 );
            dummyPlane.Altitude.Returns( 1 );
            dummyPlane.LastUpdate.Returns( DateTime.Now );
            dummyPlane.Speed.Returns( 50 );
            dummyPlane.Direction.Returns( 180 );

            return dummyPlane;
        }
        #endregion

    }
}
