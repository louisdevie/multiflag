using System.Runtime.CompilerServices;

namespace Multiflag.Bitflags
{
    internal class U8BitManipulator : BitManipulator<byte>
    {
        private static U8BitManipulator? current;

        public static U8BitManipulator Current => current ??= new U8BitManipulator();

        private U8BitManipulator()
        {
        }

        protected override bool IsPowerOfTwo(byte value) => value != 0 && (value & (value - 1)) == 0;

        public override byte Zero => 0;

        public override bool IsZero(byte flags) => flags == 0;

        public override byte Or(byte first, byte second) => (byte)(first | second);

        public override byte AndNot(byte first, byte second) => (byte)(first & ~second);

        public override bool AndEquals(byte first, byte second) => (first & second) == second;
    }
}