using Multiflag.BuiltInValueTypes;

namespace Tests
{
    public class Flag32Tests
    {
        [Fact]
        public void Addition()
        {
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2);
            Flag32 flag4 = new Flag32(4, flag2);

            uint flags = 0;
            Assert.Equal(0u, flags + flag1);
            Assert.Equal(1u, flags + flag2);
            Assert.Equal(2u, flags + flag3);
            Assert.Equal(5u, flags + flag4);

            flags = 2;
            Assert.Equal(2u, flags + flag1);
            Assert.Equal(3u, flags + flag2);
            Assert.Equal(2u, flags + flag3);
            Assert.Equal(7u, flags + flag4);

            flags = 255;
            Assert.Equal(255u, flags + flag1);
            Assert.Equal(255u, flags + flag2);
            Assert.Equal(255u, flags + flag3);
            Assert.Equal(255u, flags + flag4);
        }

        [Fact]
        public void Substraction()
        {
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2);
            Flag32 flag4 = new Flag32(4, flag2);

            uint flags = 0;
            Assert.Equal(0u, flags - flag1);
            Assert.Equal(0u, flags - flag2);
            Assert.Equal(0u, flags - flag3);
            Assert.Equal(0u, flags - flag4);

            flags = 3;
            Assert.Equal(3u, flags - flag1);
            Assert.Equal(2u, flags - flag2);
            Assert.Equal(1u, flags - flag3);
            Assert.Equal(3u, flags - flag4);

            flags = 255;
            Assert.Equal(255u, flags - flag1);
            Assert.Equal(250u, flags - flag2);
            Assert.Equal(253u, flags - flag3);
            Assert.Equal(251u, flags - flag4);
        }

        [Fact]
        public void Includes()
        {
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2);
            Flag32 flag4 = new Flag32(4, flag2);

            uint flags = 0;
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
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2, flag1);
            Flag32 flag4 = new Flag32(flag1);
            Flag32 flag5 = new Flag32(4, flag2);
            Flag32 flag6 = new Flag32(flag2);
            Flag32 flag7 = new Flag32(flag4, flag6);

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
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2, flag2);
            Flag32 flag4 = new Flag32(flag2);

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
            Flag32 flag1 = new Flag32();
            Flag32 flag2 = new Flag32(1);
            Flag32 flag3 = new Flag32(2, flag2);
            Flag32 flag4 = new Flag32(flag2);

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