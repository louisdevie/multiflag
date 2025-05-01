namespace Multiflag.Tests;

public class EnumBitflagSetTests
{
    [Fact]
    public void SignedEnumsAreUnsupported()
    {
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S8Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S16Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S32Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S64Enum>());
    }

    [Fact]
    public void ValueNotAPowerOfTwo()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag(0));
        Assert.Throws<InvalidBitflagValueException>(() => flags.Flag((U32Enum)11));
    }

    [Fact]
    public void ValueNotDefined()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        Assert.Throws<UndefinedEnumValueException>(() => flags.Flag((U32Enum)8));
    }

    [Fact]
    public void Add()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flagB = flags.Flag(U32Enum.B);
        var flagC = flags.Flag(U32Enum.C);
        var flags2And4 = flags.Flag(flagB, flagC);

        Assert.Equal((U32Enum)3, U32Enum.A + flagB);
        Assert.Equal((U32Enum)5, U32Enum.A + flagC);
        Assert.Equal((U32Enum)7, U32Enum.A + flags2And4);
    }

    [Fact]
    public void Add8Bit()
    {
        var flags = new EnumBitflagSet<U8Enum>();
        var flagB = flags.Flag(U8Enum.B);
        var flagC = flags.Flag(U8Enum.C);
        var flags2And4 = flags.Flag(flagB, flagC);

        Assert.Equal((U8Enum)3, U8Enum.A + flagB);
        Assert.Equal((U8Enum)5, U8Enum.A + flagC);
        Assert.Equal((U8Enum)7, U8Enum.A + flags2And4);
    }

    [Fact]
    public void Add16Bit()
    {
        var flags = new EnumBitflagSet<U16Enum>();
        var flagB = flags.Flag(U16Enum.B);
        var flagC = flags.Flag(U16Enum.C);
        var flags2And4 = flags.Flag(flagB, flagC);

        Assert.Equal((U16Enum)3, U16Enum.A + flagB);
        Assert.Equal((U16Enum)5, U16Enum.A + flagC);
        Assert.Equal((U16Enum)7, U16Enum.A + flags2And4);
    }

    [Fact]
    public void Add64Bit()
    {
        var flags = new EnumBitflagSet<U64Enum>();
        var flagB = flags.Flag(U64Enum.B);
        var flagC = flags.Flag(U64Enum.C);
        var flags2And4 = flags.Flag(flagB, flagC);

        Assert.Equal((U64Enum)3, U64Enum.A + flagB);
        Assert.Equal((U64Enum)5, U64Enum.A + flagC);
        Assert.Equal((U64Enum)7, U64Enum.A + flags2And4);
    }

    [Fact]
    public void Remove()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flagA = flags.Flag(U32Enum.A);
        var flagB = flags.Flag(U32Enum.B);
        var flagC = flags.Flag(U32Enum.C, flagA);

        Assert.Equal(U32Enum.B, (U32Enum)7 - flagA);
        Assert.Equal((U32Enum)5, (U32Enum)7 - flagB);
        Assert.Equal((U32Enum)3, (U32Enum)7 - flagC);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flagA = flags.Flag(U32Enum.A);
        var flagB = flags.Flag(U32Enum.B);
        var flagC = flags.Flag(U32Enum.C, flagA);

        Assert.True(flagA.IsIn(U32Enum.A));
        Assert.True(flagB.IsIn((U32Enum)3));
        Assert.False(flagC.IsIn(U32Enum.B));
        Assert.True(flagC.IsIn((U32Enum)5));
    }

    [Fact]
    public void IsAbstract()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flagA = flags.Flag(U32Enum.A);
        var flagB = flags.Flag(U32Enum.B);
        var flags1And2 = flags.Flag(flagA, flagB);
        var flagC = flags.Flag(U32Enum.C, flags1And2);

        Assert.False(flagA.IsAbstract);
        Assert.False(flagB.IsAbstract);
        Assert.True(flags1And2.IsAbstract);
        Assert.False(flagC.IsAbstract);
    }

    private enum S8Enum : sbyte;

    private enum U8Enum : byte
    {
        A = 1,
        B = 2,
        C = 4
    }

    private enum S16Enum : short;

    private enum U16Enum : ushort
    {
        A = 1,
        B = 2,
        C = 4
    }

    private enum S32Enum;

    private enum U32Enum : uint
    {
        A = 1,
        B = 2,
        C = 4
    }

    private enum S64Enum : long;

    private enum U64Enum : ulong
    {
        A = 1,
        B = 2,
        C = 4
    }
}