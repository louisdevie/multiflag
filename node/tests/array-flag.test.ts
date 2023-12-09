import ArrayFlag from '../src/array-flag'

class Set {
    public static get Empty(): string[] { return [] }
    public static get A() { return ['A'] }
    public static get B() { return ['B'] }
    public static get AB() { return ['A', 'B'] }
    public static get BC() { return ['B', 'C'] }
    public static get AC() { return ['A', 'C'] }
    public static get ABC() { return ['A', 'B', 'C'] }
}

function sorted<T>(array: T[]) : T[] {
    let copy = Array.from(array);
    copy.sort()
    return copy
}

test('Addition of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B')
    let flag4 = ArrayFlag.withValue('C', flag2)

    let flags = Set.Empty
    expect(flag1.addTo(flags)).toEqual(Set.Empty)
    flags = Set.Empty
    expect(flag2.addTo(flags)).toEqual(Set.A)
    flags = Set.Empty
    expect(flag3.addTo(flags)).toEqual(Set.B)
    flags = Set.Empty
    expect(sorted(flag4.addTo(flags))).toEqual(Set.AC)

    flags = ['B']
    expect(flag1.addTo(flags)).toEqual(Set.B)
    flags = ['B']
    expect(sorted(flag2.addTo(flags))).toEqual(Set.AB)
    flags = ['B']
    expect(flag3.addTo(flags)).toEqual(Set.B)
    flags = ['B']
    expect(sorted(flag4.addTo(flags))).toEqual(Set.ABC)

    flags = ['A', 'B', 'C']
    expect(sorted(flag1.addTo(flags))).toEqual(Set.ABC)
    flags = ['A', 'B', 'C']
    expect(sorted(flag2.addTo(flags))).toEqual(Set.ABC)
    flags = ['A', 'B', 'C']
    expect(sorted(flag3.addTo(flags))).toEqual(Set.ABC)
    flags = ['A', 'B', 'C']
    expect(sorted(flag4.addTo(flags))).toEqual(Set.ABC)
})

test('Subtraction of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B')
    let flag4 = ArrayFlag.withValue('C', flag2)

    let flags = Set.Empty
    expect(flag1.removeFrom(flags)).toEqual(Set.Empty)
    flags = Set.Empty
    expect(flag2.removeFrom(flags)).toEqual(Set.Empty)
    flags = Set.Empty
    expect(flag3.removeFrom(flags)).toEqual(Set.Empty)
    flags = Set.Empty
    expect(flag4.removeFrom(flags)).toEqual(Set.Empty)

    flags = Set.AB
    expect(sorted(flag1.removeFrom(flags))).toEqual(Set.AB)
    flags = Set.AB
    expect(flag2.removeFrom(flags)).toEqual(Set.B)
    flags = Set.AB
    expect(flag3.removeFrom(flags)).toEqual(Set.A)
    flags = Set.AB
    expect(sorted(flag4.removeFrom(flags))).toEqual(Set.AB)

    flags = Set.ABC
    expect(sorted(flag1.removeFrom(flags))).toEqual(Set.ABC)
    flags = Set.ABC
    expect(flag2.removeFrom(flags)).toEqual(Set.B)
    flags = Set.ABC
    expect(sorted(flag3.removeFrom(flags))).toEqual(Set.AC)
    flags = Set.ABC
    expect(sorted(flag4.removeFrom(flags))).toEqual(Set.AB)
})

test('Inclusion of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B')
    let flag4 = ArrayFlag.withValue('C', flag2)

    let flags = Set.Empty
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(false)
    expect(flag3.in(flags)).toBe(false)
    expect(flag4.in(flags)).toBe(false)

    flags = Set.BC
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(false)
    expect(flag3.in(flags)).toBe(true)
    expect(flag4.in(flags)).toBe(false)

    flags = Set.ABC
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(true)
    expect(flag3.in(flags)).toBe(true)
    expect(flag4.in(flags)).toBe(true)
})

