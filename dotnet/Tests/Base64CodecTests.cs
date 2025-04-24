using Multiflag.Base64Format;

namespace Multiflag.Tests;

public class Base64CodecTests
{
    [Fact]
    public void EncodeByte()
    {
        Assert.Equal('A', Base64Codec.EncodeByte(0));
        Assert.Equal('B', Base64Codec.EncodeByte(1));
        Assert.Equal('C', Base64Codec.EncodeByte(2));
        Assert.Equal('D', Base64Codec.EncodeByte(3));
        Assert.Equal('E', Base64Codec.EncodeByte(4));
        Assert.Equal('F', Base64Codec.EncodeByte(5));
        Assert.Equal('G', Base64Codec.EncodeByte(6));
        Assert.Equal('H', Base64Codec.EncodeByte(7));
        Assert.Equal('I', Base64Codec.EncodeByte(8));
        Assert.Equal('J', Base64Codec.EncodeByte(9));
        Assert.Equal('K', Base64Codec.EncodeByte(10));
        Assert.Equal('L', Base64Codec.EncodeByte(11));
        Assert.Equal('M', Base64Codec.EncodeByte(12));
        Assert.Equal('N', Base64Codec.EncodeByte(13));
        Assert.Equal('O', Base64Codec.EncodeByte(14));
        Assert.Equal('P', Base64Codec.EncodeByte(15));
        Assert.Equal('Q', Base64Codec.EncodeByte(16));
        Assert.Equal('R', Base64Codec.EncodeByte(17));
        Assert.Equal('S', Base64Codec.EncodeByte(18));
        Assert.Equal('T', Base64Codec.EncodeByte(19));
        Assert.Equal('U', Base64Codec.EncodeByte(20));
        Assert.Equal('V', Base64Codec.EncodeByte(21));
        Assert.Equal('W', Base64Codec.EncodeByte(22));
        Assert.Equal('X', Base64Codec.EncodeByte(23));
        Assert.Equal('Y', Base64Codec.EncodeByte(24));
        Assert.Equal('Z', Base64Codec.EncodeByte(25));
        Assert.Equal('a', Base64Codec.EncodeByte(26));
        Assert.Equal('b', Base64Codec.EncodeByte(27));
        Assert.Equal('c', Base64Codec.EncodeByte(28));
        Assert.Equal('d', Base64Codec.EncodeByte(29));
        Assert.Equal('e', Base64Codec.EncodeByte(30));
        Assert.Equal('f', Base64Codec.EncodeByte(31));
        Assert.Equal('g', Base64Codec.EncodeByte(32));
        Assert.Equal('h', Base64Codec.EncodeByte(33));
        Assert.Equal('i', Base64Codec.EncodeByte(34));
        Assert.Equal('j', Base64Codec.EncodeByte(35));
        Assert.Equal('k', Base64Codec.EncodeByte(36));
        Assert.Equal('l', Base64Codec.EncodeByte(37));
        Assert.Equal('m', Base64Codec.EncodeByte(38));
        Assert.Equal('n', Base64Codec.EncodeByte(39));
        Assert.Equal('o', Base64Codec.EncodeByte(40));
        Assert.Equal('p', Base64Codec.EncodeByte(41));
        Assert.Equal('q', Base64Codec.EncodeByte(42));
        Assert.Equal('r', Base64Codec.EncodeByte(43));
        Assert.Equal('s', Base64Codec.EncodeByte(44));
        Assert.Equal('t', Base64Codec.EncodeByte(45));
        Assert.Equal('u', Base64Codec.EncodeByte(46));
        Assert.Equal('v', Base64Codec.EncodeByte(47));
        Assert.Equal('w', Base64Codec.EncodeByte(48));
        Assert.Equal('x', Base64Codec.EncodeByte(49));
        Assert.Equal('y', Base64Codec.EncodeByte(50));
        Assert.Equal('z', Base64Codec.EncodeByte(51));
        Assert.Equal('0', Base64Codec.EncodeByte(52));
        Assert.Equal('1', Base64Codec.EncodeByte(53));
        Assert.Equal('2', Base64Codec.EncodeByte(54));
        Assert.Equal('3', Base64Codec.EncodeByte(55));
        Assert.Equal('4', Base64Codec.EncodeByte(56));
        Assert.Equal('5', Base64Codec.EncodeByte(57));
        Assert.Equal('6', Base64Codec.EncodeByte(58));
        Assert.Equal('7', Base64Codec.EncodeByte(59));
        Assert.Equal('8', Base64Codec.EncodeByte(60));
        Assert.Equal('9', Base64Codec.EncodeByte(61));
        Assert.Equal('-', Base64Codec.EncodeByte(62));
        Assert.Equal('_', Base64Codec.EncodeByte(63));
    }
    
