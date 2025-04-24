using System.Numerics;
using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on dynamic integers (thus allowing any number of flags).
    /// </summary>
    public class DynamicBitflagSet : FlagSet<BigInteger>
    {
        protected override void CheckValue(BigInteger value)
        {
            BigintBitManipulator.Current.CheckPowerOfTwo(value);
        }

        public override BigInteger Empty()
        {
            return BigintBitManipulator.Current.Zero;
        }

        public override bool IsEmpty(BigInteger flags)
        {
            return BigintBitManipulator.Current.IsZero(flags);
        }

        public override BigInteger Union(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseOr(first, second);
        }

        public override BigInteger Difference(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseAndNot(first, second);
        }

        public override bool IsSupersetOf(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseAndEquals(first, second);
        }
    }
}