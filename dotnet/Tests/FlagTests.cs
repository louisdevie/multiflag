namespace Multiflag.Tests;

public class FlagTests
{
    [Fact]
    public void AddTo()
    {
        Assert.Equal(1, MyBitflags.Current.FlagA.AddTo(0));
        Assert.Equal(3, MyBitflags.Current.FlagA.AddTo(2));
        Assert.Equal(7, MyBitflags.Current.FlagA.AddTo(7));

        Assert.Equal(2, MyBitflags.Current.FlagB.AddTo(0));
        Assert.Equal(2, MyBitflags.Current.FlagB.AddTo(2));
        Assert.Equal(7, MyBitflags.Current.FlagB.AddTo(7));

        Assert.Equal(3, MyBitflags.Current.FlagC.AddTo(0));
        Assert.Equal(3, MyBitflags.Current.FlagC.AddTo(2));
        Assert.Equal(7, MyBitflags.Current.FlagC.AddTo(7));

        Assert.Equal(7, MyBitflags.Current.FlagD.AddTo(2));
        Assert.Equal(5, MyBitflags.Current.FlagD.AddTo(0));
        Assert.Equal(7, MyBitflags.Current.FlagD.AddTo(7));
    }

    [Fact]
    public void Addition()
    {
        Assert.Equal(1, 0 + MyBitflags.Current.FlagA);
        Assert.Equal(3, 2 + MyBitflags.Current.FlagA);
        Assert.Equal(7, 7 + MyBitflags.Current.FlagA);

        Assert.Equal(2, 0 + MyBitflags.Current.FlagB);
        Assert.Equal(2, 2 + MyBitflags.Current.FlagB);
        Assert.Equal(7, 7 + MyBitflags.Current.FlagB);

        Assert.Equal(3, 0 + MyBitflags.Current.FlagC);
        Assert.Equal(3, 2 + MyBitflags.Current.FlagC);
        Assert.Equal(7, 7 + MyBitflags.Current.FlagC);

        Assert.Equal(7, 2 + MyBitflags.Current.FlagD);
        Assert.Equal(5, 0 + MyBitflags.Current.FlagD);
        Assert.Equal(7, 7 + MyBitflags.Current.FlagD);
    }

    [Fact]
    public void RemoveFrom()
    {
        Assert.Equal(0, MyBitflags.Current.FlagA.RemoveFrom(0));
        Assert.Equal(2, MyBitflags.Current.FlagA.RemoveFrom(2));
        Assert.Equal(2, MyBitflags.Current.FlagA.RemoveFrom(7));

        Assert.Equal(0, MyBitflags.Current.FlagB.RemoveFrom(0));
        Assert.Equal(0, MyBitflags.Current.FlagB.RemoveFrom(2));
        Assert.Equal(5, MyBitflags.Current.FlagB.RemoveFrom(7));

        Assert.Equal(0, MyBitflags.Current.FlagC.RemoveFrom(0));
        Assert.Equal(2, MyBitflags.Current.FlagC.RemoveFrom(2));
        Assert.Equal(7, MyBitflags.Current.FlagC.RemoveFrom(7));

        Assert.Equal(2, MyBitflags.Current.FlagD.RemoveFrom(2));
        Assert.Equal(0, MyBitflags.Current.FlagD.RemoveFrom(0));
        Assert.Equal(3, MyBitflags.Current.FlagD.RemoveFrom(7));
    }

    [Fact]
    public void Subtraction()
    {
        Assert.Equal(0, 0 - MyBitflags.Current.FlagA);
        Assert.Equal(2, 2 - MyBitflags.Current.FlagA);
        Assert.Equal(2, 7 - MyBitflags.Current.FlagA);

        Assert.Equal(0, 0 - MyBitflags.Current.FlagB);
        Assert.Equal(0, 2 - MyBitflags.Current.FlagB);
        Assert.Equal(5, 7 - MyBitflags.Current.FlagB);

        Assert.Equal(0, 0 - MyBitflags.Current.FlagC);
        Assert.Equal(2, 2 - MyBitflags.Current.FlagC);
        Assert.Equal(7, 7 - MyBitflags.Current.FlagC);

        Assert.Equal(0, 0 - MyBitflags.Current.FlagD);
        Assert.Equal(2, 2 - MyBitflags.Current.FlagD);
        Assert.Equal(3, 7 - MyBitflags.Current.FlagD);
    }

    [Fact]
    public void IsIn()
    {
        Assert.False(MyBitflags.Current.FlagA.IsIn(0));
        Assert.True(MyBitflags.Current.FlagA.IsIn(1));
        Assert.False(MyBitflags.Current.FlagA.IsIn(4));
        Assert.True(MyBitflags.Current.FlagA.IsIn(7));

        Assert.False(MyBitflags.Current.FlagB.IsIn(0));
        Assert.True(MyBitflags.Current.FlagB.IsIn(2));
        Assert.False(MyBitflags.Current.FlagB.IsIn(4));
        Assert.True(MyBitflags.Current.FlagB.IsIn(7));

        Assert.False(MyBitflags.Current.FlagC.IsIn(0));
        Assert.False(MyBitflags.Current.FlagC.IsIn(1));
        Assert.False(MyBitflags.Current.FlagC.IsIn(2));
        Assert.True(MyBitflags.Current.FlagC.IsIn(3));
        Assert.False(MyBitflags.Current.FlagC.IsIn(4));
        Assert.True(MyBitflags.Current.FlagC.IsIn(7));

        Assert.False(MyBitflags.Current.FlagD.IsIn(0));
        Assert.False(MyBitflags.Current.FlagD.IsIn(1));
        Assert.False(MyBitflags.Current.FlagD.IsIn(4));
        Assert.True(MyBitflags.Current.FlagD.IsIn(5));
        Assert.False(MyBitflags.Current.FlagD.IsIn(6));
        Assert.True(MyBitflags.Current.FlagD.IsIn(7));
    }

    [Fact]
    public void IsAbstract()
    {
        Assert.False(MyBitflags.Current.FlagA.IsAbstract);
        Assert.False(MyBitflags.Current.FlagB.IsAbstract);
        Assert.True(MyBitflags.Current.FlagC.IsAbstract);
        Assert.False(MyBitflags.Current.FlagD.IsAbstract);
    }

    private class MyBitflags : U8BitflagSet
    {
        private static MyBitflags? current;

        private MyBitflags()
        {
            this.FlagA = this.Flag(1);
            this.FlagB = this.Flag(2);
            this.FlagC = this.Flag(this.FlagA, this.FlagB);
            this.FlagD = this.Flag(4, this.FlagA);
        }

        public static MyBitflags Current => current ??= new MyBitflags();

        public Flag<byte> FlagA { get; }
        public Flag<byte> FlagB { get; }
        public Flag<byte> FlagC { get; }
        public Flag<byte> FlagD { get; }
    }
}