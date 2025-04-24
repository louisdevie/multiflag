using System.Numerics;
using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on dynamic integers (thus allowing any number of flags).
    /// </summary>
    public class DynamicBitflagSet : FlagSet<BigInteger>
    {
        /// <inheritdoc />
        protected override sealed void CheckValue(BigInteger value)
        {
            BigintBitManipulator.Current.CheckPowerOfTwo(value);
        }

        /// <inheritdoc />
        public override sealed BigInteger Empty()
        {
            return BigintBitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(BigInteger flags)
        {
            return BigintBitManipulator.Current.IsZero(flags);
        }

        /// <inheritdoc />
        public override sealed BigInteger Union(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed BigInteger Difference(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseAndEquals(first, second);
        }
    }
}