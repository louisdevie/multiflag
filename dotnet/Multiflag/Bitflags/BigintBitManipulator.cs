using System.Numerics;

namespace Multiflag.Bitflags
{
    internal class BigintBitManipulator : BitManipulator<BigInteger>
    {
        private static BigintBitManipulator? current;

        private BigintBitManipulator()
        {
        }

        public static BigintBitManipulator Current => current ??= new BigintBitManipulator();

        public override BigInteger Zero => 0;

        protected override bool IsPowerOfTwo(BigInteger value)
        {
            return value.IsPowerOfTwo;
        }

        public override bool IsZero(BigInteger flags)
        {
            return flags == 0;
        }

        public override BigInteger BitwiseOr(BigInteger first, BigInteger second)
        {
            return first | second;
        }

        public override BigInteger BitwiseAndNot(BigInteger first, BigInteger second)
        {
            return first & ~second;
        }

        public override bool BitwiseAndEquals(BigInteger first, BigInteger second)
        {
            return (first & second) == second;
        }
    }
}