test('Properties of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B', flag1)
    let flag4 = ArrayFlag.withoutValue(flag1)
    let flag5 = ArrayFlag.withValue('C', flag2)
    let flag6 = ArrayFlag.withoutValue(flag2)
    let flag7 = ArrayFlag.withoutValue(flag4, flag6)

    expect(flag1.isNothing).toBe(true)
    expect(flag2.isNothing).toBe(false)
    expect(flag3.isNothing).toBe(false)
    expect(flag4.isNothing).toBe(true)
    expect(flag5.isNothing).toBe(false)
    expect(flag6.isNothing).toBe(false)
    expect(flag7.isNothing).toBe(false)

    expect(flag1.isConcrete).toBe(false)
    expect(flag2.isConcrete).toBe(true)
    expect(flag3.isConcrete).toBe(true)
    expect(flag4.isConcrete).toBe(false)
    expect(flag5.isConcrete).toBe(true)
    expect(flag6.isConcrete).toBe(false)
    expect(flag7.isConcrete).toBe(false)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flag3.isAbstract).toBe(false)
    expect(flag4.isAbstract).toBe(false)
    expect(flag5.isAbstract).toBe(false)
    expect(flag6.isAbstract).toBe(true)
    expect(flag7.isAbstract).toBe(true)
})

test('Positive equality of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue<string>()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B', flag2)
    let flag4 = ArrayFlag.withoutValue(flag2)

    expect(flag1.positiveEquals(flag1)).toBe(true)
    expect(flag1.positiveEquals(flag2)).toBe(false)
    expect(flag1.positiveEquals(flag3)).toBe(false)
    expect(flag1.positiveEquals(flag4)).toBe(false)

    expect(flag2.positiveEquals(flag1)).toBe(false)
    expect(flag2.positiveEquals(flag2)).toBe(true)
    expect(flag2.positiveEquals(flag3)).toBe(false)
    expect(flag2.positiveEquals(flag4)).toBe(true)

    expect(flag3.positiveEquals(flag1)).toBe(false)
    expect(flag3.positiveEquals(flag2)).toBe(false)
    expect(flag3.positiveEquals(flag3)).toBe(true)
    expect(flag3.positiveEquals(flag4)).toBe(false)

    expect(flag4.positiveEquals(flag1)).toBe(false)
    expect(flag4.positiveEquals(flag2)).toBe(true)
    expect(flag4.positiveEquals(flag3)).toBe(false)
    expect(flag4.positiveEquals(flag4)).toBe(true)
})

test('Negative equality of ArrayFlags', () => {
    let flag1 = ArrayFlag.withoutValue<string>()
    let flag2 = ArrayFlag.withValue('A')
    let flag3 = ArrayFlag.withValue('B', flag2)
    let flag4 = ArrayFlag.withoutValue(flag2)

    expect(flag1.negativeEquals(flag1)).toBe(true)
    expect(flag1.negativeEquals(flag2)).toBe(false)
    expect(flag1.negativeEquals(flag3)).toBe(false)
    expect(flag1.negativeEquals(flag4)).toBe(true)

    expect(flag2.negativeEquals(flag1)).toBe(false)
    expect(flag2.negativeEquals(flag2)).toBe(true)
    expect(flag2.negativeEquals(flag3)).toBe(false)
    expect(flag2.negativeEquals(flag4)).toBe(false)

    expect(flag3.negativeEquals(flag1)).toBe(false)
    expect(flag3.negativeEquals(flag2)).toBe(false)
    expect(flag3.negativeEquals(flag3)).toBe(true)
    expect(flag3.negativeEquals(flag4)).toBe(false)

    expect(flag4.negativeEquals(flag1)).toBe(true)
    expect(flag4.negativeEquals(flag2)).toBe(false)
    expect(flag4.negativeEquals(flag3)).toBe(false)
    expect(flag4.negativeEquals(flag4)).toBe(true)
})
