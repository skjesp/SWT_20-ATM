using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class PlaneSeparation : IPlaneSeparation
    {
        public int MinVerticalDistance { get; private set; }
        public int MinHorizontalDistance { get; private set; }


        public PlaneSeparation( int h, int v )
        {
            SetDistance( h, v );
        }

        public void SetDistance( int h, int v )
        {
            MinHorizontalDistance = h;
            MinVerticalDistance = v;
        }

        public List<List<IPlane>> CheckPlanes( List<IPlane> planeList )
        {
            List<List<IPlane>> violatingPlanes = new List<List<IPlane>>();

            foreach ( var plane in planeList )
            {
                for ( int i = planeList.IndexOf( plane ) + 1; i < ( planeList.Count ); i++ )
                {
                    var comparePlane = planeList[ i ];
                    if ( plane.Tag == comparePlane.Tag )
                    {
                        continue;    // Do nothing if plane being compared is the same
                    }

                    double horizontalDifference = Calculator.GetDistance( plane.XCoordinate, plane.YCoordinate, comparePlane.XCoordinate, comparePlane.YCoordinate );
                    int verticalDifference = Math.Abs( plane.Altitude - comparePlane.Altitude );

                    if ( horizontalDifference >= MinHorizontalDistance )
                    {
                        continue;   // Do nothing if horizontal difference is large enough
                    }

                    if ( verticalDifference >= MinVerticalDistance )
                    {
                        continue;   // Do nothing if vertical   difference is large enough
                    }

                    // Create plane pair
                    List<IPlane> violatingPlanePair = new List<IPlane>
                    {
                        plane,
                        comparePlane
                    };

                    violatingPlanes.Add( violatingPlanePair );
                }
            }
            return violatingPlanes;
        }
    }
}