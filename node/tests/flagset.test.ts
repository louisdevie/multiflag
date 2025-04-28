import { NumberBitflagSet } from '@module'
import { ForeignFlagError } from '../src/errors'
import { FlagSet } from '../src/flagset'

test('cannot create an abstract flag with less than two parents', () => {
    const flags = new NumberBitflagSet()
    const flagA = flags.flag(1)

    expect(() => flags.flag()).toThrow(TypeError)
    expect(() => flags.flag(flagA)).toThrow(TypeError)
    expect(() => flags.flag(flagA, flagA)).toThrow(TypeError)
})
