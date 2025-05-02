namespace Multiflag.Tests;

public class EnumBitflagSetTests
{
    [Fact]
    public void SignedEnumsAreUnsupported()
    {
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S8Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<U8Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<S16Enum>());
        Assert.Throws<UnsupportedEnumTypeException>(() => new EnumBitflagSet<U16Enum>());
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
        Assert.Throws<UndefinedEnumValueException>(() => flags.Flag((U32Enum)256));
    }

    [Fact]
    public void Union()
    {
        var flags = new EnumBitflagSet<U32Enum>();

        Assert.Equal((U32Enum)0, flags.Union(0, 0));
        Assert.Equal(U32Enum.A, flags.Union(U32Enum.A, 0));
        Assert.Equal(U32Enum.B, flags.Union(0, U32Enum.B));
        Assert.Equal((U32Enum)3, flags.Union(U32Enum.A, U32Enum.B));
        Assert.Equal((U32Enum)7, flags.Union((U32Enum)3, (U32Enum)6));
    }

    [Fact] 
    public void Difference() {
        var flags = new EnumBitflagSet<U32Enum>();

        Assert.Equal((U32Enum)0, flags.Difference(0, 0));
        Assert.Equal(U32Enum.A, flags.Difference(U32Enum.A, 0));
        Assert.Equal(U32Enum.A, flags.Difference((U32Enum)3, (U32Enum)6));
        Assert.Equal(U32Enum.C, flags.Difference((U32Enum)6, (U32Enum)3));
        Assert.Equal(U32Enum.D, flags.Difference(U32Enum.D, (U32Enum)17));
    }

    [Fact]
    public void Intersection()
    {
        var flags = new EnumBitflagSet<U32Enum>();

        Assert.Equal((U32Enum)0, flags.Intersection(0, 0));
        Assert.Equal((U32Enum)0, flags.Intersection(U32Enum.A, 0));
        Assert.Equal((U32Enum)0, flags.Intersection(U32Enum.A, U32Enum.B));
        Assert.Equal(U32Enum.A, flags.Intersection(U32Enum.A, (U32Enum)3));
        Assert.Equal(U32Enum.A, flags.Intersection((U32Enum)11, (U32Enum)5));
        Assert.Equal((U32Enum)3, flags.Intersection((U32Enum)11, (U32Enum)7));
    }

    [Fact]
    public void Iterate()
    {
        var flags = new EnumBitflagSet<U32Enum>();

        Assert.Equal([], flags.Iterate(0));
        Assert.Equal([U32Enum.A], flags.Iterate(U32Enum.A));
        Assert.Equal([U32Enum.B], flags.Iterate(U32Enum.B));
        Assert.Equal([U32Enum.A, U32Enum.B], flags.Iterate((U32Enum)3));
        Assert.Equal([U32Enum.A, U32Enum.B, U32Enum.D], flags.Iterate((U32Enum)11));
        Assert.Equal([U32Enum.C, U32Enum.F, U32Enum.G], flags.Iterate((U32Enum)100));
    }

    [Fact]
    public void Minimum()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flag1 = flags.Flag(U32Enum.A);
        var flag2 = flags.Flag(U32Enum.B, flag1);
        var flag4 = flags.Flag(U32Enum.C, flag1);
        var flag8 = flags.Flag(U32Enum.D, flag4);

        Assert.Equal((U32Enum)0, flags.Minimum(0));
        Assert.Equal(U32Enum.A, flags.Minimum(U32Enum.A));
        Assert.Equal((U32Enum)0, flags.Minimum(U32Enum.B));
        Assert.Equal((U32Enum)3, flags.Minimum((U32Enum)3));
        Assert.Equal((U32Enum)3, flags.Minimum((U32Enum)11));
        Assert.Equal((U32Enum)13, flags.Minimum((U32Enum)13));
        Assert.Equal(U32Enum.A, flags.Minimum((U32Enum)17));
    }

    [Fact]
    public void Maximum()
    {
        var flags = new EnumBitflagSet<U32Enum>();
        var flag1 = flags.Flag(U32Enum.A);
        var flag2 = flags.Flag(U32Enum.B, flag1);
        var flag4 = flags.Flag(U32Enum.C, flag1);
        var flag8 = flags.Flag(U32Enum.D, flag4);

        Assert.Equal((U32Enum)0, flags.Maximum(0));
        Assert.Equal(U32Enum.A, flags.Maximum(U32Enum.A));
        Assert.Equal((U32Enum)3, flags.Maximum(U32Enum.B));
        Assert.Equal((U32Enum)3, flags.Maximum((U32Enum)3));
        Assert.Equal((U32Enum)15, flags.Maximum((U32Enum)11));
        Assert.Equal((U32Enum)13, flags.Maximum((U32Enum)13));
        Assert.Equal(U32Enum.A, flags.Maximum((U32Enum)17));
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

    private enum U8Enum : byte;

    private enum S16Enum : short;

    private enum U16Enum : ushort;

    private enum S32Enum;

    private enum U32Enum : uint
    {
        A = 1,
        B = 2,
        C = 4,
        D = 8,
        E = 16,
        F = 32,
        G = 64,
    }

    private enum S64Enum : long;

    private enum U64Enum : ulong
    {
        A = 1,
        B = 2,
        C = 4
    }
}