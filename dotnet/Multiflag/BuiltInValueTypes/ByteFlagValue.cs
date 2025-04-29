namespace Multiflag.BuiltInValueTypes
{
    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="byte"/>.
    /// </summary>
    public class ByteFlagValue : IFlagValue<byte>
    {
        public byte Substraction(byte first, byte second) => (byte)(first & ~second);

        public bool Includes(byte first, byte second) => (first & second) == second;

        public byte Union(byte first, byte second) => (byte)(first | second);

        public byte Nothing() => 0;

        public bool Equivalent(byte first, byte second) => first == second;
    }
}