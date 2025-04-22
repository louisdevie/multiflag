using System.Runtime.CompilerServices;

namespace Multiflag.Bitflags
{
    internal class U32BitManipulator : BitManipulator<uint>
    {
        private static U32BitManipulator? current;

        public static U32BitManipulator Current => current ??= new U32BitManipulator();

        private U32BitManipulator()
        {
        }

        protected override bool IsPowerOfTwo(uint value) => value != 0 && (value & (value - 1)) == 0;

        public override uint Zero => 0;

        public override bool IsZero(uint flags) => flags == 0;

        public override uint Or(uint first, uint second) => first | second;

        public override uint AndNot(uint first, uint second) => first & ~second;

        public override bool AndEquals(uint first, uint second) => (first & second) == second;
    }
}