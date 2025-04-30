using System.Collections.Generic;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 8-bit unsigned integers (thus allowing 8 different flags).
    /// </summary>
    public class U8BitflagSet : FlagSet<byte, byte>
    {
        /// <inheritdoc />
        protected override sealed byte WrapValue(byte value)
        {
            U8BitManipulator.Current.CheckPowerOfTwo(value);
            return value;
        }

        /// <inheritdoc />
        public override sealed byte Empty()
        {
            return U8BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed byte Union(byte first, byte second)
        {
            return U8BitManipulator.Current.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed byte Intersection(byte first, byte second)
        {
            return U8BitManipulator.Current.BitwiseAnd(first, second);
        }

        /// <inheritdoc />
        public override sealed byte Difference(byte first, byte second)
        {
            return U8BitManipulator.Current.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(byte first, byte second)
        {
            return U8BitManipulator.Current.BitwiseAndEquals(first, second);
        }

        /// <inheritdoc />
        public override IEnumerable<byte> Iterate(byte flags)
        {
            return new NumericFlagEnumerator<byte>(
                U8BitManipulator.Current,
                flags
            );
        }
    }
}