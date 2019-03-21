using System;

namespace SWT_20_ATM
{
    public class Plane
    {
        public Plane(string _tag, int _xCor, int _yCor, int _altitude, DateTime _time)
        {
            Tag = _tag;
            xCoordinate = _xCor;
            yCoordinate = _yCor;
            altitude = _altitude;
            lastUpdate = _time;
        }
        public string Tag;
        public int xCoordinate;
        public int yCoordinate;
        public int altitude;
        public double speed;
        public double direction;
        public DateTime lastUpdate;

        public bool Update(Plane oldPlane)
        {
            // Do not update if tags doesn't match
            if (this.Tag != oldPlane.Tag) return false;

            // Can't update with a old plane record
            if (this.lastUpdate < oldPlane.lastUpdate) return false;
            
            // Calculate direction
            direction = Calculator.GetDirection2D(oldPlane.xCoordinate, oldPlane.yCoordinate, xCoordinate, yCoordinate);

            try
            {
                // Calculate speed
                speed = Calculator.GetSpeed(oldPlane.xCoordinate, oldPlane.yCoordinate, oldPlane.lastUpdate,
                                            xCoordinate, yCoordinate, lastUpdate
                                            );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            lastUpdate = oldPlane.lastUpdate;
            return true;
        }
    }
}