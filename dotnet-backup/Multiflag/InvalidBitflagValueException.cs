using System;

namespace Multiflag
{
    public class InvalidBitflagValueException : ArgumentException
    {
        internal InvalidBitflagValueException() : base("Flag values for bit flags must be powers of two.")
        {
        }
    }
}