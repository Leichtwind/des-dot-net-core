using System;
using System.Linq;
using IBinaryEnumerable = System.Collections.Generic.IEnumerable<bool>;

namespace DES_.Net_Core
{
    public static class CharExtension
    {
        public static IBinaryEnumerable ToBinaryEnumerable(this char c) =>
            Convert
                .ToString(c, 2)
                .PadLeft(8, '0')
                .Select(bc => bc == '1');
    }
}