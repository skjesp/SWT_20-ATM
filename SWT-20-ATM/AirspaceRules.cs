using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    public class AirspaceRules : IAirspaceRules
    {
        //TODO: Make restrictions on set
        private int _MaxVerticalDistance;

        public int MaxVerticalDistance
        {
            private set { _MaxVerticalDistance = value;}
            get { return _MaxVerticalDistance; }
        }
        public int MaxHorizontalDistance { private set; get; }

        public void SetDistanceRule(int distance, int height)
        {
            MaxVerticalDistance = distance;
            MaxHorizontalDistance = height;
        }
    }
}