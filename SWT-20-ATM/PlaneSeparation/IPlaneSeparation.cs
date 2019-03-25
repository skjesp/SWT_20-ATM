﻿using System.Collections.Generic;

namespace SWT_20_ATM
{
    interface IPlaneSeparation
    {
        void SetDistance( int h, int v );
        List<List<IPlane>> CheckPlanes( List<IPlane> planeList );
    }
}
