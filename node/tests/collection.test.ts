import { CollectionFlagSet } from '@module'
import { ok } from 'node:assert'

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

test('Environment without Set', async () => {
    const originalSet = globalThis.Set
    // @ts-ignore
    delete globalThis.Set
    let module: { CollectionFlagSet: typeof CollectionFlagSet }
    await jest.isolateModulesAsync(async () => {
        module = await import('@module')
    })

    expect(() => new module.CollectionFlagSet()).toThrow()

    globalThis.Set = originalSet
})

test('Environment with Set.prototype.union', async () => {
    ok(!('union' in globalThis.Set.prototype))

    const mockUnion = jest.fn((_) => new Set())
    // @ts-ignore
    globalThis.Set.prototype.union = mockUnion
    let module: { CollectionFlagSet: typeof CollectionFlagSet } | undefined
    await jest.isolateModulesAsync(async () => {
        module = await import('@module')
    })
    ok(module !== undefined)

    const set1 = new Set()
    const set2 = new Set()
    const flags = new module.CollectionFlagSet()
    flags.union(set1, set2)
    expect(mockUnion).toHaveBeenCalledTimes(1)
    expect(mockUnion.mock.contexts[0]).toBe(set1)
    expect(mockUnion.mock.calls[0][0]).toBe(set2)

    // @ts-ignore
    delete globalThis.Set.prototype.union
})

test('Environment with Set.prototype.difference', async () => {
    ok(!('difference' in globalThis.Set.prototype))

    const mockDifference = jest.fn((_) => new Set())
    // @ts-ignore
    globalThis.Set.prototype.difference = mockDifference
    let module: { CollectionFlagSet: typeof CollectionFlagSet } | undefined
    await jest.isolateModulesAsync(async () => {
        module = await import('@module')
    })
    ok(module !== undefined)

    const set1 = new Set()
    const set2 = new Set()
    const flags = new module.CollectionFlagSet()
    flags.difference(set1, set2)
    expect(mockDifference).toHaveBeenCalledTimes(1)
    expect(mockDifference.mock.contexts[0]).toBe(set1)
    expect(mockDifference.mock.calls[0][0]).toBe(set2)

    // @ts-ignore
    delete globalThis.Set.prototype.difference
})

test('Environment with Set.prototype.intersection', async () => {
    ok(!('intersection' in globalThis.Set.prototype))

    const mockIntersection = jest.fn((_) => new Set())
    // @ts-ignore
    globalThis.Set.prototype.intersection = mockIntersection
    let module: { CollectionFlagSet: typeof CollectionFlagSet } | undefined
    await jest.isolateModulesAsync(async () => {
        module = await import('@module')
    })
    ok(module !== undefined)

    const set1 = new Set()
    const set2 = new Set()
    const flags = new module.CollectionFlagSet()
    flags.intersection(set1, set2)
    expect(mockIntersection).toHaveBeenCalledTimes(1)
    expect(mockIntersection.mock.contexts[0]).toBe(set1)
    expect(mockIntersection.mock.calls[0][0]).toBe(set2)

    // @ts-ignore
    delete globalThis.Set.prototype.intersection
})

test('Environment with Set.prototype.isSupersetOf', async () => {
    ok(!('isSupersetOf' in globalThis.Set.prototype))

    const mockIsSupersetOf = jest.fn((_) => new Set())
    // @ts-ignore
    globalThis.Set.prototype.isSupersetOf = mockIsSupersetOf
    let module: { CollectionFlagSet: typeof CollectionFlagSet } | undefined
    await jest.isolateModulesAsync(async () => {
        module = await import('@module')
    })
    ok(module !== undefined)

    const set1 = new Set()
    const set2 = new Set()
    const flags = new module.CollectionFlagSet()
    flags.isSupersetOf(set1, set2)
    expect(mockIsSupersetOf).toHaveBeenCalledTimes(1)
    expect(mockIsSupersetOf.mock.contexts[0]).toBe(set1)
    expect(mockIsSupersetOf.mock.calls[0][0]).toBe(set2)

    // @ts-ignore
    delete globalThis.Set.prototype.isSupersetOf
})
