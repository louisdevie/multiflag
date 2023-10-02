using Multiflag.BuiltInValueTypes;

namespace Tests
{
    public class Flag16Tests
    {
        [Fact]
        public void Addition()
        {
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2);
            Flag16 flag4 = new Flag16(4, flag2);

            ushort flags = 0;
            Assert.Equal(0, flags + flag1);
            Assert.Equal(1, flags + flag2);
            Assert.Equal(2, flags + flag3);
            Assert.Equal(5, flags + flag4);

            flags = 2;
            Assert.Equal(2, flags + flag1);
            Assert.Equal(3, flags + flag2);
            Assert.Equal(2, flags + flag3);
            Assert.Equal(7, flags + flag4);

            flags = 255;
            Assert.Equal(255, flags + flag1);
            Assert.Equal(255, flags + flag2);
            Assert.Equal(255, flags + flag3);
            Assert.Equal(255, flags + flag4);
        }

        [Fact]
        public void Substraction()
        {
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2);
            Flag16 flag4 = new Flag16(4, flag2);

            ushort flags = 0;
            Assert.Equal(0, flags - flag1);
            Assert.Equal(0, flags - flag2);
            Assert.Equal(0, flags - flag3);
            Assert.Equal(0, flags - flag4);

            flags = 3;
            Assert.Equal(3, flags - flag1);
            Assert.Equal(2, flags - flag2);
            Assert.Equal(1, flags - flag3);
            Assert.Equal(3, flags - flag4);

            flags = 255;
            Assert.Equal(255, flags - flag1);
            Assert.Equal(250, flags - flag2);
            Assert.Equal(253, flags - flag3);
            Assert.Equal(251, flags - flag4);
        }

        [Fact]
        public void Includes()
        {
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2);
            Flag16 flag4 = new Flag16(4, flag2);

            ushort flags = 0;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.False(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = 6;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = 7;
            Assert.True(flags.Includes(flag1));
            Assert.True(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.True(flags.Includes(flag4));
        }

        [Fact]
        public void Properties()
        {
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2, flag1);
            Flag16 flag4 = new Flag16(flag1);
            Flag16 flag5 = new Flag16(4, flag2);
            Flag16 flag6 = new Flag16(flag2);
            Flag16 flag7 = new Flag16(flag4, flag6);

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
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2, flag2);
            Flag16 flag4 = new Flag16(flag2);

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
            Flag16 flag1 = new Flag16();
            Flag16 flag2 = new Flag16(1);
            Flag16 flag3 = new Flag16(2, flag2);
            Flag16 flag4 = new Flag16(flag2);

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