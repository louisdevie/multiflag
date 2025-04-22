using System.Collections.Generic;
using System.Linq;

namespace Multiflag
{
    /// <summary>
    /// A <see cref="Flag{T}"/> represents some element of
    /// <typeparamref name="T"/> that can be added or removed from the set.
    /// When a flag is added to the set, all of its parents are added along
    /// with it, and when it is removed all of its children are removed too.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Flag<T>
    where T : notnull
    {
        private readonly FlagSet<T> set;
        private readonly T value;
        private readonly List<Flag<T>> parents;
        private readonly List<Flag<T>> children;

        /// <summary>
        /// Creates a new flag.
        /// </summary>
        /// <param name="set">The set this flag belongs to.</param>
        /// <param name="value">The value of the flag.</param>
        /// <param name="parents">The parent flags.</param>
        /// <exception cref="ForeignFlagException">
        /// If one of the parents doesn't belong to the same <see cref="FlagSet{T}"/>.
        /// </exception>
        internal Flag(FlagSet<T> set, T value, IEnumerable<Flag<T>> parents)
        {
            this.set = set;
            this.value = value;
            this.parents = parents.ToList();
            this.children = new List<Flag<T>>();

            foreach (var parent in this.parents)
            {
                if (!parent.BelongsTo(this.set))
                {
                    throw new ForeignFlagException();
                }

                parent.children.Add(this);
            }
        }

        private bool BelongsTo(FlagSet<T> flagSet)
        {
            return this.set == flagSet;
        }

        /// <summary>
        /// Add a flag if it is not already present.
        /// </summary>
        /// <param name="flags">The flags to add it to.</param>
        /// <returns>The modified flags.</returns>
        /// <remarks>
        /// If <typeparamref name="T"/> is reference type, the flags passed to the function may be modified in-place.
        /// </remarks>
        public T AddTo(T flags)
        {
            return this.parents.Aggregate(
                this.set.Union(flags, this.value),
                (current, parent) => parent.AddTo(current)
            );
        }

        /// <summary>
        /// See <see cref="AddTo(T)"/>.
        /// </summary>
        public static T operator +(T value, Flag<T> flag)
        {
            return flag.AddTo(value);
        }

        /// <summary>
        /// Removes a flag if it is present.
        /// </summary>
        /// <param name="flags">The flags to remove it from.</param>
        /// <returns>The modified flags.</returns>
        /// <remarks>
        /// If <typeparamref name="T"/> is reference type, the flags passed to the function may be modified in-place.
        /// </remarks>
        public T RemoveFrom(T flags)
        {
            return this.children.Aggregate(
                this.set.Difference(flags, this.value),
                (current, child) => child.RemoveFrom(current)
            );
        }

        /// <summary>
        /// See <see cref="RemoveFrom(T)"/>
        /// </summary>
        public static T operator -(T value, Flag<T> flag)
        {
            return flag.RemoveFrom(value);
        }

        /// <summary>
        /// Check whether this flag is in <paramref name="flags"/>.
        /// </summary>
        /// <param name="flags">The flags to search in.</param>
        /// <returns>
        /// <see langword="true"/> if this flag belongs to the set of
        /// <paramref name="flags"/>, otherwise <see langword="false"/>.
        /// </returns>
        public bool IsIn(T flags) => this.set.IsSupersetOf(flags, this.value)
                                     && this.parents.All(parent => parent.IsIn(flags));

        /// <summary>
        /// <see langword="true"/> when this flag has no value on its own.
        /// </summary>
        public bool IsAbstract => this.set.IsEmpty(this.value);
    }
}