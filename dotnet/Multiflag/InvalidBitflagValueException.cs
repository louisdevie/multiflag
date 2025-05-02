using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown by <c>FlagSet</c>s that represents the flags using a binary format when
    ///     a flag value isn't a power of two.
    /// </summary>
    public class InvalidBitflagValueException : ArgumentException
    {
        internal InvalidBitflagValueException() : base("Flag values for bit flags must be powers of two.")
        {
        }
    }
}