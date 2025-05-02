using System;
using System.Collections;
using System.Collections.Generic;
using Multiflag.Bitflags;

namespace Multiflag.Enumerators
{
    internal class EnumFlagEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly object value;
        private readonly IBitManipulator bitManipulator;
        private object remaining;
        private object current;

        public EnumFlagEnumerator(IBitManipulator bitManipulator, object value)
        {
            this.value = value;
            this.bitManipulator = bitManipulator;
            this.remaining = value;
            this.current = this.bitManipulator.One;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumFlagEnumerator<T>(this.bitManipulator, this.value);
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
                this.current = this.bitManipulator.ShiftLeft(this.current);
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

        T IEnumerator<T>.Current => (T)this.Current;

        public object Current => Enum.ToObject(typeof(T), this.current);

        public void Dispose()
        {
        }
    }
}