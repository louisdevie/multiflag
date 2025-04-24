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
    public class Base64FlagSet : FlagSet<string>
    {
        /// <summary>
        ///     Creates a flag from an index (starting from 1).
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

        protected override void CheckValue(string value)
        {
            if (!Base64Codec.DecodesToSingleFlag(value))
            {
                throw new InvalidBitflagValueException();
            }
        }

        public override string Empty()
        {
            return Base64Codec.Zero;
        }

        public override bool IsEmpty(string flags)
        {
            return Base64Codec.DecodesToZero(flags);
        }

        public override string Union(string first, string second)
        {
            return Base64Codec.BitwiseOr(first, second);
        }

        public override string Difference(string first, string second)
        {
            return Base64Codec.BitwiseAndNot(first, second);
        }

        public override bool IsSupersetOf(string first, string second)
        {
            return Base64Codec.BitwiseAndEquals(first, second);
        }
    }
}