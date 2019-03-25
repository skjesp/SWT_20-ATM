using System;
using System.Collections.Generic;
using System.Linq;

namespace SWT_20_ATM
{
    public class Konsol : IRendition
    {
        // TODO: Future change to make it pretty
        //public void clearKonsol( int newLineCount = 10 )
        //{
        //    for ( int i = 0; i < newLineCount; i++ )
        //        Console.WriteLine( "" );
        //}

        public void RenderPlanes( List<IPlane> planeList )
        {
            //clearKonsol( 30 );
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

        public void RenderViolations( List<List<IPlane>> offenderPlanes, string violation = "Separation" )
        {
            foreach ( List<IPlane> Pair in offenderPlanes )
            {
                if ( Pair.Count == 1 )
                {
                    Console.Write( "The Plane " + Pair[ 0 ].Tag );
                }
                else
                {
                    //Console.Write("The Planes " + offenderPlanes[0].Tag);,
                    Console.Write( "The Planes " );
                    foreach ( IPlane Plane in Pair )
                    {
                        if ( Pair.Last() == Plane )
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
}