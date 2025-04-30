using System.Collections.Generic;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 64-bit unsigned integers (thus allowing 64 different flags).
    /// </summary>
    public class U64BitflagSet : FlagSet<ulong, ulong>
    {
        /// <inheritdoc />
        protected override sealed ulong WrapValue(ulong value)
        {
            U64BitManipulator.Current.CheckPowerOfTwo(value);
            return value;
        }

        /// <inheritdoc />
        public override sealed ulong Empty()
        {
            return U64BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed ulong Union(ulong first, ulong second)
        {
            return U64BitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed ulong Intersection(ulong first, ulong second)
        {
            return U64BitManipulator.Current.BitwiseAnd(first, second);
        }

        /// <inheritdoc />
        public override sealed ulong Difference(ulong first, ulong second)
        {
            return U64BitManipulator.Current.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(ulong first, ulong second)
        {
            return U64BitManipulator.Current.BitwiseAndEquals(first, second);
        }

        /// <inheritdoc />
        public override IEnumerable<ulong> Iterate(ulong flags)
        {
            return new NumericFlagEnumerator<ulong>(
                U64BitManipulator.Current,
                flags
            );
        }
    }
}