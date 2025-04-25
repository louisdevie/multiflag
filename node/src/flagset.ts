import { Flag } from './flag'

/**
 * Represent a group of flags, and provide methods to use T as a set. Built-in
 * implementations exist for `number`, `BigInt`, `Set` and `Array`.
 *
 * @typeParam T - The type to be used as a set of flags.
 */
export abstract class FlagSet<T> {
    private readonly concreteFlags: Map<T, Flag<T>>

    /**
     * Creates a new empty flag set.
     */
    public constructor() {
        this.concreteFlags = new Map()
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
    public flag(...parents: Flag<T>[]): Flag<T>

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
    public flag(value: T, ...parents: Flag<T>[]): Flag<T>
    public flag(): Flag<T> {
        throw new Error('not implemented')
        /*{
            return new Flag<T>(this, this.Empty(), parents);
        }
            this.CheckValue(value);

            if (this.concreteFlags.ContainsKey(value))
            {
                throw new ReusedFlagValueException(value);
            }

            var flag = new Flag<T>(this, value, parents);
            this.concreteFlags.Add(value, flag);
            return flag;*/
    }

    /**
     * This method will be called when a new flag is about to be created with
     * that value. The default implementation does nothing, but it may be
     * overridden to throw an exception on invalid values.
     *
     * @param value - The value that will be used for the flag.
     */
    protected checkValue(value: T): void {}

    /**
     * Creates an empty set of flags.
     */
    public abstract empty(): T

    /**
     * Checks if a set of flags is the empty set.
     *
     * @param flags - The set of flags to test.
     */
    public abstract isEmpty(flags: T): boolean

    /**
     * Computes the union of two sets of flags.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags.
     *
     * @returns A new set that contains the flags of both sets.
     */
    public abstract union(first: T, second: T): T

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
    public abstract difference(first: T, second: T): T

    /**
     * Checks whether the first set of flags is a superset of the second.
     *
     * @param first - The first set of flags.
     * @param second - The second set of flags.
     */
    public abstract isSupersetOf(first: T, second: T): boolean
}
