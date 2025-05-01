namespace Multiflag.BuiltInValueTypes
{
    public class InvalidEnumTypeException : Exception
    {
        public InvalidEnumTypeException(string message) : base(message) { }
    }

    /// <summary>
    /// Built-in implementation of <see cref="IFlagValue{T}"/> for <see cref="Enum"/>s backed by <see cref="int"/>.
    /// </summary>
    public class EnumFlagValue<TEnum> : IFlagValue<TEnum> where TEnum : Enum
    {
        public EnumFlagValue()
        {
            if (!typeof(int).IsAssignableFrom(Enum.GetUnderlyingType(typeof(TEnum))))
                throw new InvalidEnumTypeException("FlagEnums are only compatible with int-backed enums.");
        }

        private int EnumToInt(TEnum value)
        {
            return (int)(object)value;
        }

        private TEnum IntToEnum(int value)
        {
            return (TEnum)(object)value;
        }

        public TEnum Substraction(TEnum first, TEnum second)
        {
            int intFirst = EnumToInt(first);
            int intSecond = EnumToInt(second);
            return IntToEnum(intFirst & ~intSecond);
        }

        public bool Includes(TEnum first, TEnum second)
        {
            int intFirst = EnumToInt(first);
            int intSecond = EnumToInt(second);
            return (intFirst & intSecond) == intSecond;
        }

        public TEnum Union(TEnum first, TEnum second)
        {
            int intFirst = EnumToInt(first);
            int intSecond = EnumToInt(second);
            return IntToEnum(intFirst | intSecond);
        }

        public TEnum Nothing() => IntToEnum(0);

        public bool Equivalent(TEnum first, TEnum second) => first.Equals(second);
    }
}