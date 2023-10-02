using Multiflag.BuiltInValueTypes;

namespace Tests
{
    public class FlagEnumTests
    {
        private enum TheEnum
        {
            A = 1,
            B = 2,
            C = 4,

            None = 0,
            All = A | B | C
        }

        [Fact]
        public void Addition()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B);
            FlagEnum<TheEnum> flag4 = new(TheEnum.C, flag2);

            TheEnum flags = TheEnum.None;
            Assert.Equal(TheEnum.None, flags + flag1);
            Assert.Equal(TheEnum.A, flags + flag2);
            Assert.Equal(TheEnum.B, flags + flag3);
            Assert.Equal(TheEnum.C | TheEnum.A, flags + flag4);

            flags = TheEnum.B;
            Assert.Equal(TheEnum.B, flags + flag1);
            Assert.Equal(TheEnum.A | TheEnum.B, flags + flag2);
            Assert.Equal(TheEnum.B, flags + flag3);
            Assert.Equal(TheEnum.All, flags + flag4);

            flags = TheEnum.All;
            Assert.Equal(TheEnum.All, flags + flag1);
            Assert.Equal(TheEnum.All, flags + flag2);
            Assert.Equal(TheEnum.All, flags + flag3);
            Assert.Equal(TheEnum.All, flags + flag4);
        }

        [Fact]
        public void Substraction()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B);
            FlagEnum<TheEnum> flag4 = new(TheEnum.C, flag2);

            TheEnum flags = TheEnum.None;
            Assert.Equal(TheEnum.None, flags - flag1);
            Assert.Equal(TheEnum.None, flags - flag2);
            Assert.Equal(TheEnum.None, flags - flag3);
            Assert.Equal(TheEnum.None, flags - flag4);

            flags = TheEnum.A | TheEnum.B;
            Assert.Equal(TheEnum.A | TheEnum.B, flags - flag1);
            Assert.Equal(TheEnum.B, flags - flag2);
            Assert.Equal(TheEnum.A, flags - flag3);
            Assert.Equal(TheEnum.A | TheEnum.B, flags - flag4);

            flags = TheEnum.All;
            Assert.Equal(TheEnum.All, flags - flag1);
            Assert.Equal(TheEnum.B, flags - flag2);
            Assert.Equal(TheEnum.A | TheEnum.C, flags - flag3);
            Assert.Equal(TheEnum.A | TheEnum.B, flags - flag4);
        }

        [Fact]
        public void Includes()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B);
            FlagEnum<TheEnum> flag4 = new(TheEnum.C, flag2);

            TheEnum flags = TheEnum.None;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.False(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = TheEnum.B | TheEnum.C;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = TheEnum.All;
            Assert.True(flags.Includes(flag1));
            Assert.True(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.True(flags.Includes(flag4));
        }

        [Fact]
        public void Properties()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B, flag1);
            FlagEnum<TheEnum> flag4 = new(flag1);
            FlagEnum<TheEnum> flag5 = new(TheEnum.C, flag2);
            FlagEnum<TheEnum> flag6 = new(flag2);
            FlagEnum<TheEnum> flag7 = new(flag4, flag6);

            Assert.True(flag1.IsNothing);
            Assert.False(flag2.IsNothing);
            Assert.False(flag3.IsNothing);
            Assert.True(flag4.IsNothing);
            Assert.False(flag5.IsNothing);
            Assert.False(flag6.IsNothing);
            Assert.False(flag7.IsNothing);

            Assert.False(flag1.IsConcrete);
            Assert.True(flag2.IsConcrete);
            Assert.True(flag3.IsConcrete);
            Assert.False(flag4.IsConcrete);
            Assert.True(flag5.IsConcrete);
            Assert.False(flag6.IsConcrete);
            Assert.False(flag7.IsConcrete);

            Assert.False(flag1.IsAbstract);
            Assert.False(flag2.IsAbstract);
            Assert.False(flag3.IsAbstract);
            Assert.False(flag4.IsAbstract);
            Assert.False(flag5.IsAbstract);
            Assert.True(flag6.IsAbstract);
            Assert.True(flag7.IsAbstract);
        }

        [Fact]
        public void PositiveEquality()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B, flag2);
            FlagEnum<TheEnum> flag4 = new(flag2);

            Assert.True(flag1.PositiveEquals(flag1));
            Assert.False(flag1.PositiveEquals(flag2));
            Assert.False(flag1.PositiveEquals(flag3));
            Assert.False(flag1.PositiveEquals(flag4));

            Assert.False(flag2.PositiveEquals(flag1));
            Assert.True(flag2.PositiveEquals(flag2));
            Assert.False(flag2.PositiveEquals(flag3));
            Assert.True(flag2.PositiveEquals(flag4));

            Assert.False(flag3.PositiveEquals(flag1));
            Assert.False(flag3.PositiveEquals(flag2));
            Assert.True(flag3.PositiveEquals(flag3));
            Assert.False(flag3.PositiveEquals(flag4));

            Assert.False(flag4.PositiveEquals(flag1));
            Assert.True(flag4.PositiveEquals(flag2));
            Assert.False(flag4.PositiveEquals(flag3));
            Assert.True(flag4.PositiveEquals(flag4));
        }

        [Fact]
        public void NegativeEquality()
        {
            FlagEnum<TheEnum> flag1 = new();
            FlagEnum<TheEnum> flag2 = new(TheEnum.A);
            FlagEnum<TheEnum> flag3 = new(TheEnum.B, flag2);
            FlagEnum<TheEnum> flag4 = new(flag2);

            Assert.True(flag1.NegativeEquals(flag1));
            Assert.False(flag1.NegativeEquals(flag2));
            Assert.False(flag1.NegativeEquals(flag3));
            Assert.True(flag1.NegativeEquals(flag4));

            Assert.False(flag2.NegativeEquals(flag1));
            Assert.True(flag2.NegativeEquals(flag2));
            Assert.False(flag2.NegativeEquals(flag3));
            Assert.False(flag2.NegativeEquals(flag4));

            Assert.False(flag3.NegativeEquals(flag1));
            Assert.False(flag3.NegativeEquals(flag2));
            Assert.True(flag3.NegativeEquals(flag3));
            Assert.False(flag3.NegativeEquals(flag4));

            Assert.True(flag4.NegativeEquals(flag1));
            Assert.False(flag4.NegativeEquals(flag2));
            Assert.False(flag4.NegativeEquals(flag3));
            Assert.True(flag4.NegativeEquals(flag4));
        }
    }
}