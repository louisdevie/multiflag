using System.Numerics;

namespace Multiflag.Bitflags
{
    internal abstract class BitManipulator
    {
        public abstract void CheckPowerOfTwo(object value);
        
        public abstract object ZeroObject { get; }

        public abstract bool IsZero(object flags);

        public abstract object BitwiseOr(object first, object second);

        public abstract object BitwiseAndNot(object first, object second);

        public abstract bool BitwiseAndEquals(object first, object second);
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

        public override object BitwiseOr(object first, object second) => this.BitwiseOr((T)first, (T)second);
        
        public abstract T BitwiseOr(T first, T second);

        public override object BitwiseAndNot(object first, object second) => this.BitwiseAndNot((T)first, (T)second);
        
        public abstract T BitwiseAndNot(T first, T second);

        public override bool BitwiseAndEquals(object first, object second) => this.BitwiseAndEquals((T)first, (T)second);
        
        public abstract bool BitwiseAndEquals(T first, T second);
    }
}