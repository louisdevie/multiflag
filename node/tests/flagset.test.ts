import { NumberBitflagSet, ReusedFlagValueError } from '@module'

test('cannot create an abstract flag with less than two parents', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)

    expect(() => flags.flag()).toThrow(TypeError)
    expect(() => flags.flag(flag1)).toThrow(TypeError)
    expect(() => flags.flag(flag1, flag1)).toThrow(TypeError)
})

test('calls to flag() with arguments in the wrong order throw a TypeError', () => {
    const flags = new NumberBitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)

    // @ts-ignore
    expect(() => flags.flag(flag1, 2, flag2)).toThrow(TypeError)
})

test('Use same value twice', () => {
    const flags = new NumberBitflagSet()
    const flag = flags.flag(1)
    expect(() => flags.flag(1)).toThrow(ReusedFlagValueError)
})
