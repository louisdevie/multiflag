namespace Multiflag.Bitflags
{
    internal interface IBitManipulator
    {
        public object Zero { get; }
        
        public object One { get; }
        
        public void CheckPowerOfTwo(object value);
        
        public bool IsZero(object value);

        public bool IsEven(object value);

        public object ShiftLeft(object value);

        public object ShiftRight(object value);

        public object BitwiseOr(object first, object second);
        
        public object BitwiseAnd(object first, object second);

        public object BitwiseAndNot(object first, object second);

        public bool BitwiseAndEquals(object first, object second);
    }
}