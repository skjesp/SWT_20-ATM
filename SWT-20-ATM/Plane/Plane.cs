using System;

namespace SWT_20_ATM
{
    public class Plane : IPlane
    {
        public Plane(string tag, int xCor, int yCor, int altitude, DateTime time)
        {
            _tag = tag;
            _xCoordinate = xCor;
            _yCoordinate = yCor;
            _altitude = altitude;
            _lastUpdate = time;
        }

        private string _tag;
        public string Tag => _tag;

        private int _xCoordinate;
        public int XCoordinate => _xCoordinate;

        private int _yCoordinate;
        public int YCoordinate => _yCoordinate;

        private int _altitude;
        public int Altitude => _altitude;

        private double _speed;
        public double Speed => _speed;

        private DateTime _lastUpdate;
        public DateTime LastUpdate => _lastUpdate;


        private double _direction;
        public double Direction => _direction;




        public bool Update(IPlane oldPlane)
        {
            // Do not update if tags doesn't match
            if ( this.Tag != oldPlane.Tag )
            {
                return false;
            }

            // Can't update with a old plane record
            if ( this.LastUpdate < oldPlane.LastUpdate )
            {
                return false;
            }

            // Calculate direction
            _direction = Calculator.GetDirection2D(oldPlane.XCoordinate, oldPlane.YCoordinate, XCoordinate, YCoordinate);

            try
            {
                // Calculate speed
                _speed = Calculator.GetSpeed(oldPlane.XCoordinate, oldPlane.YCoordinate, oldPlane.LastUpdate,
                                            XCoordinate, YCoordinate, LastUpdate
                                            );
            }
            catch ( Exception e )
            {
                //Console.WriteLine( e );
                return false;
            }

            _lastUpdate = oldPlane.LastUpdate;
            return true;
        }
    }
}