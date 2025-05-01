import { ReusedFlagValueError } from './errors'
import { Flag, ValueFlag } from './flag'

function assertIsFlag<T>(arg: unknown): Flag<T> {
    if (arg instanceof Flag) {
        return arg
    } else {
        throw new TypeError(
            'Only the first argument to flag() may be a flag value, ' +
                'other arguments must be instances of Flag.'
        )
    }
}

/**
 * Represent a group of flags, and provide methods to use T as a set. Built-in
 * implementations exist for `number`, `bigint`, base-64 `string`, `Set`
 * and `Array`.
 *
 * @typeParam V - The type of values in the set.
 * @typeParam S - The type to be used as a set of flags.
 */
export abstract class FlagSet<V, S> {
    private readonly _valueFlags: Map<V, ValueFlag<S>>

    /**
     * Creates a new empty flag set.
     */
    public constructor() {
        this._valueFlags = new Map()
    }

    /**
     * Creates a flag without a value.
     *
     * @param parents - Other flags required for this flag to be set.
     *
     * @returns A flag bound to this set.
     *
     * @throw {@link ForeignFlagError} if one of the parents doesn't belong to
     * the same {@link FlagSet}.
     */
    public flag(...parents: Flag<S>[]): Flag<S>
    /**
     * Creates a flag with a value.
     *
     * @param value - The value of the flag.
     * @param parents - Other flags required for this flag to be set.
     *
     * @returns A flag bound to this set.
     *
     * @throw {@link ForeignFlagError} if one of the parents doesn't belong to
     * the same {@link FlagSet}.
     *
     * @throw {@link ReusedFlagValueError} if another flag has already been
     * created with the same value.
     */
    public flag(value: V, ...parents: Flag<S>[]): Flag<S>
    public flag(...args: (V | Flag<S>)[]): Flag<S> {
        if (args.length > 0 && !(args[0] instanceof Flag)) {
            // create a flag with a value
            const value = args.shift() as V
            if (this._valueFlags.has(value)) {
                throw new ReusedFlagValueError(value)
            }

            const parents = new Set(args.map(assertIsFlag<S>))
            const flag = new ValueFlag(this, parents, this.wrapValue(value))

            this._valueFlags.set(value, flag)
            return flag
        } else {
            // create a flag without a value
            const parents = new Set(args.map(assertIsFlag<S>))
            if (parents.size < 2) {
                throw new TypeError(
                    'A flag without value must have at least two parents.'
                )
            }

            return new Flag(this, parents)
        }
    }

    /**
     * Transforms a value into a set containing only that value.
     * This method may throw an exception if the value is not valid.
     *
     * @param value - The value that will be used for the flag.
     */
    protected abstract wrapValue(value: V): S

    /**
     * Filters a flag set so that it only contains the flags that were declared
     * with the {@link flag} method. If a flags is missing some of its parents,
     * it will not be included in the result.
     *
     * @param flags The set of flags to filter.
     *
     * @returns A new set of flags.
     *
     * @see maximum
     */
    public minimum(flags: S): S {
        let result = this.empty()
        for (const value of this.iterate(flags)) {
            const flag = this._valueFlags.get(value)
            if (flag !== undefined && flag.isIn(flags)) {
                result = flag.addTo(result)
            }
        }
        return result
    }

    /**
     * Creates a copy of a flag set that will contain all the flags that were
     * declared with the {@link flag} method. If a flags is missing some of its
     * parents in the original set, they will be added to the result.
     *
     * @param flags The set of flags to filter.
     *
     * @returns A new set of flags.
     *
     * @see minimum
     */
    public maximum(flags: S): S {
        let result = this.empty()
        for (const value of this.iterate(flags)) {
            const flag = this._valueFlags.get(value)
            if (flag !== undefined) {
                result = flag.addTo(result)
            }
        }
        return result
    }

    /**
     * Creates an empty set of flags.
     */
    public abstract empty(): S

    /**
     * Computes the union of two sets of flags.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags.
     *
     * @returns A new set that contains the flags of both sets.
     */
    public abstract union(first: S, second: S): S

    /**
     * Computes the intersection of two set of flags.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags.
     *
     * @returns A new set that contains the flags that appear both in the first
     * set and the second set.
     */
    public abstract intersection(first: S, second: S): S

    /**
     * Computes the difference of two set of flags.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags (that will be subtracted from the
     * first).
     *
     * @returns A new set that contains the flags of the first set that do not
     * appear in the second.
     */
    public abstract difference(first: S, second: S): S

    /**
     * Checks whether the first set of flags is a superset of the second.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags.
     */
    public abstract isSupersetOf(first: S, second: S): boolean

    /**
     * Returns an iterable over the elements of a set.
     *
     * @param flags - A set of flags.
     */
    public abstract iterate(flags: S): Iterable<V>
}
