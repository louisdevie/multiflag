using System;

namespace Multiflag
{
    public class UndefinedEnumValueException : ArgumentException
    {
        internal UndefinedEnumValueException(object badValue)
            : base($"The value {badValue} cannot be used as a flag because it isn't a member of the enum.")
        {
        }
    }
}