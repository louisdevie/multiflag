using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Multiflag.Base64Format;
using Multiflag.Enumerators;

namespace Multiflag
{
    /// <summary>
    ///     Provides flags that are stored in strings using a little-endian base 64
    ///     representation.
    ///     <br />
    ///     This format is compact, easily serializable and allows for an unlimited
    ///     number of flags, but is specific to Multiflag.
    ///     Use <see cref="CollectionFlagSet{T}" /> instead if you need the data to be
    ///     easily understandable by other systems.
    /// </summary>
    public class Base64BitflagSet : FlagSet<int, string>
    {
        /// <inheritdoc />
        protected override sealed string WrapValue(int value)
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Indices should be greater than or equal to 1.");
            }

            return Base64Codec.EncodeSingleFlag(value);
        }

        /// <inheritdoc />
        public override sealed string Empty()
        {
            return Base64Codec.Zero;
        }

        /// <inheritdoc />
        public override sealed string Union(string first, string second)
        {
            return Base64Codec.BitwiseOr(first, second);
        }

        /// <inheritdoc />
        public override sealed string Intersection(string first, string second)
        {
            return Base64Codec.BitwiseAnd(first, second);
        }

        /// <inheritdoc />
        public override sealed string Difference(string first, string second)
        {
            return Base64Codec.BitwiseAndNot(first, second);
        }

        /// <inheritdoc />
        public override sealed bool IsSupersetOf(string first, string second)
        {
            return Base64Codec.BitwiseAndEquals(first, second);
        }

        /// <inheritdoc />
        public override sealed IEnumerable<int> Iterate(string flags)
        {
            return new Base64FlagEnumerator(flags);
        }
    }
}