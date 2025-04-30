using System.Collections.Generic;
using System.Numerics;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on dynamic integers (thus allowing any number of flags).
    /// </summary>
    public class DynamicBitflagSet : FlagSet<BigInteger, BigInteger>
    {
        /// <inheritdoc />
        protected override sealed BigInteger WrapValue(BigInteger value)
        {
            BigintBitManipulator.Current.CheckPowerOfTwo(value);
            return value;
        }

        /// <inheritdoc />
        public override sealed BigInteger Empty()
        {
            return BigintBitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed BigInteger Union(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed BigInteger Intersection(BigInteger first, BigInteger second)
        {
            return BigintBitManipulator.Current.BitwiseAnd(first, second);
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

        /// <inheritdoc />
        public override IEnumerable<BigInteger> Iterate(BigInteger flags)
        {
            return new NumericFlagEnumerator<BigInteger>(
                BigintBitManipulator.Current,
                flags
            );
        }
    }
}