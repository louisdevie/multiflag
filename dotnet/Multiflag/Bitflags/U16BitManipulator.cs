namespace Multiflag.Bitflags
{
    internal class U16BitManipulator : BitManipulator<ushort>
    {
        private static U16BitManipulator? current;

        private U16BitManipulator()
        {
        }

        public static U16BitManipulator Current => current ??= new U16BitManipulator();

        public override ushort Zero => 0;

        public override ushort One => 1;

        public override bool IsZero(ushort value) => value == 0;

        public override bool IsEven(ushort value) => (value & 1) == 0;

        protected override bool IsPowerOfTwo(ushort value)
        {
            return value != 0 && (value & value - 1) == 0;
        }
        
        public override ushort ShiftLeft(ushort value)
        {
            return (ushort)(value << 1);
        }

        public override ushort ShiftRight(ushort value)
        {
            return (ushort)(value >> 1);
        }

        public override ushort BitwiseOr(ushort first, ushort second)
        {
            return (ushort)(first | second);
        }

        public override ushort BitwiseAnd(ushort first, ushort second)
        {
            return (ushort)(first & second);
        }

        public override ushort BitwiseAndNot(ushort first, ushort second)
        {
            return (ushort)(first & ~second);
        }

        public override bool BitwiseAndEquals(ushort first, ushort second)
        {
            return (first & second) == second;
        }
    }
}