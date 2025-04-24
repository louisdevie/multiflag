using System.Collections.Generic;

namespace Multiflag
{
    /// <summary>
    ///     Represent a group of flags, and provide methods to use
    ///     <typeparamref name="T" /> as a set. Built-in implementations exist for
    ///     unsigned integers, enums and <see cref="HashSet{T}" />.
    /// </summary>
    /// <typeparam name="T">The type to be used as a set of flags.</typeparam>
    public abstract class FlagSet<T>
    where T : notnull
    {
        private readonly Dictionary<T, Flag<T>> concreteFlags;

        /// <summary>
        ///     Creates a new empty flag set.
        /// </summary>
        protected FlagSet()
        {
            this.concreteFlags = new Dictionary<T, Flag<T>>();
        }

        /// <summary>
        ///     Creates a flag without a value.
        /// </summary>
        /// <param name="parents">Other flags required for this flag to be set.</param>
        /// <returns>A flag bound to this set.</returns>
        /// <exception cref="ForeignFlagException">
        ///     If one of the parents doesn't belong to the same <see cref="FlagSet{T}" />.
        /// </exception>
        public Flag<T> Flag(params Flag<T>[] parents)
        {
            return new Flag<T>(this, this.Empty(), parents);
        }

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
        public Flag<T> Flag(T value, params Flag<T>[] parents)
        {
            this.CheckValue(value);

            if (this.concreteFlags.ContainsKey(value))
            {
                throw new ReusedFlagValueException(value);
            }

            var flag = new Flag<T>(this, value, parents);
            this.concreteFlags.Add(value, flag);
            return flag;
        }

        /// <summary>
        ///     This method will be called when a new flag is about to be created
        ///     with that value. The default implementation does nothing, but it
        ///     may be overriden to throw an exception on invalid values.
        /// </summary>
        /// <param name="value">The value that will be used for the flag.</param>
        protected virtual void CheckValue(T value)
        {
        }

        /// <summary>
        ///     Creates an empty set of flags.
        /// </summary>
        public abstract T Empty();

        /// <summary>
        ///     Checks if a set of flags is the empty set.
        /// </summary>
        /// <param name="flags">The set of flags to test.</param>
        public abstract bool IsEmpty(T flags);

        /// <summary>
        ///     Computes the union of two sets of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        /// <returns>A new set that contains the flags of both sets.</returns>
        public abstract T Union(T first, T second);

        /// <summary>
        ///     Computes the difference of two set of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags (that will be subtracted from the first).</param>
        /// <returns>A new set that contains the flags of the first set that do not appear in the second.</returns>
        public abstract T Difference(T first, T second);

        /// <summary>
        ///     Checks whether the first set of flags is a superset of the second.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        public abstract bool IsSupersetOf(T first, T second);
    }
}