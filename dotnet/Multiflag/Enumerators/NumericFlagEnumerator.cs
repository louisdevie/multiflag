using System.Collections;
using System.Collections.Generic;
using Multiflag.Bitflags;

namespace Multiflag.Enumerators
{
    internal class NumericFlagEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    where T : notnull
    {
        private readonly T value;
        private readonly BitManipulator<T> bitManipulator;
        private T remaining;
        private T current;

        public NumericFlagEnumerator(BitManipulator<T> bitManipulator, T value)
        {
            this.value = value;
            this.bitManipulator = bitManipulator;
            this.remaining = value;
            this.current = this.bitManipulator.One;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new NumericFlagEnumerator<T>(this.bitManipulator, this.value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool MoveNext()
        {
            if (this.bitManipulator.IsZero(this.remaining))
            {
                return false;
            }

            // move to the next bit
            while (this.bitManipulator.IsEven(this.remaining))
            {
                this.remaining = this.bitManipulator.ShiftRight(this.remaining);
                this.current = this.bitManipulator.ShiftLeft(this.remaining);
            }

            // discard this bit
            this.remaining = this.bitManipulator.BitwiseAndNot(
                this.remaining,
                this.bitManipulator.One
            );

            return true;
        }

        public void Reset()
        {
            this.remaining = this.value;
            this.current = this.bitManipulator.One;
        }

        public T Current => this.current;

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }
    }
}