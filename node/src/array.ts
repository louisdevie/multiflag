import { FlagSet } from './flagset'
import { InvalidCollectionValueError } from './errors'
import { Flag } from './flag'

export class ArrayFlagSet<T> extends FlagSet<T, T[]> {
    protected override wrapValue(value: T): T[] {
        return [value]
    }

    public override empty(): T[] {
        return []
    }

    public override union(first: T[], second: T[]): T[] {
        const unionArray: T[] = []
        for (const item of first) {
            if (!unionArray.includes(item)) {
                unionArray.push(item)
            }
        }
        for (const item of second) {
            if (!unionArray.includes(item)) {
                unionArray.push(item)
            }
        }
        return unionArray
    }

    public override intersection(first: T[], second: T[]): T[] {
        const intersectionArray: T[] = []
        for (const item of first) {
            if (!intersectionArray.includes(item) && second.includes(item)) {
                intersectionArray.push(item)
            }
        }
        return intersectionArray
    }

    public override difference(first: T[], second: T[]): T[] {
        const differenceArray: T[] = []
        for (const item of first) {
            if (!differenceArray.includes(item) && !second.includes(item)) {
                differenceArray.push(item)
            }
        }
        return differenceArray
    }

    public override isSupersetOf(first: T[], second: T[]): boolean {
        for (const item of second) {
            if (!first.includes(item)) {
                return false
            }
        }
        return true
    }

    public override iterate(flags: T[]): Iterable<T> {
        return flags
    }
}
