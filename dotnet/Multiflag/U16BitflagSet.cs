using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    /// Provides bitflags based on 16-bit unsigned integers (thus allowing 16 different flags).
    /// </summary>
    public class U16BitflagSet : FlagSet<ushort>
    {
        protected override void CheckValue(ushort value) => U16BitManipulator.Current.CheckPowerOfTwo(value);

        public override ushort Empty() => U16BitManipulator.Current.Zero;

        public override bool IsEmpty(ushort flags) => U16BitManipulator.Current.IsZero(flags);

        public override ushort Union(ushort first, ushort second) => U16BitManipulator.Current.BitwiseOr(first, second);

        public override ushort Difference(ushort first, ushort second) =>
            U16BitManipulator.Current.BitwiseAndNot(first, second);

        public override bool IsSupersetOf(ushort first, ushort second) =>
            U16BitManipulator.Current.BitwiseAndEquals(first, second);
    }
}