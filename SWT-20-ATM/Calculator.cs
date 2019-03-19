using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    public class Calculator : System.Exception
    {

        public double GetDirection2D(int x1, int y1, int x2, int y2)
        {
            int dx = x1 - y2;
            int dy = x2 - y1;
            double radians = Math.Atan2(dy, dx);
            double degrees = radians * 180 / (Math.PI);
            degrees += 180;
            degrees = 360 - degrees;

            return degrees;
        }

        public double GetDistance(int x1, int y1, int x2, int y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;

            // Calculate distance between coordinates (Meters)
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public double GetSpeed(int x1, int y1, DateTime date1, int x2, int y2, DateTime date2)
        {
            // Get seconds difference between dates
            double secondDif = date2.Subtract(date1).TotalSeconds;

            if (secondDif == 0.0) throw new System.ArgumentException("Time Difference can't be 0", "date1, date2");

            // Calculate distance between coordinates (Meters)
            double distance = this.GetDistance(x1, y1, x2, y2);

            // Calculate traveling speed unit (Meters / second)
            double speed = Math.Abs(distance / secondDif);

            return speed;
        }

        
    }
}
