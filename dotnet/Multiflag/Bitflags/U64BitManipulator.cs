using System.Runtime.CompilerServices;

namespace Multiflag.Bitflags
{
    internal class U64BitManipulator : BitManipulator<ulong>
    {
        private static U64BitManipulator? current;

        public static U64BitManipulator Current => current ??= new U64BitManipulator();

        private U64BitManipulator()
        {
        }

        protected override bool IsPowerOfTwo(ulong value) => value != 0 && (value & (value - 1)) == 0;

        public override ulong Zero => 0;

        public override bool IsZero(ulong flags) => flags == 0;

        public override ulong Or(ulong first, ulong second) => first | second;

        public override ulong AndNot(ulong first, ulong second) => first & ~second;

        public override bool AndEquals(ulong first, ulong second) => (first & second) == second;
    }
}