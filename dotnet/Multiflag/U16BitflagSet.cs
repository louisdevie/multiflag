using System.Collections.Generic;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 16-bit unsigned integers (thus allowing 16 different flags).
    /// </summary>
    public class U16BitflagSet : FlagSet<ushort, ushort>
    {
        /// <inheritdoc />
        protected override sealed ushort WrapValue(ushort value)
        {
            U16BitManipulator.Current.CheckPowerOfTwo(value);
            return value;
        }

        /// <inheritdoc />
        public override sealed ushort Empty()
        {
            return U16BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed ushort Union(ushort first, ushort second)
        {
            return U16BitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed ushort Intersection(ushort first, ushort second)
        {
            return U16BitManipulator.Current.BitwiseAnd(first, second);
        }

        /// <inheritdoc />
        public override sealed ushort Difference(ushort first, ushort second)
        {
            return U16BitManipulator.Current.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(ushort first, ushort second)
        {
            return U16BitManipulator.Current.BitwiseAndEquals(first, second);
        }

        /// <inheritdoc />
        public override IEnumerable<ushort> Iterate(ushort flags)
        {
            return new NumericFlagEnumerator<ushort>(
                U16BitManipulator.Current,
                flags
            );
        }
    }
}