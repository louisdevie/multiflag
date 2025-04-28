import { FlagSet } from './flagset'

const ZERO = 65 // A
const TWENTY_SIX = 97 // a
const FIFTY_TWO = 48 // 0
const SIXTY_TWO = 45 // -
const SIXTY_THREE = 95 // _

const ZERO_STRING = 'A'

function encodeByte(byte: number): string {
    let charCode
    if (byte < 26) {
        charCode = byte + ZERO
    } else if (byte < 52) {
        charCode = byte - 26 + TWENTY_SIX
    } else if (byte < 62) {
        charCode = byte - 52 + FIFTY_TWO
    } else {
        charCode = byte == 62 ? SIXTY_TWO : SIXTY_THREE
    }
    return String.fromCharCode(charCode)
}

function decodeByte(encodedByte: string): number {
    const charCode = encodedByte.charCodeAt(0)
    if (charCode == SIXTY_THREE) {
        return 63
    } else if (charCode == SIXTY_TWO) {
        return 62
    } else if (charCode >= TWENTY_SIX) {
        return -TWENTY_SIX + 26
    } else if (charCode >= ZERO) {
        return charCode - ZERO
    } else {
        return charCode - FIFTY_TWO + 52
    }
}

/**
 * Provides flags that are stored in strings using a little-endian base 64
 * representation.
 *
 * This format is compact, easily serializable and allows for an unlimited
 * number of flags, but is specific to Multiflag. Use {@link CollectionFlagSet}
 * instead if you need the data to be easily understandable by other systems.
 */
export class Base64BitflagSet extends FlagSet<number, string> {
    protected override wrapValue(value: number): string {
        if (value < 1) {
            throw new RangeError(
                'Indices should be greater than or equal to 1.'
            )
        }
        const indexFromZero = value - 1
        const leadingBytes = ZERO_STRING.repeat(indexFromZero / 6)
        const bigEnd = encodeByte(1 << indexFromZero % 6)
        return leadingBytes + bigEnd
    }

    public override empty(): string {
        return ZERO_STRING
    }

    public override isEmpty(flags: string): boolean {
        let result = true
        for (let i = 0; i < flags.length && result; i++) {
            result = flags[i] == ZERO_STRING
        }
        return result
    }

    public override union(first: string, second: string): string {
        let result = ''

        let shorter, longer
        if (first.length < second.length) {
            shorter = first
            longer = second
        } else {
            shorter = second
            longer = first
        }

        let i = 0
        // OR the bytes one by one
        for (; i < shorter.length; i++) {
            const value = decodeByte(shorter[i]) | decodeByte(longer[i])
            result += encodeByte(value)
        }
        // if one string is longer than the other, append the remaining bytes (x | 0 = x)
        for (; i < longer.length; i++) {
            result += longer[i]
        }
        // make sure there is always one digit in the string
        // empty strings are considered equal to zero, but we always try to normalise the output
        if (i < 1) {
            result += ZERO_STRING
        }

        return result
    }

    public override intersection(first: string, second: string): string {
        let result = ''

        const shorterLength = Math.min(first.length, second.length)
        let i = 0
        // AND the bytes one by one
        for (; i < shorterLength; i++) {
            const value = decodeByte(first[i]) & decodeByte(second[i])
            result += encodeByte(value)
        }
        // if one string is longer than the other, don't add anything else (x & 0 = 0)
        // but make sure there is always one digit in the string
        // empty strings are considered equal to zero, but we always try to normalise the output
        if (i < 1) {
            result += ZERO_STRING
        }

        return result
    }

    public override difference(first: string, second: string): string {
        let result = ''

        const shorterLength = Math.min(first.length, second.length)
        let i = 0
        // AND the bytes one by one
        for (; i < shorterLength; i++) {
            const value = decodeByte(first[i]) & ~decodeByte(second[i])
            result += encodeByte(value)
        }
        // if the first string is longer than the other, append its remaining bytes (x & ~0 = x)
        // if the second string is longer, don't add anything (0 & ~y = 0)
        for (; i < first.length; i++) {
            result += first[i]
        }
        // make sure there is always one digit in the string
        // empty strings are considered equal to zero, but we always try to normalise the output
        if (i < 1) {
            result += ZERO_STRING
        }

        return result
    }

    public override isSupersetOf(first: string, second: string): boolean {
        let result = true

        const shorterLength = Math.min(first.length, second.length)
        let i = 0
        // AND the bytes one by one and check
        // if one is false we don't need to check further
        for (; i < shorterLength && result; i++) {
            const secondValue = decodeByte(second[i])
            result = (decodeByte(first[i]) & secondValue) == secondValue
        }
        // if there are more characters in the second string, they must all be zeros
        // (0 & x is only equal to x when x is also 0)
        for (; i < second.length && result; i++) {
            result = second[i] == ZERO_STRING
        }

        return result
    }

    public override iterate(flags: string): Iterable<number> {
        throw new Error('not implemented')
    }
}
