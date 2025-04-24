using System.Runtime.CompilerServices;

namespace Multiflag.Bitflags
{
    internal class U16BitManipulator : BitManipulator<ushort>
    {
        private static U16BitManipulator? current;

        public static U16BitManipulator Current => current ??= new U16BitManipulator();

        private U16BitManipulator()
        {
        }

        protected override bool IsPowerOfTwo(ushort value) => value != 0 && (value & (value - 1)) == 0;

        public override ushort Zero => 0;

        public override bool IsZero(ushort flags) => flags == 0;

        public override ushort BitwiseOr(ushort first, ushort second) => (ushort)(first | second);

        public override ushort BitwiseAndNot(ushort first, ushort second) => (ushort)(first & ~second);

        public override bool BitwiseAndEquals(ushort first, ushort second) => (first & second) == second;
    }
}