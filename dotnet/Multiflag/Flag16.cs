using Multiflag.BuiltInValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiflag
{
    /// <summary>
    /// A flag based on a 16-bit usingned integer (thus allowing 16 different flags).
    /// </summary>
    public class Flag16 : Flag<ushort, UShortFlagValue>
    {
        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(TValue, Flag{TValue, TAdapter}[])"/>
        public Flag16(ushort value, params Flag16[] parents) : base(value, parents) { }

        /// <inheritdoc cref="Flag{TValue, TAdapter}.Flag(Flag{TValue, TAdapter}[])"/>
        public Flag16(params Flag16[] parents) : base(parents) { }
    }

}
