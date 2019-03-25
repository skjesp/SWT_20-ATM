using System;
using System.IO;
namespace SWT_20_ATM
{
    public class FileLogger : ILogger
    {
        public StringWriter Writer { get; set; }

        public bool UnderTest { get; set; }

        public FileLogger(string filePath = "./SepLog.txt", StringWriter writer = null, bool underTest = false)
        {
            _filePath = filePath;
            UnderTest = underTest;

            if ( writer == null ) Writer = new StringWriter();
        }


        public bool AddToLog(string message)
        {
            var output = string.Format( "{0:YYY:HH:mm:ss}: {1}", DateTime.Now,  message);

            if ( UnderTest )
            {
                Stream ourStream = File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Writer.WriteLine(output, ourStream);

            }
            else
            {
                String myString = "";
                Writer.WriteLine(output, myString);
            }

            Writer.Flush();

            //Console.WriteLine("Logged seperation event to file: {0}", DateTime.Now);
            return true;
        }

        string _filePath;
        public string FilePath => _filePath;
    }
}