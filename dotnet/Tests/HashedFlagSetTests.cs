using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Multiflag.Tests;

public class HashedFlagSetTests
{
    [Fact]
    public void Add()
    {
        var flags = new HashedFlagSet<string>();
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
        var flags = new HashedFlagSet<string>();
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
        var flags = new HashedFlagSet<string>();
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
        var flags = new HashedFlagSet<string>();
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