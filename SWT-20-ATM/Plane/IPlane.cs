using System;

namespace SWT_20_ATM
{
    public interface IPlane
    {
        bool Update( IPlane oldPlane );
        string Tag { get; }
        int XCoordinate { get; }
        int YCoordinate { get; }
        int Altitude { get; }
        double Speed { get; }
        double Direction { get; }
        DateTime LastUpdate { get; }
    }
}
