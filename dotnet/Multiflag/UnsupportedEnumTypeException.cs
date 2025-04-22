using System;

namespace Multiflag
{
    public class UnsupportedEnumTypeException : ArgumentException
    {
        internal UnsupportedEnumTypeException(Type underlyingType)
            : base($"Enums with an underlying type of {underlyingType} are not supported. Use one of byte, ushort, uint or ulong instead.")
        {
        }
        
        internal UnsupportedEnumTypeException(Type underlyingType, string suggestedType)
            : base($"Enums with an underlying type of {underlyingType} are not supported. Use {suggestedType} instead.")
        {
        }
    }
}