using Multiflag.BuiltInValueTypes;

namespace Tests
{
    public class FlagSetTests
    {
        private HashSet<string> EmptySet => new HashSet<string> { };
        private HashSet<string> ASet => new HashSet<string> { "A" };
        private HashSet<string> BSet => new HashSet<string> { "B" };
        private HashSet<string> CSet => new HashSet<string> { "C" };
        private HashSet<string> ABSet => new HashSet<string> { "A", "B" };
        private HashSet<string> BCSet => new HashSet<string> { "B", "C" };
        private HashSet<string> ACSet => new HashSet<string> { "A", "C" };
        private HashSet<string> ABCSet => new HashSet<string> { "A", "B", "C" };

        [Fact]
        public void Addition()
        {
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new("A");
            FlagSet<string> flag3 = new("B");
            FlagSet<string> flag4 = new("C", flag2);

            HashSet<string> flags = EmptySet;
            Assert.Equal(EmptySet, flags + flag1);
            flags = EmptySet;
            Assert.Equal(ASet, flags + flag2);
            flags = EmptySet;
            Assert.Equal(BSet, flags + flag3);
            flags = EmptySet;
            Assert.Equal(ACSet, flags + flag4);

            flags = new HashSet<string> { "B" };
            Assert.Equal(BSet, flags + flag1);
            flags = new HashSet<string> { "B" };
            Assert.Equal(ABSet, flags + flag2);
            flags = new HashSet<string> { "B" };
            Assert.Equal(BSet, flags + flag3);
            flags = new HashSet<string> { "B" };
            Assert.Equal(ABCSet, flags + flag4);

            flags = new HashSet<string> { "A", "B", "C" };
            Assert.Equal(ABCSet, flags + flag1);
            flags = new HashSet<string> { "A", "B", "C" };
            Assert.Equal(ABCSet, flags + flag2);
            flags = new HashSet<string> { "A", "B", "C" };
            Assert.Equal(ABCSet, flags + flag3);
            flags = new HashSet<string> { "A", "B", "C" };
            Assert.Equal(ABCSet, flags + flag4);
        }

        [Fact]
        public void Substraction()
        {
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new("A");
            FlagSet<string> flag3 = new("B");
            FlagSet<string> flag4 = new("C", flag2);

            HashSet<string> flags = EmptySet;
            Assert.Equal(EmptySet, flags - flag1);
            flags = EmptySet;
            Assert.Equal(EmptySet, flags - flag2);
            flags = EmptySet;
            Assert.Equal(EmptySet, flags - flag3);
            flags = EmptySet;
            Assert.Equal(EmptySet, flags - flag4);

            flags = ABSet;
            Assert.Equal(ABSet, flags - flag1);
            flags = ABSet;
            Assert.Equal(BSet, flags - flag2);
            flags = ABSet;
            Assert.Equal(ASet, flags - flag3);
            flags = ABSet;
            Assert.Equal(ABSet, flags - flag4);

            flags = ABCSet;
            Assert.Equal(ABCSet, flags - flag1);
            flags = ABCSet;
            Assert.Equal(BSet, flags - flag2);
            flags = ABCSet;
            Assert.Equal(ACSet, flags - flag3);
            flags = ABCSet;
            Assert.Equal(ABSet, flags - flag4);
        }

        [Fact]
        public void Includes()
        {
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new(ASet);
            FlagSet<string> flag3 = new(BSet);
            FlagSet<string> flag4 = new("C", flag2);

            HashSet<string> flags = EmptySet;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.False(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = BCSet;
            Assert.True(flags.Includes(flag1));
            Assert.False(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.False(flags.Includes(flag4));

            flags = ABCSet;
            Assert.True(flags.Includes(flag1));
            Assert.True(flags.Includes(flag2));
            Assert.True(flags.Includes(flag3));
            Assert.True(flags.Includes(flag4));
        }

        [Fact]
        public void Properties()
        {
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new("A");
            FlagSet<string> flag3 = new("B", flag1);
            FlagSet<string> flag4 = new(flag1);
            FlagSet<string> flag5 = new("C", flag2);
            FlagSet<string> flag6 = new(flag2);
            FlagSet<string> flag7 = new(flag4, flag6);

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
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new("A");
            FlagSet<string> flag3 = new("B", flag2);
            FlagSet<string> flag4 = new(flag2);

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
            FlagSet<string> flag1 = new();
            FlagSet<string> flag2 = new("A");
            FlagSet<string> flag3 = new("B", flag2);
            FlagSet<string> flag4 = new(flag2);

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