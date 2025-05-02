using System;

namespace Multiflag
{
    /// <summary>
    ///     Exception thrown when an <see cref="EnumBitflagSet{T}" /> is instantiated with an enumeration type
    ///     that is not backed by one of <see cref="byte" />, <see cref="ushort" />, <see cref="uint" /> or
    ///     <see cref="ulong" />.
    /// </summary>
    public class UnsupportedEnumTypeException : ArgumentException
    {
        internal UnsupportedEnumTypeException(Type underlyingType, string suggestedType)
            : base($"Enums with an underlying type of {underlyingType} are not supported. Use {suggestedType} instead.")
        {
        }
    }
}