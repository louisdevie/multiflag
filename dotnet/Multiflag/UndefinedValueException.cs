using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown when a flag is created from an <see cref="EnumBitflagSet{T}" /> with a value
    ///     that is not explicitly declared in the enum.
    /// </summary>
    public class UndefinedEnumValueException : ArgumentException
    {
        internal UndefinedEnumValueException(object badValue)
            : base($"The value {badValue} cannot be used as a flag because it isn't a member of the enum.")
        {
        }
    }
}