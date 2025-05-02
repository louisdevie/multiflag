using System;
using System.Collections;
using System.Collections.Generic;
using Multiflag.Base64Format;

namespace Multiflag.Enumerators
{
    internal class Base64FlagEnumerator : IEnumerable<int>, IEnumerator<int>
    {
        private readonly string value;
        private int currentByte;
        private int currentBit;

        public Base64FlagEnumerator(string value)
        {
            this.value = value;
            this.currentByte = 0;
            this.currentBit = 0;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new Base64FlagEnumerator(this.value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
        }

        private bool MoveNextByte() {
            // next multiple of 6
            var index = (int)Math.Ceiling(this.currentBit / 6.0);

            // skip bytes equal to zero
            while (
                index < this.value.Length
                && this.value[index] == Base64Codec.ZERO)
            {
                index++;
            }

            if (index < this.value.Length)
            {
                // found a non-zero byte
                this.currentByte = Base64Codec.DecodeByte(this.value[index]);
                this.currentBit = index * 6;
                return true;
            }
            else
            {
                // reached the end of the string
                return false;
            }
        }

        public bool MoveNext()
        {
            if (this.currentByte == 0)
            {
                if (!this.MoveNextByte())
                {
                    return false;
                }
            }

            while ((this.currentByte & 1) == 0)
            {
                this.currentByte >>= 1;
                this.currentBit += 1;
            }

            this.currentByte >>= 1;
            this.currentBit += 1;

            return true;
        }

        public void Reset()
        {
            this.currentByte = 0;
            this.currentBit = 0;
        }

        public int Current => this.currentBit;

        object IEnumerator.Current => this.Current;
    }
}