using System;

namespace SWT_20_ATM
{
    public class Separation
    {

        public string Planetag1;
        public string Planetag2;
        public DateTime date;

        public Separation(string Tag1, string Tag2, DateTime time)
        {
            Planetag1 = Tag1;
            Planetag2 = Tag2;
            date = time;

        }

        public void Write()
        {
            Console.WriteLine("Planes " + Planetag1 + " and " + Planetag2 + " are flying too close at " + date);
        }
    }
}