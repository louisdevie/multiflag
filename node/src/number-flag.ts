import Flag from './flag'
import FlagValue from './flag-value'

/**
 * A built-in implementation of {@link FlagValue} for the `number` type.
 * @internal
 */
class NumberValue implements FlagValue<number> {
    public static readonly NOTHING = 0

    nothing(): number {
        return NumberValue.NOTHING
    }

    union(first: number, second: number): number {
        return first | second
    }

    subtraction(first: number, second: number): number {
        return first & ~second
    }

    includes(first: number, second: number): boolean {
        return (first & second) === second
    }

    equivalent(first: number, second: number): boolean {
        return first === second
    }
}

export default class NumberFlag extends Flag<number, NumberValue> {
    public static withValue(
        value: number,
        ...parents: NumberFlag[]
    ): NumberFlag {
        return new NumberFlag(value, parents)
    }

    public static withoutValue(...parents: NumberFlag[]): NumberFlag {
        return new NumberFlag(NumberValue.NOTHING, parents)
    }

    protected newAdapter(): NumberValue {
        return new NumberValue()
    }
}
