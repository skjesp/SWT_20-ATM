using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWT_20_ATM
{
    public interface IRendition
    {
        void RenderPlanes(List<Plane> planeList);
        void RenderViolations(List<Plane> offenderPlanes, string violation = "Seperation");
    }
}