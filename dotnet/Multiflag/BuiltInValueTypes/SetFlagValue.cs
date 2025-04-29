namespace Multiflag.BuiltInValueTypes
{
    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="HashSet{T}"/>.
    /// </summary>
    /// <remarks>
    /// The set is modified in-place. If you do not want this behavior, make a
    /// copy of your set before using flag operation on it.
    /// </remarks>
    public class SetFlagValue<T> : IFlagValue<HashSet<T>>
    {
        public HashSet<T> Substraction(HashSet<T> first, HashSet<T> second)
        {
            first.ExceptWith(second);
            return first;
        }

        public bool Includes(HashSet<T> first, HashSet<T> second)
        {
            return first.IsSupersetOf(second);
        }

        public HashSet<T> Nothing()
        {
            return new HashSet<T>();
        }

        public HashSet<T> Union(HashSet<T> first, HashSet<T> second)
        {
            first.UnionWith(second);
            return first;
        }

        public bool Equivalent(HashSet<T> first, HashSet<T> second) => first.SetEquals(second);
    }
}
