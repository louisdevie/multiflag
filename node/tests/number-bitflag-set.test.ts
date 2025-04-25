import { NumberBitflagSet, InvalidBitflagValueError } from '@module'

test("Can't create flags with values that are not powers of two", () => {
    const flags = new NumberBitflagSet()
    expect(() => flags.flag(0)).toThrow(InvalidBitflagValueError)
    expect(() => flags.flag(11)).toThrow(InvalidBitflagValueError)
})

test('Add', () => {
    const flags = new NumberBitflagSet()
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4)
    const flags2And4 = flags.flag(flag2, flag4)

    expect(flag2.addTo(1)).toEqual(3)
    expect(flag4.addTo(1)).toEqual(5)
    expect(flags2And4.addTo(1)).toEqual(7)
})

test('Remove', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4, flag1)

    expect(flag1.removeFrom(7)).toEqual(2)
    expect(flag2.removeFrom(7)).toEqual(5)
    expect(flag4.removeFrom(7)).toEqual(3)
})

test('IsIn', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag4 = flags.flag(4, flag1)

    expect(flag1.isIn(1)).toBe(true)
    expect(flag2.isIn(3)).toBe(true)
    expect(flag4.isIn(4)).toBe(false)
    expect(flag4.isIn(5)).toBe(true)
})

test('IsAbstract', () => {
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
