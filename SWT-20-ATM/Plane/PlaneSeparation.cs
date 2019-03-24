using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class PlaneSeparation
    {
        private int _minVerticalDistance = 0;
        private int _minHorizontalDistance = 0;


        public PlaneSeparation( int h, int v )
        {
            SetDistance( h, v );
        }

        public void SetDistance( int h, int v )
        {
            _minHorizontalDistance = h;
            _minVerticalDistance = v;
        }

        public List<List<IPlane>> CheckPlanes( List<IPlane> planeList )
        {
            List<List<IPlane>> violatingPlanes = new List<List<IPlane>>();

            foreach ( var Plane in planeList )
            {
                foreach ( var comparePlane in planeList )
                {
                    if ( Plane.Tag == comparePlane.Tag )
                    {
                        continue;    // Do nothing if plane being compared is the same
                    }

                    double horizontalDifference = Calculator.GetDistance( Plane.XCoordinate, Plane.YCoordinate, comparePlane.XCoordinate, comparePlane.YCoordinate );
                    int verticalDifference = Math.Abs( Plane.Altitude - comparePlane.Altitude );

                    if ( horizontalDifference >= _minHorizontalDistance )
                    {
                        continue;   // Do nothing if horizontal difference is large enough
                    }

                    if ( verticalDifference >= _minVerticalDistance )
                    {
                        continue;   // Do nothing if vertical   difference is large enough
                    }

                    // Create plane pair
                    List<IPlane> violatingPlanePair = new List<IPlane>
                    {
                        Plane,
                        comparePlane
                    };

                    violatingPlanes.Add( violatingPlanePair );

                }
            }
            return violatingPlanes;
        }
    }
}