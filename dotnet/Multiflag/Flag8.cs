using Multiflag.BuiltInValueTypes;

namespace Multiflag
{
    /// <summary>
    /// A flag based on a 8-bit usingned integer (thus allowing 8 different flags).
    /// </summary>
    public class Flag8 : Flag<byte, ByteFlagValue>
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public Flag8(byte value, params Flag8[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public Flag8(params Flag8[] parents) : base(parents) { }
    }
}
