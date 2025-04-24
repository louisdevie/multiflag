using System.Numerics;
using System.Runtime.CompilerServices;

namespace Multiflag.Bitflags
{
    internal class BigintBitManipulator : BitManipulator<BigInteger>
    {
        private static BigintBitManipulator? current;

        public static BigintBitManipulator Current => current ??= new BigintBitManipulator();

        private BigintBitManipulator()
        {
        }

        protected override bool IsPowerOfTwo(BigInteger value) => value.IsPowerOfTwo;

        public override BigInteger Zero => 0;

        public override bool IsZero(BigInteger flags) => flags == 0;

        public override BigInteger BitwiseOr(BigInteger first, BigInteger second) => first | second;

        public override BigInteger BitwiseAndNot(BigInteger first, BigInteger second) => first & ~second;

        public override bool BitwiseAndEquals(BigInteger first, BigInteger second) => (first & second) == second;
    }
}