    [Fact]
    public void DecodeByte()
    {
        Assert.Equal(0, Base64Codec.DecodeByte('A'));
        Assert.Equal(1, Base64Codec.DecodeByte('B'));
        Assert.Equal(2, Base64Codec.DecodeByte('C'));
        Assert.Equal(3, Base64Codec.DecodeByte('D'));
        Assert.Equal(4, Base64Codec.DecodeByte('E'));
        Assert.Equal(5, Base64Codec.DecodeByte('F'));
        Assert.Equal(6, Base64Codec.DecodeByte('G'));
        Assert.Equal(7, Base64Codec.DecodeByte('H'));
        Assert.Equal(8, Base64Codec.DecodeByte('I'));
        Assert.Equal(9, Base64Codec.DecodeByte('J'));
        Assert.Equal(10, Base64Codec.DecodeByte('K'));
        Assert.Equal(11, Base64Codec.DecodeByte('L'));
        Assert.Equal(12, Base64Codec.DecodeByte('M'));
        Assert.Equal(13, Base64Codec.DecodeByte('N'));
        Assert.Equal(14, Base64Codec.DecodeByte('O'));
        Assert.Equal(15, Base64Codec.DecodeByte('P'));
        Assert.Equal(16, Base64Codec.DecodeByte('Q'));
        Assert.Equal(17, Base64Codec.DecodeByte('R'));
        Assert.Equal(18, Base64Codec.DecodeByte('S'));
        Assert.Equal(19, Base64Codec.DecodeByte('T'));
        Assert.Equal(20, Base64Codec.DecodeByte('U'));
        Assert.Equal(21, Base64Codec.DecodeByte('V'));
        Assert.Equal(22, Base64Codec.DecodeByte('W'));
        Assert.Equal(23, Base64Codec.DecodeByte('X'));
        Assert.Equal(24, Base64Codec.DecodeByte('Y'));
        Assert.Equal(25, Base64Codec.DecodeByte('Z'));
        Assert.Equal(26, Base64Codec.DecodeByte('a'));
        Assert.Equal(27, Base64Codec.DecodeByte('b'));
        Assert.Equal(28, Base64Codec.DecodeByte('c'));
        Assert.Equal(29, Base64Codec.DecodeByte('d'));
        Assert.Equal(30, Base64Codec.DecodeByte('e'));
        Assert.Equal(31, Base64Codec.DecodeByte('f'));
        Assert.Equal(32, Base64Codec.DecodeByte('g'));
        Assert.Equal(33, Base64Codec.DecodeByte('h'));
        Assert.Equal(34, Base64Codec.DecodeByte('i'));
        Assert.Equal(35, Base64Codec.DecodeByte('j'));
        Assert.Equal(36, Base64Codec.DecodeByte('k'));
        Assert.Equal(37, Base64Codec.DecodeByte('l'));
        Assert.Equal(38, Base64Codec.DecodeByte('m'));
        Assert.Equal(39, Base64Codec.DecodeByte('n'));
        Assert.Equal(40, Base64Codec.DecodeByte('o'));
        Assert.Equal(41, Base64Codec.DecodeByte('p'));
        Assert.Equal(42, Base64Codec.DecodeByte('q'));
        Assert.Equal(43, Base64Codec.DecodeByte('r'));
        Assert.Equal(44, Base64Codec.DecodeByte('s'));
        Assert.Equal(45, Base64Codec.DecodeByte('t'));
        Assert.Equal(46, Base64Codec.DecodeByte('u'));
        Assert.Equal(47, Base64Codec.DecodeByte('v'));
        Assert.Equal(48, Base64Codec.DecodeByte('w'));
        Assert.Equal(49, Base64Codec.DecodeByte('x'));
        Assert.Equal(50, Base64Codec.DecodeByte('y'));
        Assert.Equal(51, Base64Codec.DecodeByte('z'));
        Assert.Equal(52, Base64Codec.DecodeByte('0'));
        Assert.Equal(53, Base64Codec.DecodeByte('1'));
        Assert.Equal(54, Base64Codec.DecodeByte('2'));
        Assert.Equal(55, Base64Codec.DecodeByte('3'));
        Assert.Equal(56, Base64Codec.DecodeByte('4'));
        Assert.Equal(57, Base64Codec.DecodeByte('5'));
        Assert.Equal(58, Base64Codec.DecodeByte('6'));
        Assert.Equal(59, Base64Codec.DecodeByte('7'));
        Assert.Equal(60, Base64Codec.DecodeByte('8'));
        Assert.Equal(61, Base64Codec.DecodeByte('9'));
        Assert.Equal(62, Base64Codec.DecodeByte('-'));
        Assert.Equal(63, Base64Codec.DecodeByte('_'));
    }

