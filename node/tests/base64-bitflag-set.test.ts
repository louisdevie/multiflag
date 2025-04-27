import { Base64BitflagSet, InvalidBitflagValueError } from '@module'

test('Create from an index', () => {
    const flags = new Base64BitflagSet()

    const flag2 = flags.flag(2)
    expect(flag2.addTo('')).toEqual('C')

    expect(() => flags.flag(0)).toThrow(RangeError)
    expect(() => flags.flag(-2)).toThrow(RangeError)
})

test('Create from a string', () => {
    const flags = new Base64BitflagSet()

    expect(() => flags.flag('')).toThrow(InvalidBitflagValueError)
    expect(() => flags.flag('A')).toThrow(InvalidBitflagValueError)
    flags.flag('B')
    flags.flag('C')
    expect(() => flags.flag('D')).toThrow(InvalidBitflagValueError)
    flags.flag('E')
    flags.flag('EAA')
    expect(() => flags.flag('AAD')).toThrow(InvalidBitflagValueError)
})

test('Add to base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3)
    const flags2And3 = flags.flag(flag2, flag3)

    expect(flag2.addTo('B')).toEqual('D')
    expect(flag3.addTo('B')).toEqual('F')
    expect(flags2And3.addTo('B')).toEqual('H')
})

test('Remove from base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3, flag1)

    expect(flag1.removeFrom('H')).toEqual('C')
    expect(flag2.removeFrom('H')).toEqual('F')
    expect(flag3.removeFrom('H')).toEqual('D')
})

test('Is in base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3, flag1)

    expect(flag1.isIn('B')).toBe(true)
    expect(flag2.isIn('D')).toBe(true)
    expect(flag3.isIn('E')).toBe(false)
    expect(flag3.isIn('F')).toBe(true)
})

test('Is abstract', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flags1And2 = flags.flag(flag1, flag2)
    const flag3 = flags.flag(3, flags1And2)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flags1And2.isAbstract).toBe(true)
    expect(flag3.isAbstract).toBe(false)
})
