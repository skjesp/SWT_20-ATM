using System.Collections.Generic;

namespace SWT_20_ATM
{
    public interface IDecoder
    {
        void Decode(List<string> newData);
    }
}