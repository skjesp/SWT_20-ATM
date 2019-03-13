using System;

namespace SWT_20_ATM
{
    public class Separation
    {

        private string Planetag1;
        private string Planetag2;
        private DateTime date;

        public void Write()
        {
            Console.WriteLine("Planes " + Planetag1 + " and " + Planetag2 + " are flying too close at " + date);
        }
    }
}