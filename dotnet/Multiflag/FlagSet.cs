using System.Collections.Generic;

namespace Multiflag
{
    /// <summary>
    ///     Represent a group of flags, and provide methods to use
    ///     <typeparamref name="TSet" /> as a set.
    ///     Built-in implementations exist for unsigned integers, enums,
    ///     hash sets and lists.
    /// </summary>
    /// <typeparam name="TValue">The type of the flag values.</typeparam>
    /// <typeparam name="TSet">The type of the sets of flags.</typeparam>
    public abstract class FlagSet<TValue, TSet> : ISetOperations<TSet>
    where TValue : notnull
    {
        private readonly Dictionary<TValue, ValueFlag<TSet>> valueFlags;

        /// <summary>
        ///     Creates a new empty flag set.
        /// </summary>
        protected FlagSet()
        {
            this.valueFlags = new Dictionary<TValue, ValueFlag<TSet>>();
        }

        /// <summary>
        ///     Creates a flag without a value.
        /// </summary>
        /// <param name="parents">
        ///     Other flags required for this flag to be set.
        /// </param>
        /// <returns>A flag bound to this set.</returns>
        /// <exception cref="ForeignFlagException">
        ///     If one of the parents doesn't belong to the same
        ///     <see cref="FlagSet{TValue, TSet}" />.
        /// </exception>
        public Flag<TSet> Flag(params Flag<TSet>[] parents)
        {
            return new Flag<TSet>(this, parents);
        }

        /// <summary>
        ///     Creates a flag with a value.
        /// </summary>
        /// <param name="value">The value of the flag.</param>
        /// <param name="parents">
        ///     Other flags required for this flag to be set.
        /// </param>
        /// <returns>A flag bound to this set.</returns>
        /// <exception cref="ForeignFlagException">
        ///     If one of the parents doesn't belong to the same
        ///     <see cref="FlagSet{TValue, TSet}" />.
        /// </exception>
        /// <exception cref="ReusedFlagValueException">
        ///     If another flag has already been created with the same value.
        /// </exception>
        public Flag<TSet> Flag(TValue value, params Flag<TSet>[] parents)
        {
            if (this.valueFlags.ContainsKey(value))
            {
                throw new ReusedFlagValueException(value);
            }

            var flag = new ValueFlag<TSet>(this, parents, this.WrapValue(value));
            this.valueFlags.Add(value, flag);
            return flag;
        }

        /// <summary>
        ///     Transforms a value into a set containing only that value.
        ///     This method may throw an exception if the value is not valid.
        /// </summary>
        /// <param name="value">The value that will be used for the flag.</param>
        protected abstract TSet WrapValue(TValue value);

        /// <summary>
        ///     Filters a flag set so that it only contains the flags that were
        ///     declared with the <c>Flag</c> method. If a flags is missing some
        ///     of its parents, it will not be included in the result.
        /// </summary>
        /// <param name="flags">The set of flags to filter.</param>
        /// <returns>A new set of flags.</returns>
        /// <seealso cref="Maximum"/>
        public TSet Minimum(TSet flags)
        {
            TSet result = this.Empty();
            foreach (var value in this.Iterate(flags))
            {
                if (this.valueFlags.TryGetValue(value, out var flag) && flag.IsIn(flags))
                {
                    result = flag.AddTo(result);
                }
            }

            return result;
        }

        /// <summary>
        ///     Creates a copy of a flag set that will contain all the flags
        ///     that were declared with the <c>Flag</c> method. If a flag is
        ///     missing some of its parents in the original set, they will be
        ///     added to the result.
        /// </summary>
        /// <param name="flags">The set of flags to filter.</param>
        /// <returns>A new set of flags.</returns>
        /// <seealso cref="Minimum"/>
        public TSet Maximum(TSet flags)
        {
            TSet result = this.Empty();
            foreach (var value in this.Iterate(flags)) {
                if (this.valueFlags.TryGetValue(value, out var flag))
                {
                    result = flag.AddTo(result);
                }
            }

            return result;
        }

        /// <summary>
        ///     Creates an empty set of flags.
        /// </summary>
        public abstract TSet Empty();

        /// <summary>
        ///     Computes the union of two sets of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        /// <returns>
        ///     A new set that contains the flags that appear in any of the sets.
        /// </returns>
        public abstract TSet Union(TSet first, TSet second);

        /// <summary>
        ///     Computes the intersection of two sets of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        /// <returns>
        ///     A new set that contains the flags that appear in both sets.
        /// </returns>
        public abstract TSet Intersection(TSet first, TSet second);

        /// <summary>
        ///     Computes the difference of two set of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">
        ///     The second set of flags (that will be subtracted from the first).
        /// </param>
        /// <returns>
        ///     A new set that contains the flags of the first set that do not
        ///     appear in the second.
        /// </returns>
        public abstract TSet Difference(TSet first, TSet second);

        /// <summary>
        ///     Checks whether the first set of flags is a superset of the second.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        public abstract bool IsSupersetOf(TSet first, TSet second);

        /// <summary>
        ///    Returns an iterable over the elements of a set.
        /// </summary>
        /// <param name="flags">A set of flags</param>
        public abstract IEnumerable<TValue> Iterate(TSet flags);
    }
}