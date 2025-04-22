using System;

namespace Multiflag
{
    public class ForeignFlagException : ArgumentException
    {
        internal ForeignFlagException() : base(
            "Cannot create a dependency between two flags created in different sets.")
        {
        }
    }
}