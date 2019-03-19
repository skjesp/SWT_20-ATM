using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWT_20_ATM
{
    public class PlaneSeparation
    {
        private int _MinVerticalDistance = 0;
        private int _MinHorizontalDistance = 0;

        Calculator Calc = new Calculator();

        public PlaneSeparation(int H, int V)
        {
            SetDistance(H,V);
        }

        public void SetDistance(int H, int V)
        {
            _MinHorizontalDistance = H;
            _MinVerticalDistance = V;
        }

        public List<List<Plane>> CheckPlanes(List<Plane> PlaneList)
        {
            List<List<Plane>> ViolatingPlanes = new List<List<Plane>>();

            foreach (var Plane in PlaneList)
            {
                int XCoordinateToCompare = Plane.xCoordinate;
                int YCoordinateToCompare = Plane.yCoordinate;

                foreach (var ComparePlane in PlaneList)
                {
                    if (Plane.Tag != ComparePlane.Tag)
                    {
                        double HorizontalDifference = Calc.GetDistance(Plane.xCoordinate, Plane.yCoordinate, ComparePlane.xCoordinate, ComparePlane.yCoordinate);

                        int VerticalDifference = Math.Abs(Plane.altitude - ComparePlane.altitude);

                        //Check if there is a violation.
                        if ((HorizontalDifference < _MinHorizontalDistance) && (VerticalDifference < _MinVerticalDistance))
                        {
                            List<Plane> ViolatingPlanePair = new List<Plane>();
                            ViolatingPlanePair.Add(Plane);
                            ViolatingPlanePair.Add(ComparePlane);
                            ViolatingPlanes.Add(ViolatingPlanePair);
                        }
                    }
                    
                }
            }
            return ViolatingPlanes;
        }
    }
}