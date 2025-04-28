import { NumberBitflagSet } from '@module'
import { ForeignFlagError } from '../src/errors'

test('cannot create a flag with a foreign parent', () => {
    const flags = new NumberBitflagSet()
    const flagA = flags.flag(1)

    const otherFlags = new NumberBitflagSet()
    const flagX = otherFlags.flag(1)

    flags.flag(2, flagA) // OK
    expect(() => flags.flag(4, flagX)).toThrow(ForeignFlagError)
})
