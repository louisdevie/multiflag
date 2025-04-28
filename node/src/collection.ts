import { FlagSet } from './flagset'
import { InvalidCollectionValueError } from './errors'
import { Flag } from './flag'

function polyfillUnion(
    proto: object | undefined
): <T>(a: Set<T>, b: Set<T>) => Set<T> {
    if (proto && 'union' in proto) {
        return function (a, b) {
            return (proto.union as Function).call(a, b)
        }
    } else {
        return function <T>(a: Set<T>, b: Set<T>) {
            if (!(a instanceof Set) || !(b instanceof Set)) {
                throw new TypeError('Arguments must be instances of Set')
            }

            const unionSet = new Set(a)
            for (const item of b) {
                unionSet.add(item)
            }

            return unionSet
        }
    }
}

function polyfillIntersection(
    proto: object | undefined
): <T>(a: Set<T>, b: Set<T>) => Set<T> {
    if (proto && 'intersection' in proto) {
        return function (a, b) {
            return (proto.intersection as Function).call(a, b)
        }
    } else {
        return function <T>(a: Set<T>, b: Set<T>) {
            if (!(a instanceof Set) || !(b instanceof Set)) {
                throw new TypeError('Arguments must be instances of Set')
            }

            const differenceSet = new Set<T>()
            for (const item of a) {
                if (b.has(item)) {
                    differenceSet.add(item)
                }
            }

            return differenceSet
        }
    }
}

function polyfillDifference(
    proto: object | undefined
): <T>(a: Set<T>, b: Set<T>) => Set<T> {
    if (proto && 'difference' in proto) {
        return function (a, b) {
            return (proto.difference as Function).call(a, b)
        }
    } else {
        return function <T>(a: Set<T>, b: Set<T>) {
            if (!(a instanceof Set) || !(b instanceof Set)) {
                throw new TypeError('Arguments must be instances of Set')
            }

            const differenceSet = new Set<T>()
            for (const item of a) {
                if (!b.has(item)) {
                    differenceSet.add(item)
                }
            }

            return differenceSet
        }
    }
}

function polyfillIsSupersetOf(
    proto: object | undefined
): <T>(a: Set<T>, b: Set<T>) => boolean {
    if (proto && 'isSupersetOf' in proto) {
        return function (a, b) {
            return (proto.isSupersetOf as Function).call(a, b)
        }
    } else {
        return function <T>(a: Set<T>, b: Set<T>) {
            if (!(a instanceof Set) || !(b instanceof Set)) {
                throw new TypeError('Arguments must be instances of Set')
            }

            for (const item of b) {
                if (!a.has(item)) {
                    return false
                }
            }

            return true
        }
    }
}

export class CollectionFlagSet<T> extends FlagSet<T, Set<T>> {
    /**
     * Creates a new empty flag set.
     *
     * @remarks Calling this constructor in an environment that does not
     * natively support {@link Set} will throw an error.
     */
    public constructor() {
        super()
        if (typeof Set !== 'function') {
            throw new Error('This environment does not seem to support Sets')
        }
    }

    protected override wrapValue(value: T): Set<T> {
        return new Set([value])
    }

    public override empty(): Set<T> {
        return new Set()
    }

    public override union = polyfillUnion(Set.prototype)

    public override intersection = polyfillIntersection(Set.prototype)

    public override difference = polyfillDifference(Set.prototype)

    public override isSupersetOf = polyfillIsSupersetOf(Set.prototype)

    public override iterate(flags: Set<T>): Iterable<T> {
        return flags
    }
}
