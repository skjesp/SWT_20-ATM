using System.Collections.Generic;


namespace SWT_20_ATM
{
    public class Airspace : IAirspace
    {
        private List<IShape> _shapeList = new List<IShape>();

        public bool IsWithinArea( int x, int y, int z )
        {
            // If no shapes are found, no point can be within it
            if ( _shapeList.Count == 0 )
            {
                return false;
            }

            // Check if point is within any of the given shapes
            foreach ( var shape in _shapeList )
            {
                if ( shape.ContainsPoint( x, y, z ) )
                {
                    return true;
                }
            }

            return false;
        }

        public void AddShape( IShape shape )
        {
            _shapeList.Add( shape );
        }
    }
}
