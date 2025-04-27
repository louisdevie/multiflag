import { CollectionFlagSet } from '@module'

function set<T>(...values: T[]): Set<T> {
    return new Set<T>(values)
}

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
