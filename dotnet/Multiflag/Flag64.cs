using Multiflag.BuiltInValueTypes;

namespace Multiflag
{
    /// <summary>
    /// A flag based on a 64-bit usingned integer (thus allowing 64 different flags).
    /// </summary>
    public class Flag64 : Flag<ulong, ULongFlagValue>
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public Flag64(ulong value, params Flag64[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public Flag64(params Flag64[] parents) : base(parents) { }
    }
}
