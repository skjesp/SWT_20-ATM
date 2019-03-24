using System.Threading;
using TransponderReceiver;


namespace SWT_20_ATM
{
    class Program
    {
        static void Main( string[] args )
        {

            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new TransponderReceiverClient( receiver );

            // Decoder that decodes strings into plane objects
            Decoder myDecoder = new Decoder();

            // Make decoder listen to system events
            system.NewPlanesEvent += myDecoder.Decode;

            // Create used airspace
            IAirspace airspace = new Airspace();

            // Add area to airspace
            airspace.AddShape( new Cuboid( 0, 0, 500, 80000, 80000, 20000 ) );

            // Air Traffic Monitor
            ATM atm = new ATM( airspace, 300, 5000 );

            myDecoder.NewPlanesEvent += atm.UpdatePlaneList;


            // Let the real TDR execute in the background
            while ( true )
            {
                Thread.Sleep( 1000 );
            }
        }
    }
}
