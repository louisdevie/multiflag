namespace Multiflag.BuiltInValueTypes
{
    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="ulong"/>.
    /// </summary>
    public class ULongFlagValue : IFlagValue<ulong>
    {
        public ulong Substraction(ulong first, ulong second) => first & ~second;

        public bool Includes(ulong first, ulong second) => (first & second) == second;

        public ulong Union(ulong first, ulong second) => first | second;

        public ulong Nothing() => 0;

        public bool Equivalent(ulong first, ulong second) => first == second;
    }
}