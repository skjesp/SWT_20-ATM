using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class ATM
    {
        private IAirspace _observableAirspace;

        public List<IPlane> PlaneList { get; private set; }

        public List<List<IPlane>> ConditionViolationSeparation { get; private set; }

        public ILogger Logger { get; set; }

        // Rules
        private PlaneSeparation _planeSeparator;

        public ATM( IAirspace observableAirspace, int minVerticalDif, int minHorizontalDif )
        {
            _observableAirspace = observableAirspace;
            PlaneList = new List<IPlane>();
            _planeSeparator = new PlaneSeparation( minHorizontalDif, minVerticalDif );

            ConditionViolationSeparation = new List<List<IPlane>>();
        }

        public void UpdatePlaneList( List<IPlane> newPlaneList )
        {

            List<IPlane> updatedPlaneList = new List<IPlane>();

            // Only select planes present in ObservableAirspace
            foreach ( var plane in newPlaneList )
            {
                // Check if plane is within airspace
                bool planeInAirspace = _observableAirspace.IsWithinArea( plane.XCoordinate, plane.YCoordinate, plane.Altitude );
                PlaneList.Add( plane );
                // Add plane to list if it's within the airspace
                if ( planeInAirspace )
                {
                    updatedPlaneList.Add( plane );

                }
            }


            UpdateViolatingPlanes( updatedPlaneList );  // Update violating planes

            PlaneList = updatedPlaneList;
        }

        private void UpdateViolatingPlanes( List<IPlane> updatedPlaneList )
        {
            // Check for violations
            List<List<IPlane>> newViolatingPlaneList = _planeSeparator.CheckPlanes( updatedPlaneList );


            foreach ( var newPlanePair in newViolatingPlaneList )
            {
                // If new plane-pair exist in old planelist then do nothing
                if ( ConditionViolationSeparation.Contains( newPlanePair ) )
                {
                    continue;
                }

                // Create log message
                string msgToLog = string.Format( "{0:YYY:HH:mm:ss}: {1} and {2} Violates Separation condition!", DateTime.Now, newPlanePair[0].Tag, newPlanePair[1].Tag );

                // Write log message to log
                Logger?.AddToLog( msgToLog );
            }

            foreach ( var oldPlanePair in ConditionViolationSeparation )
            {
                // If old plane-pair exist in new planelist then do nothing
                if ( newViolatingPlaneList.Contains( oldPlanePair ) )
                {
                    continue;
                }
                // Create log message
                string msgToLog = string.Format( "{0:YYY:HH:mm:ss}: {1} and {2} Violates Separation condition!", DateTime.Now, oldPlanePair[0].Tag, oldPlanePair[1].Tag );

                // Write log message to log
                Logger?.AddToLog( msgToLog );
            }

            // Update ConditionViolation_Separation to contain new errors 
            ConditionViolationSeparation.Clear();
            ConditionViolationSeparation = newViolatingPlaneList;
        }

    }
}
