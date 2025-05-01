namespace Multiflag.Tests;

public class U8BitflagSetTests
{
    [Fact]
    public void ValueNotAPowerOfTwo()
    {
        var flags = new U8BitflagSet();
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(0));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(11));
    }

    [Fact]
    public void Add()
    {
        var flags = new U8BitflagSet();
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4);
        var flags2And4 = flags.Flag(flag2, flag4);

        Assert.Equal(3, 1 + flag2);
        Assert.Equal(5, 1 + flag4);
        Assert.Equal(7, 1 + flags2And4);
    }

    [Fact]
    public void Remove()
    {
        var flags = new U8BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4, flag1);

        Assert.Equal(2, 7 - flag1);
        Assert.Equal(5, 7 - flag2);
        Assert.Equal(3, 7 - flag4);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new U8BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4, flag1);

        Assert.True(flag1.IsIn(1));
        Assert.True(flag2.IsIn(3));
        Assert.False(flag4.IsIn(4));
        Assert.True(flag4.IsIn(5));
    }

    [Fact]
    public void IsAbstract()
    {
        var flags = new U8BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flags1And2 = flags.Flag(flag1, flag2);
        var flag4 = flags.Flag(4, flags1And2);

        Assert.False(flag1.IsAbstract);
        Assert.False(flag2.IsAbstract);
        Assert.True(flags1And2.IsAbstract);
        Assert.False(flag4.IsAbstract);
    }
}