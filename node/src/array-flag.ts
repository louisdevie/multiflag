import FlagValue from './flag-value'
import Flag from './flag'
import ArraySet from './array-set'

class ArrayValue<T> implements FlagValue<Array<T>> {
    nothing(): T[] {
        return []
    }

    union(first: T[], second: T[]): T[] {
        return ArraySet.inPlaceUnion(first, second)
    }

    subtraction(first: T[], second: T[]): T[] {
        return ArraySet.inPlaceDifference(first, second)
    }

    includes(first: T[], second: T[]): boolean {
        return ArraySet.isSubset(first, second)
    }

    equivalent(first: T[], second: T[]): boolean {
        return ArraySet.areEqual(first, second)
    }
}

export default class ArrayFlag<T> extends Flag<T[], ArrayValue<T>> {
    public static withValue<T>(
        value: T,
        ...parents: ArrayFlag<T>[]
    ): ArrayFlag<T> {
        return new ArrayFlag<T>([value], parents)
    }

    public static withoutValue<T>(...parents: ArrayFlag<T>[]): ArrayFlag<T> {
        return new ArrayFlag<T>(new ArrayValue<T>().nothing(), parents)
    }

    protected override newAdapter(): ArrayValue<T> {
        return new ArrayValue<T>()
    }
}
