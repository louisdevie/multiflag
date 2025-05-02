namespace Multiflag.Tests;

public class FlagSetTests
{
    [Fact]
    public void UseFlagFromOtherSetAsParent()
    {
        var flags = new U32BitflagSet();
        var flag = flags.Flag(1);

        var otherFlags = new U32BitflagSet();
        Assert.Throws<ForeignFlagException>(() => otherFlags.Flag(2, flag));
    }

    [Fact]
    public void UseSameValueTwice()
    {
        var flags = new U32BitflagSet();
        var flag = flags.Flag(1);
        Assert.Throws<ReusedFlagValueException>(() => flags.Flag(1, flag));
    }
}