namespace Multiflag.Bitflags
{
    internal class U32BitManipulator : BitManipulator<uint>
    {
        private static U32BitManipulator? current;

        private U32BitManipulator()
        {
        }

        public static U32BitManipulator Current => current ??= new U32BitManipulator();

        public override uint Zero => 0;

        protected override bool IsPowerOfTwo(uint value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        public override bool IsZero(uint flags)
        {
            return flags == 0;
        }

        public override uint BitwiseOr(uint first, uint second)
        {
            return first | second;
        }

        public override uint BitwiseAndNot(uint first, uint second)
        {
            return first & ~second;
        }

        public override bool BitwiseAndEquals(uint first, uint second)
        {
            return (first & second) == second;
        }
    }
}