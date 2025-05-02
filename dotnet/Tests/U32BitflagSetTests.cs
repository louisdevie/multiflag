namespace Multiflag.Tests;

public class U32BitflagSetTests
{
    [Fact]
    public void ValueNotAPowerOfTwo()
    {
        var flags = new U32BitflagSet();
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(0));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(11));
    }

    [Fact]
    public void Union()
    {
        var flags = new U32BitflagSet();

        Assert.Equal(0u, flags.Union(0, 0));
        Assert.Equal(1u, flags.Union(1, 0));
        Assert.Equal(2u, flags.Union(0, 2));
        Assert.Equal(3u, flags.Union(1, 2));
        Assert.Equal(7u, flags.Union(3, 6));
    }

    [Fact] 
    public void Difference() {
        var flags = new U32BitflagSet();

        Assert.Equal(0u, flags.Difference(0, 0));
        Assert.Equal(1u, flags.Difference(1, 0));
        Assert.Equal(1u, flags.Difference(3, 6));
        Assert.Equal(4u, flags.Difference(6, 3));
        Assert.Equal(8u, flags.Difference(8, 17));
    }

    [Fact]
    public void Intersection()
    {
        var flags = new U32BitflagSet();

        Assert.Equal(0u, flags.Intersection(0, 0));
        Assert.Equal(0u, flags.Intersection(1, 0));
        Assert.Equal(0u, flags.Intersection(1, 2));
        Assert.Equal(1u, flags.Intersection(1, 3));
        Assert.Equal(1u, flags.Intersection(11, 5));
        Assert.Equal(3u, flags.Intersection(11, 7));
    }

    [Fact]
    public void Iterate()
    {
        var flags = new U32BitflagSet();

        Assert.Equal([], flags.Iterate(0));
        Assert.Equal([1], flags.Iterate(1));
        Assert.Equal([2], flags.Iterate(2));
        Assert.Equal([1, 2], flags.Iterate(3));
        Assert.Equal([1, 2, 8], flags.Iterate(11));
        Assert.Equal([4, 32, 64], flags.Iterate(100));
    }

    [Fact]
    public void Minimum()
    {
        var flags = new U32BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag4 = flags.Flag(4, flag1);
        var flag8 = flags.Flag(8, flag4);

        Assert.Equal(0u, flags.Minimum(0));
        Assert.Equal(1u, flags.Minimum(1));
        Assert.Equal(0u, flags.Minimum(2));
        Assert.Equal(3u, flags.Minimum(3));
        Assert.Equal(3u, flags.Minimum(11));
        Assert.Equal(13u, flags.Minimum(13));
        Assert.Equal(1u, flags.Minimum(17));
    }

    [Fact]
    public void Maximum()
    {
        var flags = new U32BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag4 = flags.Flag(4, flag1);
        var flag8 = flags.Flag(8, flag4);

        Assert.Equal(0u, flags.Maximum(0));
        Assert.Equal(1u, flags.Maximum(1));
        Assert.Equal(3u, flags.Maximum(2));
        Assert.Equal(3u, flags.Maximum(3));
        Assert.Equal(15u, flags.Maximum(11));
        Assert.Equal(13u, flags.Maximum(13));
        Assert.Equal(1u, flags.Maximum(17));
    }

    [Fact]
    public void Add()
    {
        var flags = new U32BitflagSet();
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4);
        var flags2And4 = flags.Flag(flag2, flag4);

        Assert.Equal(3u, 1 + flag2);
        Assert.Equal(5u, 1 + flag4);
        Assert.Equal(7u, 1 + flags2And4);
    }

    [Fact]
    public void Remove()
    {
        var flags = new U32BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag4 = flags.Flag(4, flag1);

        Assert.Equal(2u, 7 - flag1);
        Assert.Equal(5u, 7 - flag2);
        Assert.Equal(3u, 7 - flag4);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new U32BitflagSet();
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
        var flags = new U32BitflagSet();
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