using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    public class Calculator
    {
        public double getDirection2D(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            double radians = Math.Atan2(dy, dx);
            double degrees = radians * 180 / Math.PI;
            degrees += 180;
            return degrees;
        }
    }
}
