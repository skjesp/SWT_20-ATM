using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    public class ATM
    {
        private Airspace ObservableAirspace;
        private List<Plane> PlaneList;
        private List<Plane> ConditionViolation_Separation;

        public ATM(Airspace observableAirspace)
        {
            ObservableAirspace = observableAirspace;
        }

        public void updatePlaneList(List<Plane> NewPlaneList)
        {
            List<Plane> updatedPlaneList = new List<Plane>();

            // Only select planes present in ObservableAirspace
            foreach (var plane in NewPlaneList)
            {
                // Check if plane is within airspace
                bool planeInAirspace = ObservableAirspace.IsWithinArea(plane.xCoordinate, plane.yCoordinate, plane.altitude);

                // Add plane to list if it's within the airspace
                if (planeInAirspace) updatedPlaneList.Add(plane);
            }


            // Check for violations
        }

    }
}
