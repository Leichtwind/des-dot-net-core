using System;
using System.Collections.Generic;
using System.Linq;
using IBinaryEnumerable = System.Collections.Generic.IEnumerable<bool>;

namespace DES_.Net_Core
{
    public static class Coder
    {
        public static IBinaryEnumerable Encode(string key, string message)
        {
            var subKeys = SubKeyGenerator.GenerateSubKeys(key);

            return ImposeSubKeys(message.ToBinaryEnumerable(), subKeys);
        }

        public static IBinaryEnumerable Decode(string key, IBinaryEnumerable message)
        {
            var subKeys = SubKeyGenerator.GenerateSubKeys(key).Reverse();

            return ImposeSubKeys(message, subKeys);
        }

        private static IBinaryEnumerable ImposeSubKeys(IBinaryEnumerable message, IEnumerable<IBinaryEnumerable> subKeys)
        {
            var permuted = message.Permute(AlgorithmConstants.IP).ToList();
            var subKeyList = subKeys.ToList();

            var l = new List<IBinaryEnumerable>
            {
                permuted.Take(32)
            };

            var r = new List<IBinaryEnumerable>
            {
                permuted.Skip(32).Take(32)
            };

            for (var i = 1; i <= 16; i++)
            {
                l.Add(r[i - 1]);

                var fs = F(r[i - 1], subKeyList[i - 1]);

                r.Add(l[i - 1].Xor(fs));
            }

            return r[16].Concat(l[16]).Permute(AlgorithmConstants.IP2);
        }

        private static IBinaryEnumerable F(IBinaryEnumerable right, IBinaryEnumerable subKey)
        {
            var er = right.Permute(AlgorithmConstants.E);
            var xorEr = er.Xor(subKey).ToList();

            var sr = new List<bool>();

            for (var i = 0; i < 8; i++)
            {
                var b = xorEr.Skip(i * 6).Take(6).ToList();

                var row = 2 * b[0].ToByte() + b[5].ToByte();
                var column = 8 * b[1].ToByte() + 4 * b[2].ToByte() + 2 * b[3].ToByte() + b[4].ToByte();

                var m = AlgorithmConstants.S[i][row][column];

                sr.AddRange(
                    Convert
                        .ToString(m, 2)
                        .PadLeft(4, '0')
                        .Select(c => c == '1')
                );
            }

            return sr.Permute(AlgorithmConstants.P);
        }
    }
}