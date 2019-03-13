using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class SeparationChecker
    {
        private ILogger myLogger;
        private List<Separation> LastSeparations = new List<Separation>();

        public SeparationChecker(ILogger logger)
        {
            myLogger = logger;
        }

        public List<Separation> CheckForSeparations(List<Plane> PlaneList)
        {

            List<Separation> CurrentSeparations = new List<Separation>();
            
            foreach (var iPlane in PlaneList)
            {
                for (int i = PlaneList.IndexOf(iPlane)+1; i < (PlaneList.Count); i++)
                {
                    int DistanceHeight = Math.Abs(iPlane.altitude - PlaneList[i].altitude);

                    double distx = iPlane.xCoordinate-PlaneList[i].xCoordinate;
                    double disty = iPlane.yCoordinate - PlaneList[i].yCoordinate;
                    double DistanceHorizontal = Math.Sqrt((distx * distx) + (disty * disty));
                    
                    if (DistanceHeight < 300 && DistanceHorizontal < 5000)
                    {
                        
                        CurrentSeparations.Add(new Separation(iPlane.Tag, PlaneList[i].Tag, iPlane.lastUpdate));
                    }    
                }
            }

            CheckForNew(CurrentSeparations);

            return CurrentSeparations;
        }

        private void CheckForNew(List<Separation> newList)
        {
            
            foreach (var i in newList)
            {
                
                bool isnew = true;

                foreach (var sep in LastSeparations)
                {
                    
                    if ((((i.Planetag1 == sep.Planetag1) || (i.Planetag1 == sep.Planetag2)) && ((i.Planetag2 == sep.Planetag1) || (i.Planetag2 == sep.Planetag2))))
                    {

                        isnew = false;
                    }
                }

                if (isnew)
                {
                    myLogger.AddToLog(i);
                }

            }

            LastSeparations = newList;

        }
    }
}