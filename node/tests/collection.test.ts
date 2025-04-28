import { CollectionFlagSet } from '@module'

function set<T>(...values: T[]): Set<T> {
    return new Set<T>(values)
}

test('Union of two sets', () => {
    const flags = new CollectionFlagSet<string>()

    expect(flags.union(set(), set())).toEqual(set())
    expect(flags.union(set('A'), set())).toEqual(set('A'))
    expect(flags.union(set(), set('B'))).toEqual(set('B'))
    expect(flags.union(set('A'), set('B'))).toEqual(set('A', 'B'))
    expect(flags.union(set('A', 'B'), set('B', 'C'))).toEqual(
        set('A', 'B', 'C')
    )
})

test('Difference of two sets', () => {
    const flags = new CollectionFlagSet<string>()

    expect(flags.difference(set(), set())).toEqual(set())
    expect(flags.difference(set('A'), set())).toEqual(set('A'))
    expect(flags.difference(set('A', 'B'), set('B', 'C'))).toEqual(set('A'))
    expect(flags.difference(set('B', 'C'), set('A', 'B'))).toEqual(set('C'))
    expect(flags.difference(set('D'), set('A', 'E'))).toEqual(set('D'))
})

test('Intersection of two sets', () => {
    const flags = new CollectionFlagSet<string>()

    expect(flags.intersection(set(), set())).toEqual(set())
    expect(flags.intersection(set('A'), set())).toEqual(set())
    expect(flags.intersection(set('A'), set('B'))).toEqual(set())
    expect(flags.intersection(set('A'), set('A', 'B'))).toEqual(set('A'))
    expect(flags.intersection(set('A', 'B', 'D'), set('A', 'C'))).toEqual(
        set('A')
    )
    expect(flags.intersection(set('A', 'B', 'D'), set('A', 'B', 'C'))).toEqual(
        set('A', 'B')
    )
})

test('Iterate over a set', () => {
    const flags = new CollectionFlagSet<string>()

    expect([...flags.iterate(set())]).toEqual([])
    expect([...flags.iterate(set('A'))]).toEqual(['A'])
    expect([...flags.iterate(set('A', 'B', 'C'))]).toEqual(['A', 'B', 'C'])
})

test('Normalise to minimum', () => {
    const flags = new CollectionFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B', flagA)
    const flagC = flags.flag('C', flagA)
    const flagD = flags.flag('D', flagC)

    expect(flags.minimum(set())).toEqual(set())
    expect(flags.minimum(set('A'))).toEqual(set('A'))
    expect(flags.minimum(set('B'))).toEqual(set())
    expect(flags.minimum(set('A', 'B'))).toEqual(set('A', 'B'))
    expect(flags.minimum(set('A', 'B', 'D'))).toEqual(set('A', 'B'))
    expect(flags.minimum(set('A', 'C', 'D'))).toEqual(set('A', 'C', 'D'))
    expect(flags.minimum(set('A', 'E'))).toEqual(set('A'))
})

test('Normalise to maximum', () => {
    const flags = new CollectionFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B', flagA)
    const flagC = flags.flag('C', flagA)
    const flagD = flags.flag('D', flagC)

    expect(flags.maximum(set())).toEqual(set())
    expect(flags.maximum(set('A'))).toEqual(set('A'))
    expect(flags.maximum(set('B'))).toEqual(set('B', 'A'))
    expect(flags.maximum(set('A', 'B'))).toEqual(set('A', 'B'))
    expect(flags.maximum(set('A', 'B', 'D'))).toEqual(set('A', 'B', 'D', 'C'))
    expect(flags.maximum(set('A', 'C', 'D'))).toEqual(set('A', 'C', 'D'))
    expect(flags.maximum(set('A', 'E'))).toEqual(set('A'))
})

test('Add to set', () => {
    const flags = new CollectionFlagSet<string>()
    const flagB = flags.flag('B')
    const flagC = flags.flag('C')
    const flagsBAndC = flags.flag(flagB, flagC)

    expect(flagB.addTo(set('A'))).toEqual(set('A', 'B'))
    expect(flagC.addTo(set('A'))).toEqual(set('A', 'C'))
    expect(flagsBAndC.addTo(set('A'))).toEqual(set('A', 'B', 'C'))
})

test('Remove from set', () => {
    const flags = new CollectionFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagC = flags.flag('C', flagA)

    expect(flagA.removeFrom(set('A', 'B', 'C'))).toEqual(set('B'))
    expect(flagB.removeFrom(set('A', 'B', 'C'))).toEqual(set('A', 'C'))
    expect(flagC.removeFrom(set('A', 'B', 'C'))).toEqual(set('A', 'B'))
})

test('Is in set', () => {
    const flags = new CollectionFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagC = flags.flag('C', flagA)

    expect(flagA.isIn(set('A'))).toBe(true)
    expect(flagB.isIn(set('A', 'B'))).toBe(true)
    expect(flagC.isIn(set('C'))).toBe(false)
    expect(flagC.isIn(set('A', 'C'))).toBe(true)
})

test('Is abstract', () => {
    const flags = new CollectionFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagsAAndB = flags.flag(flagA, flagB)
    const flagC = flags.flag('C', flagsAAndB)

    expect(flagA.isAbstract).toBe(false)
    expect(flagB.isAbstract).toBe(false)
    expect(flagsAAndB.isAbstract).toBe(true)
    expect(flagC.isAbstract).toBe(false)
})
