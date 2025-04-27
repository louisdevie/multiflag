import { ArrayFlagSet } from '@module'

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
