using Multiflag.BuiltInValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiflag
{
    /// <summary>
    /// A flag based on a 32-bit usingned integer (thus allowing 32 different flags).
    /// </summary>
    public class Flag32 : Flag<uint, UIntFlagValue>
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public Flag32(uint value, params Flag32[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public Flag32(params Flag32[] parents) : base(parents) { }
    }

}
