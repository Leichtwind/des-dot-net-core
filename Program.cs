using System;
using System.Linq;

namespace DES_.Net_Core
{
    internal static class Program
    {
        private const string Key = "secret#!";
        private const string Message = "Hi, Mark";

        private static void Main()
        {
            var encoded = Coder.Encode(Key, Message).ToList();
            var decoded = Coder.Decode(Key, encoded);

            Console.WriteLine($"Original Message: {Message}");
            Console.WriteLine($"Encoded: {encoded.ConvertToString()}");
            Console.WriteLine($"Decoded: {decoded.ConvertToString()}");
        }
    }
}