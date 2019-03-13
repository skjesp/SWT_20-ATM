using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_20_ATM
{
    class AirspaceRules : IAirspaceRules
    {
        //TODO: Make restrictions on set
        public int MaxVerticalDistance { private set; get; }
        public int MaxHorizontalDistance { private set; get; }

        public void SetDistanceRule(int distance, int height)
        {
            MaxVerticalDistance = distance;
            MaxHorizontalDistance = height;
        }
    }
}