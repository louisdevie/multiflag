using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 32-bit unsigned integers (thus allowing 32 different flags).
    /// </summary>
    public class U32BitflagSet : FlagSet<uint>
    {
        /// <inheritdoc />
        protected override sealed void CheckValue(uint value)
        {
            U32BitManipulator.Current.CheckPowerOfTwo(value);
        }

        /// <inheritdoc />
        public override sealed uint Empty()
        {
            return U32BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(uint flags)
        {
            return U32BitManipulator.Current.IsZero(flags);
        }

        /// <inheritdoc />
        public override sealed uint Union(uint first, uint second)
        {
            return U32BitManipulator.Current.BitwiseOr(first, second);
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
    }
}