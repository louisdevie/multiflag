using System.Collections.Generic;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 32-bit unsigned integers (thus allowing 32 different flags).
    /// </summary>
    public class U32BitflagSet : FlagSet<uint, uint>
    {
        /// <inheritdoc />
        protected override sealed uint WrapValue(uint value)
        {
            U32BitManipulator.Current.CheckPowerOfTwo(value);
            return value;
        }

        /// <inheritdoc />
        public override sealed uint Empty()
        {
            return U32BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed uint Union(uint first, uint second)
        {
            return U32BitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed uint Intersection(uint first, uint second)
        {
            return U32BitManipulator.Current.BitwiseAnd(first, second);
        }

        /// <inheritdoc />
        public override sealed uint Difference(uint first, uint second)
        {
            return U32BitManipulator.Current.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(uint first, uint second)
        {
            return U32BitManipulator.Current.BitwiseAndEquals(first, second);
        }

        /// <inheritdoc />
        public override IEnumerable<uint> Iterate(uint flags)
        {
            return new NumericFlagEnumerator<uint>(
                U32BitManipulator.Current,
                flags
            );
        }
    }
}