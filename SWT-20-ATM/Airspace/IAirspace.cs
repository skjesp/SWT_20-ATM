namespace SWT_20_ATM
{
    public interface IAirspace
    {
        bool IsWithinArea( int x, int y, int z );
        void AddShape( IShape shape );
    }
}
