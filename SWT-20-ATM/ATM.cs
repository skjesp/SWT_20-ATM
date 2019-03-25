using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class ATM
    {
        public IAirspace ObservableAirspace { get; private set; }       // Airspace to observe
        public ILogger Logger { get; private set; }                     // Logger to log to
        public IPlaneSeparation PlaneSeparator { get; private set; }    // PlaneSeparation Condition
        public IRendition RenditionOutputter { get; private set; }      // Object to render output

        public List<IPlane> PlaneList { get; private set; }                             // List to store current planes in   
        public List<List<IPlane>> ConditionViolationSeparation { get; private set; }    // List to store violating planes


        public ATM( IAirspace observableAirspace,
                    IPlaneSeparation planeSeparator,
                    IRendition renditionOutputter,
                    ILogger logger = null )
        {
            ObservableAirspace = observableAirspace;
            PlaneSeparator = planeSeparator;
            Logger = logger;
            RenditionOutputter = renditionOutputter;

            PlaneList = new List<IPlane>();
            ConditionViolationSeparation = new List<List<IPlane>>();
        }

        public void UpdatePlaneList( List<IPlane> newPlaneList )
        {

            List<IPlane> updatedPlaneList = new List<IPlane>();

            // Only select planes present in ObservableAirspace
            foreach ( var plane in newPlaneList )
            {
                // Check if plane is within airspace
                bool planeInAirspace = ObservableAirspace.IsWithinArea( plane.XCoordinate, plane.YCoordinate, plane.Altitude );
                PlaneList.Add( plane );
                // Add plane to list if it's within the airspace
                if ( planeInAirspace )
                {
                    updatedPlaneList.Add( plane );

                }
            }


            UpdateViolatingPlanes( updatedPlaneList );  // Update violating planes

            PlaneList = updatedPlaneList;

            RenditionOutputter.RenderPlanes( PlaneList );
        }

        private void UpdateViolatingPlanes( List<IPlane> updatedPlaneList )
        {
            // Check for violations
            List<List<IPlane>> newViolatingPlaneList = PlaneSeparator.CheckPlanes( updatedPlaneList );


            foreach ( var newPlanePair in newViolatingPlaneList )
            {
                // If new plane-pair exist in old planelist then do nothing
                if ( ConditionViolationSeparation.Contains( newPlanePair ) )
                {
                    continue;
                }

                // Create log message
                string msgToLog = string.Format( "{0:YYY:HH:mm:ss}: {1} and {2} Violates Separation condition!", DateTime.Now, newPlanePair[ 0 ].Tag, newPlanePair[ 1 ].Tag );

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
                string msgToLog = string.Format( "{0:YYY:HH:mm:ss}: {1} and {2} no longer violates Separation condition!", DateTime.Now, oldPlanePair[ 0 ].Tag, oldPlanePair[ 1 ].Tag );

                // Write log message to log
                Logger?.AddToLog( msgToLog );
            }

            // Update ConditionViolation_Separation to contain new errors 
            ConditionViolationSeparation.Clear();
            ConditionViolationSeparation = newViolatingPlaneList;

            RenditionOutputter.RenderViolations( ConditionViolationSeparation );
        }

    }
}
