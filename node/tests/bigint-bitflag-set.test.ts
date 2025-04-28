import { DynamicBitflagSet, InvalidBitflagValueError } from '@module'

const bigPowerOfTwo = 2n ** 100n

test('Not powers of two', () => {
    const flags = new DynamicBitflagSet()
    expect(() => flags.flag(0n)).toThrow(InvalidBitflagValueError)
    expect(() => flags.flag(11n)).toThrow(InvalidBitflagValueError)
})

test('Union of two numbers', () => {
    const flags = new DynamicBitflagSet()

    expect(flags.union(0n, 0n)).toEqual(0n)
    expect(flags.union(1n, 0n)).toEqual(1n)
    expect(flags.union(0n, 2n)).toEqual(2n)
    expect(flags.union(1n, 2n)).toEqual(3n)
    expect(flags.union(3n, 6n)).toEqual(7n)
})

test('Difference of two numbers', () => {
    const flags = new DynamicBitflagSet()

    expect(flags.difference(0n, 0n)).toEqual(0n)
    expect(flags.difference(1n, 0n)).toEqual(1n)
    expect(flags.difference(3n, 6n)).toEqual(1n)
    expect(flags.difference(6n, 3n)).toEqual(4n)
    expect(flags.difference(8n, 17n)).toEqual(8n)
})

test('Intersection of two numbers', () => {
    const flags = new DynamicBitflagSet()

    expect(flags.intersection(0n, 0n)).toEqual(0n)
    expect(flags.intersection(1n, 0n)).toEqual(0n)
    expect(flags.intersection(1n, 2n)).toEqual(0n)
    expect(flags.intersection(1n, 3n)).toEqual(1n)
    expect(flags.intersection(11n, 5n)).toEqual(1n)
    expect(flags.intersection(11n, 7n)).toEqual(3n)
})

test('Iterate over a number', () => {
    const flags = new DynamicBitflagSet()

    expect([...flags.iterate(0n)]).toEqual([])
    expect([...flags.iterate(1n)]).toEqual([1n])
    expect([...flags.iterate(2n)]).toEqual([2n])
    expect([...flags.iterate(3n)]).toEqual([1n, 2n])
    expect([...flags.iterate(11n)]).toEqual([1n, 2n, 8n])
    expect([...flags.iterate(100n)]).toEqual([4n, 32n, 64n])
})

test('Normalise to minimum', () => {
    const flags = new DynamicBitflagSet()
    const flag1 = flags.flag(1n)
    const flag2 = flags.flag(2n, flag1)
    const flag4 = flags.flag(4n, flag1)
    const flag8 = flags.flag(8n, flag4)

    expect(flags.minimum(0n)).toEqual(0n)
    expect(flags.minimum(1n)).toEqual(1n)
    expect(flags.minimum(2n)).toEqual(0n)
    expect(flags.minimum(3n)).toEqual(3n)
    expect(flags.minimum(11n)).toEqual(3n)
    expect(flags.minimum(13n)).toEqual(13n)
    expect(flags.minimum(17n)).toEqual(1n)
})

test('Normalise to maximum', () => {
    const flags = new DynamicBitflagSet()
    const flag1 = flags.flag(1n)
    const flag2 = flags.flag(2n, flag1)
    const flag4 = flags.flag(4n, flag1)
    const flag8 = flags.flag(8n, flag4)

    expect(flags.maximum(0n)).toEqual(0n)
    expect(flags.maximum(1n)).toEqual(1n)
    expect(flags.maximum(2n)).toEqual(3n)
    expect(flags.maximum(3n)).toEqual(3n)
    expect(flags.maximum(11n)).toEqual(15n)
    expect(flags.maximum(13n)).toEqual(13n)
    expect(flags.maximum(17n)).toEqual(1n)
})

test('Add to bigint', () => {
    const flags = new DynamicBitflagSet()
    const flag2 = flags.flag(2n)
    const flag4 = flags.flag(4n)
    const flags2And4 = flags.flag(flag2, flag4)
    const flag100 = flags.flag(bigPowerOfTwo)

    expect(flag2.addTo(1n)).toEqual(3n)
    expect(flag4.addTo(1n)).toEqual(5n)
    expect(flags2And4.addTo(1n)).toEqual(7n)
    expect(flag100.addTo(1n)).toEqual(bigPowerOfTwo + 1n)
    expect(flag2.addTo(bigPowerOfTwo)).toEqual(bigPowerOfTwo + 2n)
})

test('Remove from bigint', () => {
    const flags = new DynamicBitflagSet()
    const flag1 = flags.flag(1n)
    const flag2 = flags.flag(2n)
    const flag4 = flags.flag(4n, flag1)
    const flag100 = flags.flag(bigPowerOfTwo)

    expect(flag1.removeFrom(7n)).toEqual(2n)
    expect(flag2.removeFrom(7n)).toEqual(5n)
    expect(flag4.removeFrom(7n)).toEqual(3n)
    expect(flag100.removeFrom(bigPowerOfTwo + 2n)).toEqual(2n)
    expect(flag100.removeFrom(2n)).toEqual(2n)
    expect(flag1.removeFrom(bigPowerOfTwo - 1n)).toEqual(bigPowerOfTwo - 6n)
})

test('Is in bigint', () => {
    const flags = new DynamicBitflagSet()
    const flag1 = flags.flag(1n)
    const flag2 = flags.flag(2n)
    const flag4 = flags.flag(4n, flag1)

    expect(flag1.isIn(1n)).toBe(true)
    expect(flag2.isIn(3n)).toBe(true)
    expect(flag4.isIn(4n)).toBe(false)
    expect(flag4.isIn(5n)).toBe(true)
    expect(flag1.isIn(bigPowerOfTwo + 1n)).toBe(true)
})

test('Is abstract', () => {
    const flags = new DynamicBitflagSet()
    const flag1 = flags.flag(1n)
    const flag2 = flags.flag(2n)
    const flags1And2 = flags.flag(flag1, flag2)
    const flag4 = flags.flag(4n, flags1And2)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flags1And2.isAbstract).toBe(true)
    expect(flag4.isAbstract).toBe(false)
})
