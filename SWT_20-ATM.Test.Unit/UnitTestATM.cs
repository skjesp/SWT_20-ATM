using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Init()
        {
            _logger = NSubstitute.Substitute.For<ILogger>();
            _airspace = NSubstitute.Substitute.For<IAirspace>();

            _planeSeparation = Substitute.For<IPlaneSeparation>();
            _planeSeparation.CheckPlanes( Arg.Any<List<IPlane>>() ).Returns( new List<List<IPlane>>() );

            // Todo: delete comment
            //_planeSeparation = new PlaneSeparation( 500, 3000 );

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
        [TestCase]
        public void UpdateViolatingPlanes_Calls_PlaneSeparatorCheckPlanes_withCorrectArgs()
        {
            // Create dummyPlanes
            IPlane plane1 = GetDummyIPlane();

            // Add dummyPlanes to a list
            List<IPlane> planeList = new List<IPlane> { plane1 };

            // Make sure airspace always returns true so all given planes will be passed to UpdateViolatingPlanes()
            _airspace.IsWithinArea( 0, 0, 0 ).ReturnsForAnyArgs( true );

            uut.UpdatePlaneList( planeList );

            // Check that the argument was of the correct type
            _planeSeparation.Received().CheckPlanes( Arg.Any<List<IPlane>>() );
        }

        [TestCase]
        public void UpdateViolatingPlanes_Calls_AddToLog_withCorrectArgs_WhenNewViolationFound()
        {
            // Make sure airspace always returns true so all given planes will be passed to UpdateViolatingPlanes()
            _airspace.IsWithinArea( 0, 0, 0 ).ReturnsForAnyArgs( true );


            // Create dummyPlanes
            IPlane plane1 = GetDummyIPlane();
            IPlane plane2 = GetDummyIPlane();

            List<List<IPlane>> planeVIolationList = new List<List<IPlane>>();
            List<IPlane> planePair = new List<IPlane> { plane1, plane2 };

            planeVIolationList.Add( planePair );


            _planeSeparation.CheckPlanes( Arg.Any<List<IPlane>>() ).Returns( planeVIolationList );



            // Just to run code parameter does not matter
            uut.UpdatePlaneList( new List<IPlane>() );      // First time should run addToLog since planes is new
            uut.UpdatePlaneList( new List<IPlane>() );      // Second time nothing should happen sinces planes already exists

            // Get all _loggers Received calls from AddToLog where string argument matched 
            // Code example found at: https://stackoverflow.com/questions/52439697/how-to-check-any-of-multiple-overloads-called-nsubstitute
            // Answer posted by: David Tchepak sep 22'18
            var calls = _logger.ReceivedCalls()
                         .Where( x => x.GetMethodInfo().Name == nameof( _logger.AddToLog ) )
                         .Where( x => ( (string) x.GetArguments()[0] ).Contains( "Violates Separation condition!" ) );

            // Check if number of expected arguments was found
            Assert.AreEqual( 1, calls.Count() );
        }

        [TestCase]
        public void UpdateViolatingPlanes_Calls_AddToLog_withCorrectArgs_WhenViolationFoundDissapeared()
        {
            // Make sure airspace always returns true so all given planes will be passed to UpdateViolatingPlanes()
            _airspace.IsWithinArea( 0, 0, 0 ).ReturnsForAnyArgs( true );


            // Create dummyPlanes
            IPlane plane1 = GetDummyIPlane();
            IPlane plane2 = GetDummyIPlane();

            // PlaneViolationList Containing planes
            List<List<IPlane>> planeVIolationList = new List<List<IPlane>>();
            List<IPlane> planePair = new List<IPlane> { plane1, plane2 };
            planeVIolationList.Add( planePair );

            // PlaneViolationList containing no planes
            List<List<IPlane>> EmptyplaneViolationList = new List<List<IPlane>>();

            _planeSeparation.CheckPlanes( Arg.Any<List<IPlane>>() ).Returns( planeVIolationList );



            // Just to run code parameter does not matter
            uut.UpdatePlaneList( new List<IPlane>() );      // First time should run addToLog since planes is new


            _planeSeparation.CheckPlanes( Arg.Any<List<IPlane>>() ).Returns( EmptyplaneViolationList );

            uut.UpdatePlaneList( new List<IPlane>() );      // Second time violation disappeared

            // Get all _loggers Received calls from AddToLog where string argument matched 
            // Code example found at: https://stackoverflow.com/questions/52439697/how-to-check-any-of-multiple-overloads-called-nsubstitute
            // Answer posted by: David Tchepak sep 22'18
            var calls = _logger.ReceivedCalls()
                .Where( x => x.GetMethodInfo().Name == nameof( _logger.AddToLog ) )
                .Where( x => ( (string) x.GetArguments()[0] ).Contains( "no longer violates Separation condition!" ) );

            // Check if number of expected arguments was found
            Assert.AreEqual( 1, calls.Count() );
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
