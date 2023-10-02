using System.ComponentModel;
using System.Diagnostics;

namespace Multiflag
{
    /// <summary>
    /// A generic flag.<br/>
    /// Either use one of the built-in implementations, or inherit this class
    /// to support custom flags. The <typeparamref name="TAdapter"/>'s job is
    /// to implement certain methods for <typeparamref name="TValue"/> (see
    /// <see cref="IFlagValue{T}"/>).<br/>
    /// All flags can have parent/child relationships with others flags of the
    /// same type : for a flag to be included in a flag value, all of its
    /// parents must be too. That means adding a flag will add all its parents
    /// along with it, and removing one removes all of its children.
    /// </summary>
    /// <typeparam name="TValue">The type of the flag values.</typeparam>
    /// <typeparam name="TAdapter">The adapter that enables a <typeparamref name="TValue"/> to be used for flags.</typeparam>
    public class Flag<TValue, TAdapter> where TAdapter : IFlagValue<TValue>, new()
    {
        private TValue value;
        private TAdapter adapter;
        private HashSet<Flag<TValue, TAdapter>> parents, children;

        /// <summary>
        /// Create a new flag with a value.
        /// </summary>
        /// <param name="value">The value of the flag.</param>
        /// <param name="parents">The parent flags. See the class' documentation for details about parents.</param>
        public Flag(TValue value, params Flag<TValue, TAdapter>[] parents)
        {
            this.value = value;
            this.adapter = new();
            this.parents = parents.ToHashSet();
            this.children = new();

            foreach (var parent in this.parents)
            {
                parent.children.Add(this);
            }
        }

        /// <summary>
        /// Create a flag with no value on its own.
        /// </summary>
        /// <param name="parents">The parent flags. See the class' documentation for details about parents.</param>
        public Flag(params Flag<TValue, TAdapter>[] parents)
        {
            this.adapter = new();
            this.value = this.adapter.Nothing();
            this.parents = parents.ToHashSet();
            this.children = new();

            foreach (var parent in this.parents)
            {
                parent.children.Add(this);
            }
        }

        /// <summary>
        /// Add a flag if it is not already present.
        /// </summary>
        /// <param name="flags">The flags to add it to.</param>
        /// <returns>The modified flags.</returns>
        /// <remarks>
        /// If <typeparamref name="TValue"/> is reference type, the flags passed to the function may be modified in-place.
        /// </remarks>
        public TValue AddTo(TValue flags)
        {
            TValue newValue = this.adapter.Union(flags, this.value);
            foreach (var parent in this.parents)
            {
                newValue = parent.AddTo(newValue);
            }
            return newValue;
        }

        /// <summary>
        /// See <see cref="AddTo(TValue)"/>.
        /// </summary>
        public static TValue operator +(TValue value, Flag<TValue, TAdapter> flag)
        {
            return flag.AddTo(value);
        }

        /// <summary>
        /// Removes a flag if it is present.
        /// </summary>
        /// <param name="flags">The flags to remove it from.</param>
        /// <returns>The modified flags.</returns>
        /// <remarks>
        /// If <typeparamref name="TValue"/> is reference type, the flags passed to the function may be modified in-place.
        /// </remarks>
        public TValue RemoveFrom(TValue flags)
        {
            TValue newValue = this.adapter.Substraction(flags, this.value);
            foreach (var child in this.children)
            {
                newValue = child.RemoveFrom(newValue);
            }
            return newValue;
        }

        /// <summary>
        /// See <see cref="RemoveFrom(TValue)"/>
        /// </summary>
        public static TValue operator -(TValue value, Flag<TValue, TAdapter> flag)
        {
            return flag.RemoveFrom(value);
        }

        /// <summary>
        /// Check wether that flag is included in <paramref name="flags"/>.
        /// </summary>
        /// <param name="flags">The flags to search in.</param>
        /// <returns>
        /// <see langword="true"/> if this flag is included in
        /// <paramref name="flags"/>, otherwise <see langword="false"/>.
        /// </returns>
        public bool In(TValue flags)
        {
            return this.adapter.Includes(flags, this.value) && this.parents.All(parent => parent.In(flags));
        }

