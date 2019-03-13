﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace SWT_20_ATM
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;
        public List<string> TransponderDataList;

        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            //System.Console.WriteLine("New data");
            foreach (var data in e.TransponderData)
            {
                //TransponderDataList.Add(e);
                System.Console.WriteLine($"Transponderdata {data}");
            }
        }
    }
}
