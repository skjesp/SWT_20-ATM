using System.Collections.Generic;

namespace SWT_20_ATM
{
    public interface IRendition
    {
        void RenderPlanes( List<IPlane> planeList );
        void RenderViolations( List<List<IPlane>> offenderPlanes, string violation = "Seperation" );
    }
}