    [Fact]
    public void EncodeSingleFlag()
    {
        Assert.Equal("B", Base64Codec.EncodeSingleFlag(1));
        Assert.Equal("C", Base64Codec.EncodeSingleFlag(2));
        Assert.Equal("E", Base64Codec.EncodeSingleFlag(3));
        Assert.Equal("I", Base64Codec.EncodeSingleFlag(4));
        Assert.Equal("Q", Base64Codec.EncodeSingleFlag(5));
        Assert.Equal("g", Base64Codec.EncodeSingleFlag(6));
        Assert.Equal("AB", Base64Codec.EncodeSingleFlag(7));
        Assert.Equal("AI", Base64Codec.EncodeSingleFlag(10));
        Assert.Equal("AAAAAAAAAAAAAAAAI", Base64Codec.EncodeSingleFlag(100));
    }

    [Fact]
    public void DecodesToZero()
    {
        Assert.True(Base64Codec.DecodesToZero(""));
        Assert.True(Base64Codec.DecodesToZero("A"));
        Assert.False(Base64Codec.DecodesToZero("X"));
        Assert.False(Base64Codec.DecodesToZero("AX"));
        Assert.True(Base64Codec.DecodesToZero("AA"));
        Assert.False(Base64Codec.DecodesToZero("XA"));
    }

    [Fact]
    public void DecodesToSingleFlag()
    {
        Assert.False(Base64Codec.DecodesToSingleFlag(""));
        Assert.False(Base64Codec.DecodesToSingleFlag("A"));
        Assert.True(Base64Codec.DecodesToSingleFlag("B"));
        Assert.True(Base64Codec.DecodesToSingleFlag("C"));
        Assert.False(Base64Codec.DecodesToSingleFlag("D"));
        Assert.True(Base64Codec.DecodesToSingleFlag("E"));
        Assert.True(Base64Codec.DecodesToSingleFlag("I"));
        Assert.True(Base64Codec.DecodesToSingleFlag("IAA"));
        Assert.True(Base64Codec.DecodesToSingleFlag("AAI"));
        Assert.False(Base64Codec.DecodesToSingleFlag("IAI"));
    }

    [Fact]
    public void BitwiseOr()
    {
        Assert.Equal("A", Base64Codec.BitwiseOr("", ""));
        Assert.Equal("A", Base64Codec.BitwiseOr("A", ""));
        Assert.Equal("B", Base64Codec.BitwiseOr("B", ""));
        Assert.Equal("X", Base64Codec.BitwiseOr("X", ""));
        Assert.Equal("X", Base64Codec.BitwiseOr("", "X"));
        Assert.Equal("V", Base64Codec.BitwiseOr("R", "F"));
        Assert.Equal("VD0", Base64Codec.BitwiseOr("RD0", "F"));
        Assert.Equal("VD0", Base64Codec.BitwiseOr("RD0", "FAA"));
    }

    [Fact]
    public void BitwiseAndNot()
    {
        Assert.Equal("A", Base64Codec.BitwiseAndNot("", ""));
        Assert.Equal("A", Base64Codec.BitwiseAndNot("A", ""));
        Assert.Equal("B", Base64Codec.BitwiseAndNot("B", ""));
        Assert.Equal("X", Base64Codec.BitwiseAndNot("X", ""));
        Assert.Equal("A", Base64Codec.BitwiseAndNot("", "X"));
        Assert.Equal("Q", Base64Codec.BitwiseAndNot("R", "F"));
        Assert.Equal("QD0", Base64Codec.BitwiseAndNot("RD0", "F"));
        Assert.Equal("QD0", Base64Codec.BitwiseAndNot("RD0", "FAA"));
        Assert.Equal("E", Base64Codec.BitwiseAndNot("F", "RD0"));
    }

    [Fact]
    public void BitwiseAndEquals()
    {
        Assert.True(Base64Codec.BitwiseAndEquals("", ""));
        Assert.True(Base64Codec.BitwiseAndEquals("A", ""));
        Assert.True(Base64Codec.BitwiseAndEquals("", "A"));
        Assert.True(Base64Codec.BitwiseAndEquals("X", ""));
        Assert.False(Base64Codec.BitwiseAndEquals("", "X"));
        Assert.False(Base64Codec.BitwiseAndEquals("R", "F"));
        Assert.True(Base64Codec.BitwiseAndEquals("RD0", "AA0"));
        Assert.True(Base64Codec.BitwiseAndEquals("RD0", "RAA"));
    }
}