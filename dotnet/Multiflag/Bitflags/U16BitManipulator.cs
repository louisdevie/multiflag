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

        protected override bool IsPowerOfTwo(ushort value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        public override bool IsZero(ushort flags)
        {
            return flags == 0;
        }

        public override ushort BitwiseOr(ushort first, ushort second)
        {
            return (ushort)(first | second);
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