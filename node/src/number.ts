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
    private _value: number
    private _current: number

    public constructor(value: number) {
        this._value = value
        this._current = 1
    }

    public [Symbol.iterator](): IterableIterator<number> {
        return this
    }

    public next(): IteratorResult<number, undefined> {
        if (this._value == 0) {
            return { done: true, value: undefined }
        }

        while ((this._value & 1) == 0) {
            this._value >>= 1
            this._current <<= 1
        }

        const result = this._current
        this._value >>= 1
        this._current <<= 1

        return { done: false, value: result }
    }
}
