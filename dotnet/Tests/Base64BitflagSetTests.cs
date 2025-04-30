namespace Multiflag.Tests;

public class Base64BitflagSetTests
{
    [Fact]
    public void Create()
    {
        var flags = new Base64BitflagSet();

        var flag2 = flags.Flag(2);
        Assert.Equal("C", "" + flag2);

        Assert.Throws<ArgumentOutOfRangeException>(() => flags.Flag(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => flags.Flag(-2));
    }
    
    [Fact]
    public void Union() {
        var flags = new Base64BitflagSet();

        Assert.Equal("A", flags.Union("", ""));
        Assert.Equal("A", flags.Union("A", "A"));
        Assert.Equal("B", flags.Union("B", "A"));
        Assert.Equal("C", flags.Union("A", "C"));
        Assert.Equal("D", flags.Union("B", "C"));
        Assert.Equal("H", flags.Union("D", "G"));
    }

    [Fact]
    public void Difference()
    {
        var flags = new Base64BitflagSet();

        Assert.Equal("A", flags.Difference("", ""));
        Assert.Equal("A", flags.Difference("A", "A"));
        Assert.Equal("B", flags.Difference("B", "A"));
        Assert.Equal("B", flags.Difference("D", "G"));
        Assert.Equal("E", flags.Difference("G", "D"));
        Assert.Equal("IB", flags.Difference("IB", "R"));
    }

    [Fact]
    public void Intersection()
    {
        var flags = new Base64BitflagSet();

        Assert.Equal("A", flags.Intersection("", ""));
        Assert.Equal("A", flags.Intersection("A", "A"));
        Assert.Equal("A", flags.Intersection("B", "A"));
        Assert.Equal("A", flags.Intersection("B", "C"));
        Assert.Equal("B", flags.Intersection("B", "D"));
        Assert.Equal("B", flags.Intersection("L", "F"));
        Assert.Equal("D", flags.Intersection("L", "H"));
    }

    [Fact]
    public void Iterate()
    {
        var flags = new Base64BitflagSet();

        Assert.Equal([], flags.Iterate("A"));
        Assert.Equal([1], flags.Iterate("B"));
        Assert.Equal([2], flags.Iterate("C"));
        Assert.Equal([1, 2], flags.Iterate("D"));
        Assert.Equal([1, 2, 4], flags.Iterate("L"));
        Assert.Equal([3, 6, 7], flags.Iterate("kB"));
    }

    [Fact]
    public void Minimum()
    {
        var flags = new Base64BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag3 = flags.Flag(3, flag1);
        var flag4 = flags.Flag(4, flag3);

        Assert.Equal("A", flags.Minimum("A"));
        Assert.Equal("B", flags.Minimum("B"));
        Assert.Equal("A", flags.Minimum("C"));
        Assert.Equal("D", flags.Minimum("D"));
        Assert.Equal("D", flags.Minimum("L"));
        Assert.Equal("N", flags.Minimum("N"));
        Assert.Equal("B", flags.Minimum("R"));
    }

    [Fact]
    public void Maximum()
    {
        var flags = new Base64BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2, flag1);
        var flag3 = flags.Flag(3, flag1);
        var flag4 = flags.Flag(4, flag3);

        Assert.Equal("A", flags.Maximum("A"));
        Assert.Equal("B", flags.Maximum("B"));
        Assert.Equal("D", flags.Maximum("C"));
        Assert.Equal("D", flags.Maximum("D"));
        Assert.Equal("P", flags.Maximum("L"));
        Assert.Equal("N", flags.Maximum("N"));
        Assert.Equal("B", flags.Maximum("R"));
    }

    [Fact]
    public void Add()
    {
        var flags = new Base64BitflagSet();
        var flag2 = flags.Flag(2);
        var flag3 = flags.Flag(3);
        var flags2And3 = flags.Flag(flag2, flag3);

        Assert.Equal("D", "B" + flag2);
        Assert.Equal("F", "B" + flag3);
        Assert.Equal("H", "B" + flags2And3);
    }

    [Fact]
    public void Remove()
    {
        var flags = new Base64BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag3 = flags.Flag(3, flag1);

        Assert.Equal("C", "H" - flag1);
        Assert.Equal("F", "H" - flag2);
        Assert.Equal("D", "H" - flag3);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new Base64BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flag3 = flags.Flag(3, flag1);

        Assert.True(flag1.IsIn("B"));
        Assert.True(flag2.IsIn("D"));
        Assert.False(flag3.IsIn("E"));
        Assert.True(flag3.IsIn("F"));
    }

    [Fact]
    public void IsAbstract()
    {
        var flags = new Base64BitflagSet();
        var flag1 = flags.Flag(1);
        var flag2 = flags.Flag(2);
        var flags1And2 = flags.Flag(flag1, flag2);
        var flag3 = flags.Flag(3, flags1And2);

        Assert.False(flag1.IsAbstract);
        Assert.False(flag2.IsAbstract);
        Assert.True(flags1And2.IsAbstract);
        Assert.False(flag3.IsAbstract);
    }
}