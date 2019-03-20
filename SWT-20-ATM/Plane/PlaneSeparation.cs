using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWT_20_ATM
{
    public class PlaneSeparation
    {
        private int _minVerticalDistance = 0;
        private int _minHorizontalDistance = 0;
        

        public PlaneSeparation(int h, int v)
        {
            SetDistance(h,v);
        }

        public void SetDistance(int h, int v)
        {
            _minHorizontalDistance = h;
            _minVerticalDistance = v;
        }

        public List<List<Plane>> CheckPlanes(List<Plane> planeList)
        {
            List<List<Plane>> violatingPlanes = new List<List<Plane>>();

            foreach (var Plane in planeList)
            {
                foreach (var comparePlane in planeList)
                {
                    if (Plane.Tag == comparePlane.Tag) continue;    // Do nothing if plane being compared is the same

                    double horizontalDifference = Calculator.GetDistance(Plane.xCoordinate, Plane.yCoordinate, comparePlane.xCoordinate, comparePlane.yCoordinate);
                    int verticalDifference = Math.Abs(Plane.altitude - comparePlane.altitude);

                    if (horizontalDifference >= _minHorizontalDistance) continue;   // Do nothing if horizontal difference is large enough
                    if (verticalDifference   >= _minVerticalDistance)   continue;   // Do nothing if vertical   difference is large enough

                    // Create plane pair
                    List<Plane> violatingPlanePair = new List<Plane>
                    {
                        Plane,
                        comparePlane
                    };

                    violatingPlanes.Add(violatingPlanePair);

                }
            }
            return violatingPlanes;
        }
    }
}