import { ArrayFlagSet } from '@module'

test('Union of two arrays', () => {
    const flags = new ArrayFlagSet<string>()

    expect(flags.union([], [])).toEqual([])
    expect(flags.union(['A'], [])).toEqual(['A'])
    expect(flags.union([], ['B'])).toEqual(['B'])
    expect(flags.union(['A'], ['B'])).toEqual(['A', 'B'])
    expect(flags.union(['A', 'B'], ['B', 'C'])).toEqual(['A', 'B', 'C'])
})

test('Difference of two arrays', () => {
    const flags = new ArrayFlagSet<string>()

    expect(flags.difference([], [])).toEqual([])
    expect(flags.difference(['A'], [])).toEqual(['A'])
    expect(flags.difference(['A', 'B'], ['B', 'C'])).toEqual(['A'])
    expect(flags.difference(['B', 'C'], ['A', 'B'])).toEqual(['C'])
    expect(flags.difference(['D'], ['A', 'E'])).toEqual(['D'])
})

test('Intersection of two arrays', () => {
    const flags = new ArrayFlagSet<string>()

    expect(flags.intersection([], [])).toEqual([])
    expect(flags.intersection(['A'], [])).toEqual([])
    expect(flags.intersection(['A'], ['B'])).toEqual([])
    expect(flags.intersection(['A'], ['A', 'B'])).toEqual(['A'])
    expect(flags.intersection(['A', 'B', 'D'], ['A', 'C'])).toEqual(['A'])
    expect(flags.intersection(['A', 'B', 'D'], ['A', 'B', 'C'])).toEqual([
        'A',
        'B',
    ])
})

test('Iterate over an array', () => {
    const flags = new ArrayFlagSet<string>()

    expect([...flags.iterate([])]).toEqual([])
    expect([...flags.iterate(['A'])]).toEqual(['A'])
    expect([...flags.iterate(['A', 'B', 'C'])]).toEqual(['A', 'B', 'C'])
})

test('Normalise to minimum', () => {
    const flags = new ArrayFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B', flagA)
    const flagC = flags.flag('C', flagA)
    const flagD = flags.flag('D', flagC)

    expect(flags.minimum([])).toEqual([])
    expect(flags.minimum(['A'])).toEqual(['A'])
    expect(flags.minimum(['B'])).toEqual([])
    expect(flags.minimum(['A', 'B'])).toEqual(['A', 'B'])
    expect(flags.minimum(['A', 'B', 'D'])).toEqual(['A', 'B'])
    expect(flags.minimum(['A', 'C', 'D'])).toEqual(['A', 'C', 'D'])
    expect(flags.minimum(['A', 'E'])).toEqual(['A'])
})

test('Normalise to maximum', () => {
    const flags = new ArrayFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B', flagA)
    const flagC = flags.flag('C', flagA)
    const flagD = flags.flag('D', flagC)

    expect(flags.maximum([])).toEqual([])
    expect(flags.maximum(['A'])).toEqual(['A'])
    expect(flags.maximum(['B'])).toEqual(['B', 'A'])
    expect(flags.maximum(['A', 'B'])).toEqual(['A', 'B'])
    expect(flags.maximum(['A', 'B', 'D'])).toEqual(['A', 'B', 'D', 'C'])
    expect(flags.maximum(['A', 'C', 'D'])).toEqual(['A', 'C', 'D'])
    expect(flags.maximum(['A', 'E'])).toEqual(['A'])
})

test('Add to array', () => {
    const flags = new ArrayFlagSet<string>()
    const flagB = flags.flag('B')
    const flagC = flags.flag('C')
    const flagsBAndC = flags.flag(flagB, flagC)

    expect(flagB.addTo(['A'])).toEqual(['A', 'B'])
    expect(flagC.addTo(['A'])).toEqual(['A', 'C'])
    expect(flagsBAndC.addTo(['A'])).toEqual(['A', 'B', 'C'])
})

test('Remove from array', () => {
    const flags = new ArrayFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagC = flags.flag('C', flagA)

    expect(flagA.removeFrom(['A', 'B', 'C'])).toEqual(['B'])
    expect(flagB.removeFrom(['A', 'B', 'C'])).toEqual(['A', 'C'])
    expect(flagC.removeFrom(['A', 'B', 'C'])).toEqual(['A', 'B'])
})

test('Is in array', () => {
    const flags = new ArrayFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagC = flags.flag('C', flagA)

    expect(flagA.isIn(['A'])).toBe(true)
    expect(flagB.isIn(['A', 'B'])).toBe(true)
    expect(flagC.isIn(['C'])).toBe(false)
    expect(flagC.isIn(['A', 'C'])).toBe(true)
})

test('Is abstract', () => {
    const flags = new ArrayFlagSet<string>()
    const flagA = flags.flag('A')
    const flagB = flags.flag('B')
    const flagsAAndB = flags.flag(flagA, flagB)
    const flagC = flags.flag('C', flagsAAndB)

    expect(flagA.isAbstract).toBe(false)
    expect(flagB.isAbstract).toBe(false)
    expect(flagsAAndB.isAbstract).toBe(true)
    expect(flagC.isAbstract).toBe(false)
})
