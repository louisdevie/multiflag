namespace Multiflag.BuiltInValueTypes
{
    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="ushort"/>.
    /// </summary>
    public class UShortFlagValue : IFlagValue<ushort>
    {
        public ushort Substraction(ushort first, ushort second) => (ushort)(first & ~second);

        public bool Includes(ushort first, ushort second) => (first & second) == second;

        public ushort Union(ushort first, ushort second) => (ushort)(first | second);

        public ushort Nothing() => 0;

        public bool Equivalent(ushort first, ushort second) => first == second;
    }
}