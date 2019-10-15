namespace DES_.Net_Core
{
    public static class BoolExtension
    {
        public static byte ToByte(this bool source) => (byte) (source ? 1 : 0);
    }
}