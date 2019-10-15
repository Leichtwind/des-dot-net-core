using System.Collections.Generic;
using System.Linq;
using IBinaryEnumerable = System.Collections.Generic.IEnumerable<bool>;

namespace DES_.Net_Core
{
    public static class SubKeyGenerator
    {
        public static IEnumerable<IBinaryEnumerable> GenerateSubKeys(string key)
        {
            var permuted = key.ToBinaryEnumerable().Permute(AlgorithmConstants.PC1).ToList();

            var c = new List<IBinaryEnumerable>
            {
                permuted.Take(28)
            };

            var d = new List<IBinaryEnumerable>
            {
                permuted.Skip(28).Take(28)
            };

            for (var i = 0; i < 16; i++)
            {
                c.Add(c[i].ShiftLeftCircular(AlgorithmConstants.SHIFTS[i]));
                d.Add(d[i].ShiftLeftCircular(AlgorithmConstants.SHIFTS[i]));
            }

            var keys = c.Zip(d).Skip(1).Select(tuple => tuple.First.Concat(tuple.Second));

            return keys.Select(list => list.Permute(AlgorithmConstants.PC2));
        }
    }
}