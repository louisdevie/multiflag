using System.Collections.Generic;
using System.Linq;

namespace Multiflag
{
    /// <summary>
    ///     A <see cref="Flag{T}" /> represents some element of
    ///     <typeparamref name="TSet" /> that can be added or removed from the set.
    ///     When a flag is added to the set, all of its parents are added along
    ///     with it, and when it is removed all of its children are removed too.
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    public class Flag<TSet>
    {
        private readonly ISetOperations<TSet> operations;
        private readonly List<Flag<TSet>> parents;
        private readonly List<Flag<TSet>> children;

        /// <summary>
        ///     Creates a new flag.
        /// </summary>
        internal Flag(ISetOperations<TSet> operations, IEnumerable<Flag<TSet>> parents)
        {
            this.operations = operations;
            this.parents = parents.ToList();
            this.children = new List<Flag<TSet>>();

            foreach (var parent in this.parents)
            {
                if (!parent.BelongsTo(this.operations))
                {
                    throw new ForeignFlagException();
                }

                parent.children.Add(this);
            }
        }
        
        internal ISetOperations<TSet> Operations => this.operations;

        /// <summary>
        ///     <see langword="true" /> when this flag has no value on its own.
        /// </summary>
        public virtual bool IsAbstract => true;

        private bool BelongsTo(ISetOperations<TSet> flagSet)
        {
            return this.operations == flagSet;
        }

        /// <summary>
        ///     Add a flag if it is not already present.
        /// </summary>
        /// <param name="flags">The flags to add it to.</param>
        /// <returns>A copy of the flags with this flag added.</returns>
        public virtual TSet AddTo(TSet flags)
        {
            return this.parents.Aggregate(
                flags,
                (current, parent) => parent.AddTo(current)
            );
        }

        /// <summary>
        ///     See <see cref="AddTo(TSet)" />.
        /// </summary>
        public static TSet operator +(TSet value, Flag<TSet> flag)
        {
            return flag.AddTo(value);
        }

        /// <summary>
        ///     Removes a flag if it is present.
        /// </summary>
        /// <param name="flags">The flags to remove it from.</param>
        /// <returns>The modified flags.</returns>
        /// <remarks>
        ///     If <typeparamref name="TSet" /> is reference type, the flags passed to the function may be modified in-place.
        /// </remarks>
        public virtual TSet RemoveFrom(TSet flags)
        {
            return this.children.Aggregate(
                flags,
                (current, child) => child.RemoveFrom(current)
            );
        }

        /// <summary>
        ///     See <see cref="RemoveFrom(TSet)" />
        /// </summary>
        public static TSet operator -(TSet value, Flag<TSet> flag)
        {
            return flag.RemoveFrom(value);
        }

        /// <summary>
        ///     Check whether this flag is in <paramref name="flags" />.
        /// </summary>
        /// <param name="flags">The flags to search in.</param>
        /// <returns>
        ///     <see langword="true" /> if this flag belongs to the set of
        ///     <paramref name="flags" />, otherwise <see langword="false" />.
        /// </returns>
        public virtual bool IsIn(TSet flags)
        {
            return this.parents.All(parent => parent.IsIn(flags));
        }
    }
}