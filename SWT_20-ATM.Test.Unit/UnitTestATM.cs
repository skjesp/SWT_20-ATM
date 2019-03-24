using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;

namespace SWT_20_ATM.Test.Unit
{
    [TestClass]
    public class UnitTestATM
    {
        private ILogger _logger;
        private IAirspace _airspace;

        [SetUp]
        public void init()
        {
            _logger = NSubstitute.Substitute.For<ILogger>();
            _airspace = NSubstitute.Substitute.For<IAirspace>();


            // Air Traffic Monitor
            ATM atm = new ATM( _airspace, 300, 5000 );
        }

        [TestCase()]
        public void test_UpdatePlaneList_calls_airspaceWithCorrectArgs()
        {


            _airspace.IsWithinArea( Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>() ).Returns( true );

        }
    }
}
