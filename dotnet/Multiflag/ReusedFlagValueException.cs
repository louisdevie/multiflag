using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown if the <see cref="FlagSet{T}.Flag(T, ValueFlag{T}[])" /> method is called
    ///     with a value that was already used for another flag in the same <see cref="FlagSet{T}" />.
    /// </summary>
    public class ReusedFlagValueException : ArgumentException
    {
        internal ReusedFlagValueException(object value)
            : base($"The flag value {value} is already being used for another flag.")
        {
        }
    }
}