using System.Numerics;

namespace Multiflag.Tests;

public class DynamicBitflagSetTests
{
    private static readonly BigInteger BigPowerOfTwo = new BigInteger(1) << 100;

    [Fact]
    public void ValueNotAPowerOfTwo()
    {
        var flags = new DynamicBitflagSet();
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(0));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(11));
    }

    [Fact]
    public void Add()
    {
        var flags = new DynamicBitflagSet();
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4);
        var flags2And4 = flags.Flag(flag2, flag4);
        var flag100 = flags.Flag(BigPowerOfTwo);

        Assert.Equal(3u, 1 + flag2);
        Assert.Equal(5u, 1 + flag4);
        Assert.Equal(7u, 1 + flags2And4);
        Assert.Equal(BigPowerOfTwo + 1, 1 + flag100);
        Assert.Equal(BigPowerOfTwo + 2, BigPowerOfTwo + flag2);
    }

    [Fact]
    public void Remove()
    {
        var flags = new DynamicBitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4, flag1);
        var flag100 = flags.Flag(BigPowerOfTwo);

        Assert.Equal(2u, 7 - flag1);
        Assert.Equal(5u, 7 - flag2);
        Assert.Equal(3u, 7 - flag4);
        Assert.Equal(2, BigPowerOfTwo + 2 - flag100);
        Assert.Equal(2, 2 - flag100);
        Assert.Equal(BigPowerOfTwo - 6, BigPowerOfTwo - 1 - flag1);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new DynamicBitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4, flag1);

        Assert.True(flag1.IsIn(1));
        Assert.True(flag2.IsIn(3));
        Assert.False(flag4.IsIn(4));
        Assert.True(flag4.IsIn(5));
        Assert.False(flag4.IsIn(BigPowerOfTwo + 1));
    }

    [Fact]
    public void IsAbstract()
    {
        var flags = new DynamicBitflagSet();
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