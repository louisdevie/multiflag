/**
 * An adapter to use a type `T` as flag storage. Built-in implementations exists for `Number` and `Array`.
 */
export default interface FlagValue<T> {
    /**
     * Creates an empty set of flags.
     */
    nothing(): T

    /**
     * Compute the union of two sets of flags. For mutable types, the `first` set may be modified to perform the
     * operation in-place.
     * @param first A set of flags that may be modified by this method.
     * @param second A second set of flags that will not be modified by the method.
     */
    union(first: T, second: T): T

    /**
     * Compute the difference of two sets of flags. For mutable types, the `first` set may be modified to perform the
     * operation in-place.
     * @param first A set of flags that may be modified by this method.
     * @param second A second set of flags that will not be modified by the method.
     */
    subtraction(first: T, second: T): T

    /**
     * Check if the `first` set of flags is a superset of the `second` one.
     * @param first A first set of flags.
     * @param second A second set of flags.
     */
    includes(first: T, second: T): boolean

    /**
     * Check if the `first` and `second` set of flags contains the same elements.
     * @param first A first set of flags.
     * @param second A second set of flags.
     */
    equivalent(first: T, second: T): boolean
}
