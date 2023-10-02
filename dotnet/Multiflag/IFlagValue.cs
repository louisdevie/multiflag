namespace Multiflag
{
    /// <summary>
    /// An adapter to use a regular type as a flag. Built-in implementations
    /// exist for <see cref="byte"/>, <see cref="ushort"/>, <see cref="uint"/>,
    /// <see cref="ulong"/>, <see cref="Enum"/>s and <see cref="HashSet{T}"/>.
    /// <br/>
    /// For other integers and custom enumerations, it can easily be implented
    /// as such:
    /// <code>
    ///   Nothing() => 0
    ///   Union(a, b) => a | b
    ///   Substraction => a &amp; ~b
    ///   Includes => (a &amp; b) == b
    ///   Equivalent => a == b
    /// </code>
    /// </summary>
    /// <typeparam name="T">The type to be used as a flag.</typeparam>
    public interface IFlagValue<T>
    {
        /// <summary>
        /// Creates an empty set of flags that can only include itself.
        /// </summary>
        T Nothing();

        /// <summary>
        /// Returns the union of two sets of flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        /// <remarks>
        /// If <typeparamref name="T"/> is a reference type, the operation may
        /// be done in-place on the first set.<br/>That is, the first parameter is
        /// should not be reused after being passed to that function.
        /// </remarks>
        T Union(T first, T second);

        /// <summary>
        /// Returns the substraction af one set of flags by another.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags that is substracted to the first.</param>
        /// <remarks>
        /// If <typeparamref name="T"/> is a reference type, the operation may
        /// be done in-place on the first set.<br/>That is, the first parameter is
        /// should not be reused after being passed to that function.
        /// </remarks>
        T Substraction(T first, T second);

        /// <summary>
        /// Checks wether the first set of flags is a superset of the second.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        bool Includes(T first, T second);

        /// <summary>
        /// Checks wether two sets of flags contains exactly the same same flags.
        /// </summary>
        /// <param name="first">The first set of flags.</param>
        /// <param name="second">The second set of flags.</param>
        bool Equivalent(T first, T second);
    }
}