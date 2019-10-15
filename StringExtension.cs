using System.Linq;
using IBinaryEnumerable = System.Collections.Generic.IEnumerable<bool>;

namespace DES_.Net_Core
{
    public static class StringExtension
    {
        public static IBinaryEnumerable ToBinaryEnumerable(this string source) => 
            source.SelectMany(c => c.ToBinaryEnumerable());
    }
}