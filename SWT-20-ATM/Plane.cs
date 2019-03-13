using System;

namespace SWT_20_ATM
{
    public class Plane
    {
        public Plane(string tag, int xCor, int yCor, int alti, DateTime time)
        {
            Tag = tag;
            xCoordinate = xCor;
            yCoordinate = yCor;
            altitude = alti;
        }
        public string Tag;
        public int xCoordinate;
        public int yCoordinate;
        public int altitude;
        public float speed;
        public float direction;
        public DateTime lastUpdate;
    }
}