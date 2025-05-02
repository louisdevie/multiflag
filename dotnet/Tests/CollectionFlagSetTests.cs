using System.Collections.Immutable;

namespace Multiflag.Tests;

public class CollectionFlagSetTests
{
    [Fact]
    public void Union()
    {
        var flags = new CollectionFlagSet<string>();

        Assert.Equal([], flags.Union([], []));
        Assert.Equal(["A"], flags.Union(["A"], []));
        Assert.Equal(["B"], flags.Union([], ["B"]));
        Assert.Equal(["A", "B"], flags.Union(["A"], ["B"]));
        Assert.Equal(["A", "B", "C"], flags.Union(["A", "B"], ["B", "C"]));
    }

    [Fact]
    public void Difference()
    {
        var flags = new CollectionFlagSet<string>();

        Assert.Equal([], flags.Difference([], []));
        Assert.Equal(["A"], flags.Difference(["A"], []));
        Assert.Equal(["A"], flags.Difference(["A", "B"], ["B", "C"]));
        Assert.Equal(["C"], flags.Difference(["B", "C"], ["A", "B"]));
        Assert.Equal(["D"], flags.Difference(["D"], ["A", "E"]));
    }


    [Fact]
    public void Intersection()
    {
        var flags = new CollectionFlagSet<string>();

        Assert.Equal([], flags.Intersection([], []));
        Assert.Equal([], flags.Intersection(["A"], []));
        Assert.Equal([], flags.Intersection(["A"], ["B"]));
        Assert.Equal(["A"], flags.Intersection(["A"], ["A", "B"]));
        Assert.Equal(["A"], flags.Intersection(["A", "B", "D"], ["A", "C"]));
        Assert.Equal(["A", "B"], flags.Intersection(["A", "B", "D"], ["A", "B", "C"]));
    }

    [Fact]
    public void Iterate()
    {
        var flags = new CollectionFlagSet<string>();

        Assert.Equal([], flags.Iterate([]));
        Assert.Equal(["A"], flags.Iterate(["A"]));
        Assert.Contains("A", flags.Iterate(["A", "B", "C"]));
        Assert.Contains("B", flags.Iterate(["A", "B", "C"]));
        Assert.Contains("C", flags.Iterate(["A", "B", "C"]));
    }

    [Fact]
    public void Minimum()
    {
        var flags = new CollectionFlagSet<string>();
        var flagA = flags.Flag("A");
        var flagB = flags.Flag("B", flagA);
        var flagC = flags.Flag("C", flagA);
        var flagD = flags.Flag("D", flagC);

        Assert.Equal([], flags.Minimum([]));
        Assert.Equal(["A"], flags.Minimum(["A"]));
        Assert.Equal([], flags.Minimum(["B"]));
        Assert.Equal(["A", "B"], flags.Minimum(["A", "B"]));
        Assert.Equal(["A", "B"], flags.Minimum(["A", "B", "D"]));
        Assert.Equal(["A", "C", "D"], flags.Minimum(["A", "C", "D"]));
        Assert.Equal(["A"], flags.Minimum(["A", "E"]));
    }

    [Fact]
    public void Maximum()
    {
        var flags = new CollectionFlagSet<string>();
        var flagA = flags.Flag("A");
        var flagB = flags.Flag("B", flagA);
        var flagC = flags.Flag("C", flagA);
        var flagD = flags.Flag("D", flagC);

        Assert.Equal([], flags.Maximum([]));
        Assert.Equal(["A"], flags.Maximum(["A"]));
        Assert.Equal(["B", "A"], flags.Maximum(["B"]));
        Assert.Equal(["A", "B"], flags.Maximum(["A", "B"]));
        Assert.Equal(["A", "B", "D", "C"], flags.Maximum(["A", "B", "D"]));
        Assert.Equal(["A", "C", "D"], flags.Maximum(["A", "C", "D"]));
        Assert.Equal(["A"], flags.Maximum(["A", "E"]));
    }

    [Fact]
    public void Add()
    {
        var flags = new CollectionFlagSet<string>();
        var flagB = flags.Flag("B");
        var flagC = flags.Flag("C");
        var flagsBAndC = flags.Flag(flagB, flagC);

        Assert.Equal(ImmutableHashSet.Create("A", "B"), ImmutableHashSet.Create("A") + flagB);
        Assert.Equal(ImmutableHashSet.Create("A", "C"), ImmutableHashSet.Create("A") + flagC);
        Assert.Equal(ImmutableHashSet.Create("A", "B", "C"), ImmutableHashSet.Create("A") + flagsBAndC);
    }

    [Fact]
    public void Remove()
    {
        var flags = new CollectionFlagSet<string>();
        var flagA = flags.Flag("A");
        var flagB = flags.Flag("B");
        var flagC = flags.Flag("C", flagA);

        Assert.Equal(ImmutableHashSet.Create("B"), ImmutableHashSet.Create("A", "B", "C") - flagA);
        Assert.Equal(ImmutableHashSet.Create("A", "C"), ImmutableHashSet.Create("A", "B", "C") - flagB);
        Assert.Equal(ImmutableHashSet.Create("A", "B"), ImmutableHashSet.Create("A", "B", "C") - flagC);
    }

    [Fact]
    public void IsIn()
    {
        var flags = new CollectionFlagSet<string>();
        var flagA = flags.Flag("A");
        var flagB = flags.Flag("B");
        var flagC = flags.Flag("C", flagA);

        Assert.True(flagA.IsIn(ImmutableHashSet.Create("A")));
        Assert.True(flagB.IsIn(ImmutableHashSet.Create("A", "B")));
        Assert.False(flagC.IsIn(ImmutableHashSet.Create("C")));
        Assert.True(flagC.IsIn(ImmutableHashSet.Create("A", "C")));
    }

    [Fact]
    public void IsAbstract()
    {
        var flags = new CollectionFlagSet<string>();
        var flagA = flags.Flag("A");
        var flagB = flags.Flag("B");
        var flagsAAndB = flags.Flag(flagA, flagB);
        var flagC = flags.Flag("C", flagsAAndB);

        Assert.False(flagA.IsAbstract);
        Assert.False(flagB.IsAbstract);
        Assert.True(flagsAAndB.IsAbstract);
        Assert.False(flagC.IsAbstract);
    }
}