        /// <summary>
        /// <see langword="true"/> when this flag has no value and its parents are « nothing » too.
        /// </summary>
        /// <remarks>
        /// If this is <see langword="true"/>, IsConcrete and IsAbstract are <see langword="false"/>.
        /// </remarks>
        public bool IsNothing => this.adapter.Equivalent(this.value, this.adapter.Nothing())
                              && this.parents.All(parent => parent.IsNothing);

        /// <summary>
        /// <see langword="true"/> when this flag has a value.
        /// </summary>
        /// <remarks>
        /// If this is <see langword="true"/>, IsNothing and IsAbstract are <see langword="false"/>.
        /// </remarks>
        public bool IsConcrete => !this.adapter.Equivalent(this.value, this.adapter.Nothing());

        /// <summary>
        /// <see langword="true"/> when this flag has no value but some parents.
        /// </summary>
        /// <remarks>
        /// If this is <see langword="true"/>, IsNothing and IsConcrete are <see langword="false"/>.
        /// </remarks>

        public bool IsAbstract => this.adapter.Equivalent(this.value, this.adapter.Nothing())
                               && this.parents.Any(parent => !parent.IsNothing);

        /// <summary>
        /// Determines wether the two flags are equivalent when added to a set.
        /// </summary>
        /// <param name="other">The other flag to compare with this one.</param>
        /// <returns>
        /// <see langword="true"/> if the two flags are equivalent, <see langword="false"/> otherwise.
        /// </returns>
        public bool PositiveEquals(Flag<TValue, TAdapter> other)
        {
            TValue addedThis = this.AddTo(this.adapter.Nothing());
            TValue addedOther = other.AddTo(this.adapter.Nothing());

            return this.adapter.Equivalent(addedThis, addedOther);
        }

        private TValue NegativeUnionWith(TValue value)
        {
            value = this.adapter.Union(value, this.value);
            foreach (var child in this.children)
            {
                child.NegativeUnionWith(value);
            }
            return value;
        }

        /// <summary>
        /// Determines wether the two flags are equivalent when removed from a set.
        /// </summary>
        /// <param name="other">The other flag to compare with this one.</param>
        /// <returns>
        /// <see langword="true"/> if the two flags are equivalent, <see langword="false"/> otherwise.
        /// </returns>
        public bool NegativeEquals(Flag<TValue, TAdapter> other)
        {
            TValue removedThis = this.NegativeUnionWith(this.adapter.Nothing());
            TValue removedOther = other.NegativeUnionWith(this.adapter.Nothing());

            return this.adapter.Equivalent(removedThis, removedOther);
        }

        /// <inheritdoc cref="Equals(object?)"/>
        /// <remarks>
        /// The equality is checked using both PositiveEquals and NegativeEquals.
        /// </remarks>
        public bool Equals(Flag<TValue, TAdapter> other)
        {
            return this.PositiveEquals(other) && this.NegativeEquals(other);
        }

        public override bool Equals(object? obj)
        {
            return obj is Flag<TValue, TAdapter> other
                && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.value!.GetHashCode();
        }
    }

    public static class FlagExtensions
    {
        /// <summary>
        /// Check wether a flag is included in that value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to check.</typeparam>
        /// <typeparam name="TAdapter">A corresponding adapter.</typeparam>
        /// <param name="flag">The flag to check.</param>
        /// <returns>
        /// <see langword="true"/> if the flag is included, otherwise
        /// <see langword="false"/>.
        /// </returns>
        public static bool Includes<TValue, TAdapter>(this TValue value, Flag<TValue, TAdapter> flag)
        where TAdapter : IFlagValue<TValue>, new()
        {
            return flag.In(value);
        }
    }
}