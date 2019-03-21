using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class Decoder : IDecoder
    {
        public delegate void NewPlaneListEvent(List<Plane> planeList);
        public event NewPlaneListEvent NewPlanesEvent;

        public List<Plane> OldPlaneList;

        public Decoder()
        {
            OldPlaneList = new List<Plane>();
        }

        public void Decode(List<string> newData)
        {
            List<Plane> newPlaneList = new List<Plane>();
            
            foreach (var data in newData)
            {
                string[] result = data.Split(';');

                DateTime tempDateTime = DateTime.ParseExact(result[4], "yyyyMMddHHmmssfff", null);

                Plane tempPlane = new Plane(result[0],int.Parse(result[1]), int.Parse(result[2]), int.Parse(result[3]), tempDateTime);

                newPlaneList.Add(tempPlane);
            }

            // Update the speed and direction of the new planes found
            foreach (var oldPlane in OldPlaneList)
            {
                foreach (var newPlane in newPlaneList)
                {
                    if (newPlane.Tag == oldPlane.Tag)
                    {
                        newPlane.Update(oldPlane);      // Update plane found in record
                        break;                          // No reason to loop rest of planes
                    }
                }
            }

            
            // Update oldPlaneList with new planes
            OldPlaneList = newPlaneList;

            // Get all planes that got all properties
            List<Plane> completePlaneList = GetCompletePlanes(newPlaneList);

            // Call event
            NewPlanesEvent?.Invoke(completePlaneList);
        }

        public List<Plane> GetCompletePlanes(List<Plane> planeList)
        {
            List<Plane> completePlaneList = new List<Plane>();  // List that will contain all complete planes

            foreach (var plane in planeList)
            {
                if (double.IsNaN(plane.speed)) continue;        // Do not add plane if speed is NaN
                if (double.IsNaN(plane.direction)) continue;    // Do not add plane if direction is NaN

                completePlaneList.Add(plane);                   // Add plane to list that will be returned
            }

            return completePlaneList;
        }
    }

}