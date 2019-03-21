using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace SWT_20_ATM
{
    public class TransponderReceiverClient
    {
        public delegate void NewPlaneEvent(List<string> planeList);
        public event NewPlaneEvent NewPlanesEvent;

        private ITransponderReceiver receiver;
        public List<string> TransponderDataList;

        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver)
        {

            TransponderDataList = new List<string>();
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            TransponderDataList.Clear();
            // Just display data
            //System.Console.WriteLine("New data");
            foreach (var data in e.TransponderData)
            {
                TransponderDataList.Add(data);
                System.Console.WriteLine($"Transponderdata {data}");
            }

            NewPlanesEvent(TransponderDataList);
            Console.WriteLine("");

        }
    }
}
