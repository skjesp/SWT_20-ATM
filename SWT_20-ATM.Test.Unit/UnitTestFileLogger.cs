using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestFileLogger
    {

        private FileLogger _uut;

        [SetUp]
        public void init()
        {
            _uut = new FileLogger();
        }

        [TestCase(@"./SepLog.txt")]
        public void ReceivePath_Constructor(string x)
        {
            Assert.That(_uut.FilePath, Is.EqualTo(x));
        }

        [TestCase("")]
        [TestCase("Test String 2")]
        public void AddToLog_Test(string message)
        {
            _uut.Writer = Substitute.For<StringWriter>();
            _uut.AddToLog(message);

            var expectedMessage = string.Format( "{0:YYY:HH:mm:ss}: {1}", DateTime.Now,  message);

            // Get all _loggers Received calls from AddToLog where string argument matched 
            // Code example found at: https://stackoverflow.com/questions/52439697/how-to-check-any-of-multiple-overloads-called-nsubstitute
            // Answer posted by: David Tchepak sep 22'18
            var calls = _uut.Writer.ReceivedCalls()
                .Where( x => x.GetMethodInfo().Name == nameof( _uut.Writer.WriteLine ) )
                .Where( x => ( (string) x.GetArguments()[0] )
                .Contains( expectedMessage ) );

            // Check if number of expected arguments was found
            Assert.AreEqual(1, calls.Count());
        }

    }
}
