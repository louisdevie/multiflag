import { NumberBitflagSet, InvalidBitflagValueError } from '@module'

test('Not powers of two', () => {
    const flags = new NumberBitflagSet()
    expect(() => flags.flag(0)).toThrow(InvalidBitflagValueError)
    expect(() => flags.flag(11)).toThrow(InvalidBitflagValueError)
})

test('Union of two numbers', () => {
    const flags = new NumberBitflagSet()

    expect(flags.union(0, 0)).toEqual(0)
    expect(flags.union(1, 0)).toEqual(1)
    expect(flags.union(0, 2)).toEqual(2)
    expect(flags.union(1, 2)).toEqual(3)
    expect(flags.union(3, 6)).toEqual(7)
})

test('Iterate over a number', () => {
    const flags = new NumberBitflagSet()

    expect([...flags.iterate(0)]).toEqual([])
    expect([...flags.iterate(1)]).toEqual([1])
    expect([...flags.iterate(2)]).toEqual([2])
    expect([...flags.iterate(3)]).toEqual([1, 2])
    expect([...flags.iterate(11)]).toEqual([1, 2, 8])
    expect([...flags.iterate(100)]).toEqual([4, 32, 64])
})

test('Normalise to minimum', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2, flag1)
    const flag4 = flags.flag(4, flag1)
    const flag8 = flags.flag(8, flag4)

    expect(flags.minimum(0)).toEqual(0)
    expect(flags.minimum(1)).toEqual(1)
    expect(flags.minimum(2)).toEqual(0)
    expect(flags.minimum(3)).toEqual(3)
    expect(flags.minimum(11)).toEqual(3)
    expect(flags.minimum(13)).toEqual(13)
    expect(flags.minimum(17)).toEqual(1)
})

test('Normalise to maximum', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2, flag1)
    const flag4 = flags.flag(4, flag1)
    const flag8 = flags.flag(8, flag4)

    expect(flags.maximum(0)).toEqual(0)
    expect(flags.maximum(1)).toEqual(1)
    expect(flags.maximum(2)).toEqual(3)
    expect(flags.maximum(3)).toEqual(3)
    expect(flags.maximum(11)).toEqual(15)
    expect(flags.maximum(13)).toEqual(13)
    expect(flags.maximum(17)).toEqual(1)
})

test('Add to number', () => {
    const flags = new NumberBitflagSet()
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4)
    const flags2And4 = flags.flag(flag2, flag4)

    expect(flag2.addTo(1)).toEqual(3)
    expect(flag4.addTo(1)).toEqual(5)
    expect(flags2And4.addTo(1)).toEqual(7)
})

test('Remove from number', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4, flag1)

    expect(flag1.removeFrom(7)).toEqual(2)
    expect(flag2.removeFrom(7)).toEqual(5)
    expect(flag4.removeFrom(7)).toEqual(3)
})

test('Is in number', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4, flag1)

    expect(flag1.isIn(1)).toBe(true)
    expect(flag2.isIn(3)).toBe(true)
    expect(flag4.isIn(4)).toBe(false)
    expect(flag4.isIn(5)).toBe(true)
})

test('Is abstract', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flags1And2 = flags.flag(flag1, flag2)
    const flag4 = flags.flag(4, flags1And2)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flags1And2.isAbstract).toBe(true)
    expect(flag4.isAbstract).toBe(false)
})
