﻿using NSubstitute;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    class UnitTestPlane
    {
        private Plane uut;
        private DateTime uutDateTime;
        private DateTime validDateTime;

        [SetUp]
        public void init()
        {
            uutDateTime = new DateTime( 2000, 01, 01, 12, 30, 30 );
            validDateTime = new DateTime( 2000, 01, 01, 12, 30, 20 );
            uut = new Plane( "UUT.Tag", 1000, 1000, 500, uutDateTime );
        }

        [TestCase( "wrongTag" )]
        public void Update_WrongTag_ReturnFalse( string wrongTag )
        {
            Plane newPlane = new Plane( wrongTag, 2000, 2000, 500, validDateTime );

            Assert.IsFalse( uut.Update( newPlane ) );
        }

        [TestCase( "UUT.Tag" )]
        public void Update_SameTag_ReturnTrue( string RightTag )
        {
            Plane newPlane = new Plane( RightTag, 2000, 2000, 500, validDateTime );

            Assert.IsTrue( uut.Update( newPlane ) );
        }

        [TestCase( 29 )]  //1 second behind UUT
        public void Update_OlderTimestamp_ReturnTrue( int time )
        {
            DateTime Wrongtime = new DateTime( 2000, 01, 01, 12, 30, time );
            Plane newPlane = new Plane( "UUT.Tag", 1000, 1000, 500, Wrongtime );

            Assert.IsTrue( uut.Update( newPlane ) );
        }

        [TestCase( 31 )]  //1 second behind UUT
        public void Update_NewerTimestamp_ReturnFalse( int time )
        {
            DateTime Wrongtime = new DateTime( 2000, 01, 01, 12, 30, time );
            Plane newPlane = new Plane( "UUT.Tag", 1000, 1000, 500, Wrongtime );

            Assert.IsFalse( uut.Update( newPlane ) );
        }

        /// <summary>
        /// Test if update returns false if invalid arguments is given.
        /// </summary>
        [TestCase]
        public void Update_InvalidSpeed_throwException()
        {
            // Make one plane with exact same properties as uut
            IPlane newPlane = Substitute.For<IPlane>();
            newPlane = Substitute.For<IPlane>();
            newPlane.Tag.Returns( "UUT.Tag" );
            newPlane.XCoordinate.Returns( 1000 );
            newPlane.YCoordinate.Returns( 1000 );
            newPlane.Altitude.Returns( 500 );
            newPlane.LastUpdate.Returns( uutDateTime );


            // newplane has flown 1 meter  on the xCoordinate.
            newPlane.XCoordinate.Returns( uut.XCoordinate + 1 );

            // This should fail since newPlane has moved but LastUpdate is the same
            // This means that newPlane moved in no time, which is impossible
            Assert.IsFalse( uut.Update( newPlane ) );
        }
    }
}
