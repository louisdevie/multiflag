namespace Multiflag.Tests;

public class U16BitflagSetTests
{
    [Fact]
    public void ValueNotAPowerOfTwo()
    {
        var flags = new U16BitflagSet();
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(0));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(11));
    }

    [Fact]
    public void Union()
    {
        var flags = new U16BitflagSet();

        Assert.Equal(0, flags.Union(0, 0));
        Assert.Equal(1, flags.Union(1, 0));
        Assert.Equal(2, flags.Union(0, 2));
        Assert.Equal(3, flags.Union(1, 2));
        Assert.Equal(7, flags.Union(3, 6));
    }

    [Fact] 
    public void Difference() {
        var flags = new U16BitflagSet();

        Assert.Equal(0, flags.Difference(0, 0));
        Assert.Equal(1, flags.Difference(1, 0));
        Assert.Equal(1, flags.Difference(3, 6));
        Assert.Equal(4, flags.Difference(6, 3));
        Assert.Equal(8, flags.Difference(8, 17));
    }

    [Fact]
    public void Intersection()
    {
        var flags = new U16BitflagSet();

        Assert.Equal(0, flags.Intersection(0, 0));
        Assert.Equal(0, flags.Intersection(1, 0));
        Assert.Equal(0, flags.Intersection(1, 2));
        Assert.Equal(1, flags.Intersection(1, 3));
        Assert.Equal(1, flags.Intersection(11, 5));
        Assert.Equal(3, flags.Intersection(11, 7));
    }

    [Fact]
    public void Iterate()
    {
        var flags = new U16BitflagSet();

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
        var flags = new U16BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag4 = flags.Flag(4, flag1);
        var flag8 = flags.Flag(8, flag4);

        Assert.Equal(0, flags.Minimum(0));
        Assert.Equal(1, flags.Minimum(1));
        Assert.Equal(0, flags.Minimum(2));
        Assert.Equal(3, flags.Minimum(3));
        Assert.Equal(3, flags.Minimum(11));
        Assert.Equal(13, flags.Minimum(13));
        Assert.Equal(1, flags.Minimum(17));
    }

    [Fact]
    public void Maximum()
    {
        var flags = new U16BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag4 = flags.Flag(4, flag1);
        var flag8 = flags.Flag(8, flag4);

        Assert.Equal(0, flags.Maximum(0));
        Assert.Equal(1, flags.Maximum(1));
        Assert.Equal(3, flags.Maximum(2));
        Assert.Equal(3, flags.Maximum(3));
        Assert.Equal(15, flags.Maximum(11));
        Assert.Equal(13, flags.Maximum(13));
        Assert.Equal(1, flags.Maximum(17));
    }

    [Fact]
    public void Add()
    {
        var flags = new U16BitflagSet();
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
        var flags = new U16BitflagSet();
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
        var flags = new U16BitflagSet();
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
        var flags = new U16BitflagSet();
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