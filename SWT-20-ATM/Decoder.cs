using System;
using System.Collections.Generic;

namespace SWT_20_ATM
{
    public class Decoder : IDecoder
    {
        private List<Plane> decoderList;
        public void Decode(List<string> newData)
        {
            decoderList.Clear();
            foreach (var data in newData)
            {
                string[] result = data.Split(';');

                DateTime tempDateTime = DateTime.ParseExact(result[4], "yyyyMMddHHmmssfff", null);

                Plane tempPlane = new Plane(result[0],int.Parse(result[1]), int.Parse(result[2]), int.Parse(result[3]), tempDateTime);
                
                decoderList.Add(tempPlane);

            }
        }
    }
}