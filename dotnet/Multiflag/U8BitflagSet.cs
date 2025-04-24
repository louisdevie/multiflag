using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 8-bit unsigned integers (thus allowing 8 different flags).
    /// </summary>
    public class U8BitflagSet : FlagSet<byte>
    {
        /// <inheritdoc />
        protected override sealed void CheckValue(byte value)
        {
            U8BitManipulator.Current.CheckPowerOfTwo(value);
        }

        /// <inheritdoc />
        public override sealed byte Empty()
        {
            return U8BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(byte flags)
        {
            return U8BitManipulator.Current.IsZero(flags);
        }

        /// <inheritdoc />
        public override sealed byte Union(byte first, byte second)
        {
            return U8BitManipulator.Current.BitwiseOr(first, second);
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
    }
}