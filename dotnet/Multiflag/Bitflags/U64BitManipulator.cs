namespace Multiflag.Bitflags
{
    internal class U64BitManipulator : BitManipulator<ulong>
    {
        private static U64BitManipulator? current;

        private U64BitManipulator()
        {
        }

        public static U64BitManipulator Current => current ??= new U64BitManipulator();

        public override ulong Zero => 0;

        protected override bool IsPowerOfTwo(ulong value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        public override bool IsZero(ulong flags)
        {
            return flags == 0;
        }

        public override ulong BitwiseOr(ulong first, ulong second)
        {
            return first | second;
        }

        public override ulong BitwiseAndNot(ulong first, ulong second)
        {
            return first & ~second;
        }

        public override bool BitwiseAndEquals(ulong first, ulong second)
        {
            return (first & second) == second;
        }
    }
}