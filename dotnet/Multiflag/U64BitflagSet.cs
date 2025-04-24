using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    /// Provides bitflags based on 64-bit unsigned integers (thus allowing 64 different flags).
    /// </summary>
    public class U64BitflagSet : FlagSet<ulong>
    {
        protected override void CheckValue(ulong value) => U64BitManipulator.Current.CheckPowerOfTwo(value);

        public override ulong Empty() => U64BitManipulator.Current.Zero;

        public override bool IsEmpty(ulong flags) => U64BitManipulator.Current.IsZero(flags);

        public override ulong Union(ulong first, ulong second) => U64BitManipulator.Current.BitwiseOr(first, second);

        public override ulong Difference(ulong first, ulong second) => U64BitManipulator.Current.BitwiseAndNot(first, second);

        public override bool IsSupersetOf(ulong first, ulong second) =>
            U64BitManipulator.Current.BitwiseAndEquals(first, second);
    }
}