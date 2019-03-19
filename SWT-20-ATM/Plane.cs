using System;

namespace SWT_20_ATM
{
    public class Plane : Calculator
    {
        public Plane(string _tag, int _xCor, int _yCor, int _altitude, DateTime _time)
        {
            Tag = _tag;
            xCoordinate = _xCor;
            yCoordinate = _yCor;
            altitude = _altitude;
        }
        public string Tag;
        public int xCoordinate;
        public int yCoordinate;
        public int altitude;
        public double speed;
        public double direction;
        public DateTime lastUpdate;

        public bool Update(Plane newPlane)
        {
            // Do not update if tags doesn't match
            if (this.Tag != newPlane.Tag) return false;

            // Can't update with a old plane record
            if (this.lastUpdate > newPlane.lastUpdate) return false;

            // Calculate direction
            speed = GetDirection2D(xCoordinate, yCoordinate, newPlane.xCoordinate, newPlane.yCoordinate);
            
            // Calculate speed


            return true;
        }
    }
}