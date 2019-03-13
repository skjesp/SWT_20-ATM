using System;

namespace SWT_20_ATM
{
    public class FileLogger : ILogger
    {
        public void AddToLog(Separation SepToLog)
        {
            SepToLog.Write();
        }
    }
}