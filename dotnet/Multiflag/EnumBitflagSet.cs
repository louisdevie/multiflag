using System;
using System.Collections.Generic;
using Multiflag.Bitflags;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides bitflags based on dynamic integers (thus allowing any number of flags).
    /// </summary>
    public class EnumBitflagSet<T> : FlagSet<T, T>
    where T : Enum
    {
        private readonly IBitManipulator bitManipulator;
        private readonly Type enumType;
        private readonly Type underlyingType;

        /// <summary>
        ///     Creates a new empty flag set for a specific enum type.
        /// </summary>
        /// <exception cref="UnsupportedEnumTypeException">
        ///     If <typeparamref name="T" /> is not backed by one of <see cref="byte" />,
        ///     <see cref="ushort" />, <see cref="uint" /> or <see cref="ulong" />.
        /// </exception>
        public EnumBitflagSet()
        {
            this.enumType = typeof(T);
            this.underlyingType = this.enumType.GetEnumUnderlyingType();
            this.bitManipulator = FindBitManipulator(this.underlyingType);
        }

        private static IBitManipulator FindBitManipulator(Type underlyingType)
        {
            if (underlyingType == typeof(uint))
            {
                return U32BitManipulator.Current;
            }
            else if (underlyingType == typeof(ulong))
            {
                return U64BitManipulator.Current;
            }
            else if (underlyingType == typeof(long))
            {
                throw new UnsupportedEnumTypeException(underlyingType, "ulong");
            } 
            else
            {
                throw new UnsupportedEnumTypeException(underlyingType, "uint");
            }
        }

        private object EnumToInt(object value)
        {
            return Convert.ChangeType(value, this.underlyingType);
        }

        private T IntToEnum(object value)
        {
            return (T)Enum.ToObject(this.enumType, value);
        }

        /// <inheritdoc />
        protected override sealed T WrapValue(T value)
        {
            this.bitManipulator.CheckPowerOfTwo(this.EnumToInt(value));

            if (!Enum.IsDefined(this.enumType, value))
            {
                throw new UndefinedEnumValueException(value);
            }

            return value;
        }

        /// <inheritdoc />
        public override sealed T Empty()
        {
            return this.IntToEnum(this.bitManipulator.Zero);
        }

        /// <inheritdoc />
        public override sealed T Union(T first, T second)
        {
            return this.IntToEnum(
                this.bitManipulator.BitwiseOr(
                    this.EnumToInt(first),
                    this.EnumToInt(second)
                )
            );
        }

        /// <inheritdoc />
        public override sealed T Intersection(T first, T second)
        {
            return this.IntToEnum(
                this.bitManipulator.BitwiseAnd(
                    this.EnumToInt(first),
                    this.EnumToInt(second)
                )
            );
        }

        /// <inheritdoc />
        public override sealed T Difference(T first, T second)
        {
            return this.IntToEnum(
                this.bitManipulator.BitwiseAndNot(
                    this.EnumToInt(first),
                    this.EnumToInt(second)
                )
            );
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(T first, T second)
        {
            return this.bitManipulator.BitwiseAndEquals(
                this.EnumToInt(first),
                this.EnumToInt(second)
            );
        }

        /// <inheritdoc />
        public override IEnumerable<T> Iterate(T flags)
        {
            return new EnumFlagEnumerator<T>(
                this.bitManipulator,
                flags
            );
        }
    }
}