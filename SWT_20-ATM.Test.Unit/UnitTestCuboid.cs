using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestCuboid
    {
        [TestCase( 0, 0, 0, true )]     // Test point on the corner 1
        [TestCase( 0, 0, 10, true )]     // Test point on the corner 2
        [TestCase( 0, 10, 0, true )]     // Test point on the corner 3
        [TestCase( 0, 10, 10, true )]     // Test point on the corner 4
        [TestCase( 10, 0, 0, true )]     // Test point on the corner 5
        [TestCase( 10, 0, 10, true )]     // Test point on the corner 6
        [TestCase( 10, 10, 0, true )]     // Test point on the corner 7
        [TestCase( 10, 10, 10, true )]     // Test point on the corner 8

        [TestCase( 5, 5, 5, true )]     // Test point in the middle

        [TestCase( -5, 5, 5, false )]     // Test point in the outside X-
        [TestCase( 15, -5, 5, false )]     // Test point in the outside X+

        [TestCase( 5, -5, 5, false )]     // Test point in the outside y-
        [TestCase( 5, 15, 5, false )]     // Test point in the outside y+

        [TestCase( 5, 5, -5, false )]     // Test point in the outside z-
        [TestCase( 5, 5, 15, false )]     // Test point in the outside z+
        public void CuboidContains_Points( int x, int y, int z, bool result )
        {
            Cuboid uut = new Cuboid( 0, 0, 0, 10, 10, 10 );

            Assert.AreEqual( result, uut.ContainsPoint( x, y, z ) );
        }

    }
}
