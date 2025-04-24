using System;
using Multiflag.Base64Format;

namespace Multiflag
{
    /// <summary>
    ///     Provides flags that are stored in strings using a little-endian base 64
    ///     representation.
    ///     <br />
    ///     This format is compact, easily serializable and allows for an unlimited
    ///     number of flags, but is specific to Multiflag.
    ///     Use <see cref="HashedFlagSet{T}" /> instead if you need the data to be
    ///     easily understandable by other systems.
    /// </summary>
    public class Base64BitflagSet : FlagSet<string>
    {
        /// <summary>
        ///     Creates a flag from an index.
        ///     The value of the flag will be 2 to the power of <paramref name="index" />.
        /// </summary>
        /// <param name="index">The index of the flag. It must be greater than or equal to one.</param>
        /// <param name="parents">Other flags required for this flag to be set.</param>
        /// <returns>A flag bound to this set.</returns>
        /// <exception cref="ForeignFlagException">
        ///     If one of the parents doesn't belong to the same <see cref="FlagSet{T}" />.
        /// </exception>
        /// <exception cref="ReusedFlagValueException">
        ///     If another flag has already been created with the same index.
        /// </exception>
        public Flag<string> Flag(int index, params Flag<string>[] parents)
        {
            if (index < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Indices should be greater than or equal to 1.");
            }

            return this.Flag(Base64Codec.EncodeSingleFlag(index), parents);
        }

        /// <inheritdoc />
        protected override sealed void CheckValue(string value)
        {
            if (!Base64Codec.DecodesToSingleFlag(value))
            {
                throw new InvalidBitflagValueException();
            }
        }

        /// <inheritdoc />
        public override sealed string Empty()
        {
            return Base64Codec.Zero;
        }

        /// <inheritdoc />
        public override sealed bool IsEmpty(string flags)
        {
            return Base64Codec.DecodesToZero(flags);
        }

        /// <inheritdoc />
        public override sealed string Union(string first, string second)
        {
            return Base64Codec.BitwiseOr(first, second);
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
    }
}