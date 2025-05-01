using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown by <c>FlagSet</c>s that represents the flags using hash sets when a flag value
    ///     does not contain exactly one element.
    /// </summary>
    public class InvalidHashSetValueException : ArgumentException
    {
        internal InvalidHashSetValueException() : base("Flag values for hash sets must contain exactly one value.")
        {
        }
    }
}