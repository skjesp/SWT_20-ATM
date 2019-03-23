using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWT_20_ATM
{
    public class Konsol : IRendition
    {
        public void RenderPlanes(List<Plane> planeList)
        {
            foreach (Plane iPlane in planeList)
            {
                if (iPlane.speed != 0)
                {
                    Console.WriteLine(iPlane.Tag + ": Coordinates x-y: " + iPlane.xCoordinate + "-" + iPlane.yCoordinate
                                      + " Altitude: " + iPlane.altitude + " Velocity: " + iPlane.speed + " Compass course: " + iPlane.direction);
                }
                else
                {
                    Console.WriteLine(iPlane.Tag + ": Coordinates x-y: " + iPlane.xCoordinate + "-" + iPlane.yCoordinate
                                      + " Altitude: " + iPlane.altitude);
                }
            }
        }

        public void RenderViolations(List<Plane> offenderPlanes, string violation = "Separation")
        {
            if (offenderPlanes.Count == 1)
            {
                Console.Write("The Plane " + offenderPlanes[0].Tag);
            }
            else
            {
                //Console.Write("The Planes " + offenderPlanes[0].Tag);,
                Console.Write("The Planes ");
                foreach (Plane iPlane in offenderPlanes)
                {
                    if (offenderPlanes.Last() == iPlane)
                    {
                        Console.Write(iPlane.Tag);
                    }
                    else
                    {
                        Console.Write(iPlane.Tag + ", ");
                    }
                    
                    
                }
            }

            Console.WriteLine(" has violated the " + violation + " rule.");
        }
    }
}