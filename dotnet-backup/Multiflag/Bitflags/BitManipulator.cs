using System.Numerics;

namespace Multiflag.Bitflags
{
    internal abstract class BitManipulator
    {
        public abstract void CheckPowerOfTwo(object value);
        
        public abstract object ZeroObject { get; }

        public abstract bool IsZero(object flags);

        public abstract object Or(object first, object second);

        public abstract object AndNot(object first, object second);

        public abstract bool AndEquals(object first, object second);
    }
    
    internal abstract class BitManipulator<T> : BitManipulator
    where T : notnull
    {
        public override void CheckPowerOfTwo(object value) => this.CheckPowerOfTwo((T)value);
        
        public void CheckPowerOfTwo(T value)
        {
            if (!this.IsPowerOfTwo(value))
            {
                throw new InvalidBitflagValueException();
            }
        }

        protected abstract bool IsPowerOfTwo(T value);
        
        public override object ZeroObject => this.Zero;

        public abstract T Zero { get; }

        public override bool IsZero(object flags) => this.IsZero((T)flags);
        
        public abstract bool IsZero(T flags);

        public override object Or(object first, object second) => this.Or((T)first, (T)second);
        
        public abstract T Or(T first, T second);

        public override object AndNot(object first, object second) => this.AndNot((T)first, (T)second);
        
        public abstract T AndNot(T first, T second);

        public override bool AndEquals(object first, object second) => this.AndEquals((T)first, (T)second);
        
        public abstract bool AndEquals(T first, T second);
    }
}