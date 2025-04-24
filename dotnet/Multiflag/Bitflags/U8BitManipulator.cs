namespace Multiflag.Bitflags
{
    internal class U8BitManipulator : BitManipulator<byte>
    {
        private static U8BitManipulator? current;

        private U8BitManipulator()
        {
        }

        public static U8BitManipulator Current => current ??= new U8BitManipulator();

        public override byte Zero => 0;

        protected override bool IsPowerOfTwo(byte value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        public override bool IsZero(byte flags)
        {
            return flags == 0;
        }

        public override byte BitwiseOr(byte first, byte second)
        {
            return (byte)(first | second);
        }

        public override byte BitwiseAndNot(byte first, byte second)
        {
            return (byte)(first & ~second);
        }

        public override bool BitwiseAndEquals(byte first, byte second)
        {
            return (first & second) == second;
        }
    }
}