using Multiflag.BuiltInValueTypes;

namespace Multiflag
{
    /// <summary>
    /// A flag based on an enum.<br/>
    /// </summary>
    /// <remarks>
    /// It only works with enums backed by an int, but you can implement
    /// <see cref="IFlagValue{T}"/> to support other enums.
    /// </remarks>
    public class FlagEnum<TEnum> : Flag<TEnum, EnumFlagValue<TEnum>>
    where TEnum : Enum
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public FlagEnum(TEnum value, params FlagEnum<TEnum>[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public FlagEnum(params FlagEnum<TEnum>[] parents) : base(parents) { }
    }
}
