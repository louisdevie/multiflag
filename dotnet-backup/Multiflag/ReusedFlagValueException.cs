using System;

namespace Multiflag
{
    public class ReusedFlagValueException : ArgumentException
    {
        internal ReusedFlagValueException(object value)
            : base($"The flag value {value} is already being used for another flag.")
        {
        }
    }
}