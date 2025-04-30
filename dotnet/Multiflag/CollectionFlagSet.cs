using System.Collections.Generic;
using System.Collections.Immutable;

namespace Multiflag
{
    /// <summary>
    ///     Provides flags that work on HashSets. The flags can have values of any type and there is no limit to the number
    ///     of different flags, and sets can easily be serialized (as a JSON array for example).
    /// </summary>
    public class CollectionFlagSet<T> : FlagSet<T, IImmutableSet<T>>
    where T : notnull
    {
        /// <inheritdoc />
        protected override sealed IImmutableSet<T> WrapValue(T value)
        {
            return ImmutableHashSet.Create(value);
        }

        /// <inheritdoc />
        public override sealed IImmutableSet<T> Empty()
        {
            return ImmutableHashSet<T>.Empty;
        }

        /// <inheritdoc />
        public override sealed IImmutableSet<T> Union(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.Union(second);
        }
        
        /// <inheritdoc />
        public override sealed IImmutableSet<T> Intersection(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.Intersect(second);
        }

        /// <inheritdoc />
        public override sealed IImmutableSet<T> Difference(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.Except(second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(IImmutableSet<T> first, IImmutableSet<T> second)
        {
            return first.IsSupersetOf(second);
        }

        /// <inheritdoc />
        public override IEnumerable<T> Iterate(IImmutableSet<T> flags)
        {
            return flags;
        }
    }
}