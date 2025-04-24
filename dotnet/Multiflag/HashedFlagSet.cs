using System.Collections.Immutable;

namespace Multiflag
{
    /// <summary>
    ///     Provides flags that work on HashSets. The flags can have values of any type and there is no limit to the number
    ///     of different flags, and sets can easily be serialized (as a JSON array for example).
    /// </summary>
    public class HashedFlagSet<T> : FlagSet<IImmutableSet<T>>
    where T : notnull
    {
        /// <summary>
        ///     Creates a flag with a value.
        /// </summary>
        /// <param name="value">The value of the flag.</param>
        /// <param name="parents">Other flags required for this flag to be set.</param>
        /// <returns>A flag bound to this set.</returns>
        /// <exception cref="ForeignFlagException">
        ///     If one of the parents doesn't belong to the same <see cref="FlagSet{T}" />.
        /// </exception>
        /// <exception cref="ReusedFlagValueException">
        ///     If another flag has already been created with the same value.
        /// </exception>
        public Flag<IImmutableSet<T>> Flag(T value, params Flag<IImmutableSet<T>>[] parents)
        {
            return this.Flag(ImmutableHashSet.Create(value), parents);
        }

        public override IImmutableSet<T> Empty()
        {
            return ImmutableHashSet<T>.Empty;
        }

        public override bool IsEmpty(IImmutableSet<T> flags)
        {
            return flags.Count == 0;
        }

        public override IImmutableSet<T> Union(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.Union(second);
        }

        public override IImmutableSet<T> Difference(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.Except(second);
        }

        public override bool IsSupersetOf(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.IsSupersetOf(second);
        }
    }
}