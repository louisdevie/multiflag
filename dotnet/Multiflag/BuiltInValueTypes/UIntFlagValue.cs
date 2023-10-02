namespace Multiflag.BuiltInValueTypes
{
    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="uint"/>.
    /// </summary>
    public class UIntFlagValue : IFlagValue<uint>
    {
        public uint Substraction(uint first, uint second) => first & ~second;

        public bool Includes(uint first, uint second) => (first & second) == second;

        public uint Union(uint first, uint second) => first | second;

        public uint Nothing() => 0;

        public bool Equivalent(uint first, uint second) => first == second;
    }
}