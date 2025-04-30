using System.Numerics;

namespace Multiflag.Bitflags
{
    internal abstract class BitManipulator<T> : IBitManipulator
    where T : notnull
    {
        public abstract T Zero { get; }

        object IBitManipulator.Zero => this.Zero;
        
        public abstract T One { get; }

        object IBitManipulator.One => this.One;
        
        public void CheckPowerOfTwo(T value)
        {
            if (!this.IsPowerOfTwo(value))
            {
                throw new InvalidBitflagValueException();
            }
        }

        public void CheckPowerOfTwo(object value) => this.CheckPowerOfTwo((T)value);
        
        public abstract bool IsZero(T value);

        public bool IsZero(object value) => this.IsZero((T)value);

        public abstract bool IsEven(T value);
        
        public bool IsEven(object value) => this.IsEven((T)value);

        protected abstract bool IsPowerOfTwo(T value);

        public abstract T ShiftLeft(T value);
        
        public object ShiftLeft(object value) => this.ShiftLeft((T) value);

        public abstract T ShiftRight(T value);
        
        public object ShiftRight(object value) => this.ShiftRight((T) value);

        public abstract T BitwiseOr(T first, T second);
        
        public object BitwiseOr(object first, object second) => this.BitwiseOr((T)first, (T)second);
        
        public abstract T BitwiseAnd(T first, T second);
        
        public object BitwiseAnd(object first, object second) => this.BitwiseAnd((T)first, (T)second);

        public abstract T BitwiseAndNot(T first, T second);
        
        public object BitwiseAndNot(object first, object second) => this.BitwiseAndNot((T)first, (T)second);

        public abstract bool BitwiseAndEquals(T first, T second);
        
        public bool BitwiseAndEquals(object first, object second) => this.BitwiseAndEquals((T)first, (T)second);
    }
}