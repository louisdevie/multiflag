import { FlagSet } from './flagset'
import { InvalidBitflagValueError } from './errors'

interface BigIntImpl {
    readonly AVAILABLE: boolean
    readonly ZERO: bigint
    readonly ONE: bigint
}

const __BI: BigIntImpl = (function () {
    if (typeof BigInt === 'function') {
        return { AVAILABLE: true, ZERO: BigInt(0), ONE: BigInt(1) }
    } else {
        return { AVAILABLE: false } as BigIntImpl
    }
})()

export class DynamicBitflagSet extends FlagSet<bigint, bigint> {
    /**
     * Creates a new empty flag set.
     *
     * @remarks Calling this constructor in an environment that does not
     * natively support {@link BigInt} will throw an error.
     */
    public constructor() {
        super()
        if (!__BI.AVAILABLE) {
            throw new Error('This environment does not seem to support BigInts')
        }
    }

    protected override wrapValue(value: bigint): bigint {
        if (value == __BI.ZERO || (value & (value - __BI.ONE)) != __BI.ZERO) {
            throw new InvalidBitflagValueError()
        }
        return value
    }

    public override empty(): bigint {
        return __BI.ZERO
    }

    public override isEmpty(flags: bigint): boolean {
        return flags == __BI.ZERO
    }

    public override union(first: bigint, second: bigint): bigint {
        return first | second
    }

    public override intersection(first: bigint, second: bigint): bigint {
        return first & second
    }

    public override difference(first: bigint, second: bigint): bigint {
        return first & ~second
    }

    public override isSupersetOf(first: bigint, second: bigint): boolean {
        return (first & second) == second
    }

    public override iterate(flags: bigint): Iterable<bigint> {
        throw new Error('not implemented')
    }
}
