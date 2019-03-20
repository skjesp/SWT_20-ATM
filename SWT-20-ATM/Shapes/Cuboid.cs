using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    public class Cuboid : IShape
    {
        private int x_1;
        private int y_1;
        private int z_1;

        private int x_2;
        private int y_2;
        private int z_2;

        public Cuboid(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            x_1 = x1;
            y_1 = y1;
            z_1 = z1;

            x_2 = x2;
            y_2 = y2;
            z_2 = z2;
        }

        // Returns true : point is outside of shape
        // Returns false: point is inside shape
        public bool ContainsPoint(int x, int y, int z)
        {
            int maxX = (x_1 < x_2) ? x_2 : x_1;
            int minX = (x_1 > x_2) ? x_2 : x_1;

            int maxY = (y_1 < y_2) ? y_2 : y_1;
            int minY = (y_1 > y_2) ? y_2 : y_1;
             
            int maxZ = (z_1 < z_2) ? z_2 : z_1;
            int minZ = (z_1 > z_2) ? z_2 : z_1;

            // Check if point is within borders 
            if (x < minX || x > maxX) return false;
            if (y < minY || y > maxY) return false;
            if (z < minZ || z > maxZ) return false;

            return true;
        }
    }
}
