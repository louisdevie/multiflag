using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown when a flag is associated with another one
    ///     that was created from a different <see cref="FlagSet{T}" />.
    /// </summary>
    public class ForeignFlagException : ArgumentException
    {
        internal ForeignFlagException() : base(
            "Cannot create a dependency between two flags created in different sets.")
        {
        }
    }
}