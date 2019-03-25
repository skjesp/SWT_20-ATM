using NUnit.Framework;
using System.IO;
namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestFileLogger
    {

        private FileLogger uut;

        [SetUp]
        public void init()
        {
            uut = new FileLogger();
        }
        [TestCase( @"./SepLog.txt" )]
        public void ReceivePath_Constructor( string x )
        {
            uut = new FileLogger( x );
            
        // Todo: Mextline needs a fix
            Assert.That( uut.FilePath, Is.EqualTo( x ) );
        }

        //// Maybe not necessary.
        //*void NoPathSpec_Constructor()
        //{
        //    uut = new FileLogger();
        //    Assert.That(uut._filePath, IsEqualTo("../SepLog.txt"));
        //}*/

        /*[TestCase( "Test String 1" )]
        [TestCase( "Test String 2" )]
        public void AddToLog_Test( string x )
        {
            uut.AddToLog( x );
            string text = File.ReadAllText( "./SepLog.txt" );
            // Todo: Mextline needs a fix
           Assert.That( text, Is.EqualTo( x ) );
        }*/

    }
}
