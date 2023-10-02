using Multiflag.BuiltInValueTypes;

namespace Multiflag
{
    /// <summary>
    /// A flag based on a <see cref="HashSet{T}"/>. This allows you to use
    /// pretty much anything as a flag value.
    /// </summary>
    /// <typeparam name="T">The type of the values in the set.</typeparam>
    /// <remarks>
    /// The set is modified in-place. If you do not want this behavior, make a
    /// copy of your set before doing any flag operations on it.
    /// </remarks>
    public class FlagSet<T> : Flag<HashSet<T>, SetFlagValue<T>>
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public FlagSet(HashSet<T> value, params FlagSet<T>[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public FlagSet(T value, params FlagSet<T>[] parents) : base(new HashSet<T> { value }, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public FlagSet(params FlagSet<T>[] parents) : base(parents) { }
    }
}
