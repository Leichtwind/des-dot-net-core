using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBinaryEnumerable = System.Collections.Generic.IEnumerable<bool>;

namespace DES_.Net_Core
{
    public static class BinaryEnumerableExtension
    {
        private static IBinaryEnumerable ShiftLeftCircular(this IBinaryEnumerable source)
        {
            var enumerable = source.ToList();

            var first = enumerable.First();
            var result = enumerable.Skip(1).ToList();

            result.Add(first);

            return result;
        }

        public static IBinaryEnumerable ShiftLeftCircular(this IBinaryEnumerable source, int numberOfShifts)
        {
            var result = source;

            for (var i = 0; i < numberOfShifts; i++)
            {
                result = result.ShiftLeftCircular();
            }

            return result;
        }

        public static IBinaryEnumerable Xor(this IBinaryEnumerable source, IBinaryEnumerable arg) =>
            source.Zip(arg).Select(tuple => tuple.First ^ tuple.Second);

        public static IBinaryEnumerable Permute(this IBinaryEnumerable source, IEnumerable<int> permutationTable)
        {
            var list = source.ToList();

            return permutationTable.Select(i => list[i - 1]);
        }

        public static string ConvertToString(this IBinaryEnumerable source)
        {
            var builder = new StringBuilder();
            var list = source.ToList();

            for (var i = 0; i < list.Count; i += 8)
            {
                var octet = list.Skip(i).Take(8).Select(b => b ? '1' : '0');

                var number = Convert.ToInt32(string.Join(null, octet), 2);

                builder.Append((char) number);
            }

            return builder.ToString();
        }

        public static string ToBinaryString(this IBinaryEnumerable source) =>
            string.Join(null, source.Select(b => b ? '1' : '0'));
    }
}