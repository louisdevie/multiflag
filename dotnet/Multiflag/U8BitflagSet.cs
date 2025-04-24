using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    /// Provides bitflags based on 8-bit unsigned integers (thus allowing 8 different flags).
    /// </summary>
    public class U8BitflagSet : FlagSet<byte>
    {
        protected override void CheckValue(byte value) => U8BitManipulator.Current.CheckPowerOfTwo(value);

        public override byte Empty() => U8BitManipulator.Current.Zero;

        public override bool IsEmpty(byte flags) => U8BitManipulator.Current.IsZero(flags);

        public override byte Union(byte first, byte second) => U8BitManipulator.Current.BitwiseOr(first, second);

        public override byte Difference(byte first, byte second) => U8BitManipulator.Current.BitwiseAndNot(first, second);

        public override bool IsSupersetOf(byte first, byte second) => U8BitManipulator.Current.BitwiseAndEquals(first, second);
    }
}