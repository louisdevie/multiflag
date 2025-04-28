import { FlagSet } from './flagset'
import { InvalidBitflagValueError } from './errors'

export class NumberBitflagSet extends FlagSet<number, number> {
    protected override wrapValue(value: number): number {
        if (value == 0 || (value & (value - 1)) != 0) {
            throw new InvalidBitflagValueError()
        }
        return value
    }

    public override empty(): number {
        return 0
    }

    public override isEmpty(flags: number): boolean {
        return flags === 0
    }

    public override union(first: number, second: number): number {
        return first | second
    }

    public override intersection(first: number, second: number): number {
        return first & second
    }

    public override difference(first: number, second: number): number {
        return first & ~second
    }

    public override isSupersetOf(first: number, second: number): boolean {
        return (first & second) == second
    }

    public override iterate(flags: number): Iterable<number> {
        return new NumberBitflagIterator(flags)
    }
}

class NumberBitflagIterator implements IterableIterator<number> {
    private value: number
    private current: number

    public constructor(value: number) {
        this.value = value
        this.current = 1
    }

    public [Symbol.iterator](): IterableIterator<number> {
        return this
    }

    public next(): IteratorResult<number, undefined> {
        if (this.value == 0) {
            return { done: true, value: undefined }
        }

        while ((this.value & 1) == 0) {
            this.value >>= 1
            this.current <<= 1
        }

        const result = this.current
        this.value >>= 1
        this.current <<= 1

        return { done: false, value: result }
    }
}
