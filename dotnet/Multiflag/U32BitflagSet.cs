using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    /// Provides bitflags based on 32-bit unsigned integers (thus allowing 32 different flags).
    /// </summary>
    public class U32BitflagSet : FlagSet<uint>
    {
        protected override void CheckValue(uint value) => U32BitManipulator.Current.CheckPowerOfTwo(value);

        public override uint Empty() => U32BitManipulator.Current.Zero;

        public override bool IsEmpty(uint flags) => U32BitManipulator.Current.IsZero(flags);

        public override uint Union(uint first, uint second) => U32BitManipulator.Current.BitwiseOr(first, second);

        public override uint Difference(uint first, uint second) => U32BitManipulator.Current.BitwiseAndNot(first, second);

        public override bool IsSupersetOf(uint first, uint second) =>
            U32BitManipulator.Current.BitwiseAndEquals(first, second);
    }
}