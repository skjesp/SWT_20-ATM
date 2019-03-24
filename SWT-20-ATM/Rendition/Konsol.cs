using System;
using System.Collections.Generic;
using System.Linq;

namespace SWT_20_ATM
{
    public class Konsol : IRendition
    {
        public void RenderPlanes( List<IPlane> planeList )
        {
            foreach ( IPlane iPlane in planeList )
            {
                if ( iPlane.Speed != 0 )
                {
                    Console.WriteLine( iPlane.Tag + ": Coordinates x-y: " + iPlane.XCoordinate + "-" + iPlane.YCoordinate
                                      + " Altitude: " + iPlane.Altitude + " Velocity: " + iPlane.Speed + " Compass course: " + iPlane.Direction );
                }
                else
                {
                    Console.WriteLine( iPlane.Tag + ": Coordinates x-y: " + iPlane.XCoordinate + "-" + iPlane.YCoordinate
                                      + " Altitude: " + iPlane.Altitude );
                }
            }
        }

        public void RenderViolations( List<IPlane> offenderPlanes, string violation = "Separation" )
        {
            if ( offenderPlanes.Count == 1 )
            {
                Console.Write( "The Plane " + offenderPlanes[0].Tag );
            }
            else
            {
                //Console.Write("The Planes " + offenderPlanes[0].Tag);,
                Console.Write( "The Planes " );
                foreach ( IPlane Plane in offenderPlanes )
                {
                    if ( offenderPlanes.Last() == Plane )
                    {
                        Console.Write( Plane.Tag );
                    }
                    else
                    {
                        Console.Write( Plane.Tag + ", " );
                    }


                }
            }

            Console.WriteLine( " has violated the " + violation + " rule." );
        }
    }
}