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

        public override BigInteger Zero => BigInteger.Zero;
        
        public override BigInteger One => BigInteger.One;

        public override bool IsZero(BigInteger value)
        {
            return value.IsZero;
        }

        public override bool IsEven(BigInteger value)
        {
            return value.IsEven;
        }

        protected override bool IsPowerOfTwo(BigInteger value)
        {
            return value.IsPowerOfTwo;
        }

        public override BigInteger BitwiseOr(BigInteger first, BigInteger second)
        {
            return first | second;
        }

        public override BigInteger BitwiseAnd(BigInteger first, BigInteger second)
        {
            return first & second;
        }

        public override BigInteger BitwiseAndNot(BigInteger first, BigInteger second)
        {
            return first & ~second;
        }

        public override bool BitwiseAndEquals(BigInteger first, BigInteger second)
        {
            return (first & second) == second;
        }

        public override BigInteger ShiftLeft(BigInteger value)
        {
            return value << 1;
        }

        public override BigInteger ShiftRight(BigInteger value)
        {
            return value >> 1;
        }
    }
}