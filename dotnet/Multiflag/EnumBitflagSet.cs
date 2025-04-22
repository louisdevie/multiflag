using System;
using Multiflag.Bitflags;

namespace Multiflag
{
    /// <summary>
    /// Provides bitflags based on dynamic integers (thus allowing any number of flags).
    /// </summary>
    public class EnumBitflagSet<T> : FlagSet<T>
    where T : Enum
    {
        private readonly Type enumType;
        private readonly Type underlyingType;
        private readonly BitManipulator bitManipulator;

        public EnumBitflagSet()
        {
            this.enumType = typeof(T);
            this.underlyingType = this.enumType.GetEnumUnderlyingType();
            this.bitManipulator = FindBitManipulator(this.underlyingType);
        }

        private static BitManipulator FindBitManipulator(Type underlyingType)
        {
            if (underlyingType == typeof(sbyte))
            {
                throw new UnsupportedEnumTypeException(underlyingType, "byte");
            }
            else if (underlyingType == typeof(byte))
            {
                return U8BitManipulator.Current;
            }
            else if (underlyingType == typeof(short))
            {
                throw new UnsupportedEnumTypeException(underlyingType, "ushort");
            }
            else if (underlyingType == typeof(ushort))
            {
                return U16BitManipulator.Current;
            }
            else if (underlyingType == typeof(int))
            {
                throw new UnsupportedEnumTypeException(underlyingType, "uint");
            }
            else if (underlyingType == typeof(uint))
            {
                return U32BitManipulator.Current;
            }
            else if (underlyingType == typeof(long))
            {
                throw new UnsupportedEnumTypeException(underlyingType, "ulong");
            }
            else if (underlyingType == typeof(ulong))
            {
                return U64BitManipulator.Current;
            }
            else
            {
                throw new UnsupportedEnumTypeException(underlyingType);
            }
        }

        private object EnumToInt(object value) => Convert.ChangeType(value, this.underlyingType);

        private T IntToEnum(object value) => (T)Enum.ToObject(this.enumType, value);

        protected override void CheckValue(T value)
        {
            this.bitManipulator.CheckPowerOfTwo(this.EnumToInt(value));

            if (!Enum.IsDefined(this.enumType, value))
            {
                throw new UndefinedEnumValueException(value);
            }
        }

        public override T Empty() => this.IntToEnum(this.bitManipulator.ZeroObject);

        public override bool IsEmpty(T flags) => this.bitManipulator.IsZero(this.EnumToInt(flags));

        public override T Union(T first, T second) =>
            this.IntToEnum(this.bitManipulator.Or(this.EnumToInt(first), this.EnumToInt(second)));

        public override T Difference(T first, T second) =>
            this.IntToEnum(this.bitManipulator.AndNot(this.EnumToInt(first), this.EnumToInt(second)));

        public override bool IsSupersetOf(T first, T second) =>
            this.bitManipulator.AndEquals(this.EnumToInt(first), this.EnumToInt(second));
    }
}