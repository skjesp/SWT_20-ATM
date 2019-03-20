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
        private List<List<Plane>> ConditionViolation_Separation;

        // Rules
        private PlaneSeparation PlaneSeparator;

        public ATM(Airspace observableAirspace, int minVerticalDif, int minHorizontalDif)
        {
            ObservableAirspace = observableAirspace;
            PlaneSeparator = new PlaneSeparation(minHorizontalDif, minVerticalDif);

            ConditionViolation_Separation = new List<List<Plane>>();
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


            UpdateViolatingPlanes(updatedPlaneList);  // Update violating planes
        }

        private void UpdateViolatingPlanes(List<Plane> updatedPlaneList)
        {
            // Check for violations
            List<List<Plane>> newViolatingPlaneList = PlaneSeparator.CheckPlanes(updatedPlaneList);


            foreach (var newPlanePair in newViolatingPlaneList)
            {
                // If new plane-pair exist in old planelist then do nothing
                if (ConditionViolation_Separation.Contains(newPlanePair)) continue;
                // Todo: Write to log
            }

            foreach (var oldPlanePair in ConditionViolation_Separation)
            {
                // If old plane-pair exist in new planelist then do nothing
                if (newViolatingPlaneList.Contains(oldPlanePair)) continue;
                // Todo: Write to log
            }

            // Update ConditionViolation_Separation to contain new errors 
            ConditionViolation_Separation.Clear();
            ConditionViolation_Separation = newViolatingPlaneList;
        }

    }
}
