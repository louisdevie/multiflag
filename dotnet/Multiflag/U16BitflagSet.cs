using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on 16-bit unsigned integers (thus allowing 16 different flags).
    /// </summary>
    public class U16BitflagSet : FlagSet<ushort>
    {
        /// <inheritdoc />
        protected override sealed void CheckValue(ushort value)
        {
            U16BitManipulator.Current.CheckPowerOfTwo(value);
        }

        /// <inheritdoc />
        public override sealed ushort Empty()
        {
            return U16BitManipulator.Current.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(ushort flags)
        {
            return U16BitManipulator.Current.IsZero(flags);
        }

        /// <inheritdoc />
        public override sealed ushort Union(ushort first, ushort second)
        {
            return U16BitManipulator.Current.BitwiseOr(first, second);
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
    }
}