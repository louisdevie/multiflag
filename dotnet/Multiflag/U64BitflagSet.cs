using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 64-bit unsigned integers (thus allowing 64 different flags).
    /// </summary>
    public class U64BitflagSet : FlagSet<ulong>
    {
        /// <inheritdoc />
        protected override sealed void CheckValue(ulong value)
        {
            U64BitManipulator.Current.CheckPowerOfTwo(value);
        }

        /// <inheritdoc />
        public override sealed ulong Empty()
        {
            return U64BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(ulong flags)
        {
            return U64BitManipulator.Current.IsZero(flags);
        }

        /// <inheritdoc />
        public override sealed ulong Union(ulong first, ulong second)
        {
            return U64BitManipulator.Current.BitwiseOr(first, second);
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
    }
}