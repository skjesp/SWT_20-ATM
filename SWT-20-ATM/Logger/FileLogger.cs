using System;
using System.IO;
namespace SWT_20_ATM
{
    public class FileLogger : ILogger
    {
        public FileLogger(string filePath)
        {
            if (filePath == null)
            {
                _filePath = "../SepLog.txt";
                File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            else
            {
                _filePath = filePath;
                File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }

        }
        public void AddToLog(PlaneSeparation SepToLog)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine(_filePath, SepToLog);
            sw.Flush();
        }

        string _filePath;
    }
}