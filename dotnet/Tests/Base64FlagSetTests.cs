namespace Multiflag.Tests;

public class Base64FlagSetTests
{
    [Fact]
    public void CreateFromIndex()
    {
        var flags = new Base64FlagSet();

        var flag2 = flags.Flag(2);
        Assert.Equal("C", "" + flag2);

        Assert.Throws<ArgumentOutOfRangeException>(() => flags.Flag(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => flags.Flag(-2));
    }

    [Fact]
    public void CreateFromString()
    {
        var flags = new Base64FlagSet();

        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(""));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag("A"));
        flags.Flag("B");
        flags.Flag("C");
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag("D"));
        flags.Flag("E");
        flags.Flag("EAA");
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag("AAD"));
    }

    [Fact]
    public void Add()
    {
        var flags = new Base64FlagSet();
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
        var flags = new Base64FlagSet();
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
        var flags = new Base64FlagSet();
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
        var flags = new Base64FlagSet();
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