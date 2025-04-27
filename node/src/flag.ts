import { FlagSet } from './flagset'
import { ForeignFlagError } from './errors'

/**
 * A `Flag` represents some element that can be added or removed from a set of
 * type `T`. When a flag is added to the set, all of its parents are added along
 * with it, and when it is removed all of its children are removed too.
 *
 * @typeParam T - The type of value the flag can be stored in
 */
export class Flag<T> {
    private readonly children: Flag<T>[]
    private readonly parents: Flag<T>[]
    private readonly set: FlagSet<unknown, T>
    private readonly value: T

    /**
     * Creates a new flag.
     * @param set - The set this flag belongs to.</param>
     * @param value - The value of the flag.</param>
     * @param parents - The parent flags.</param>
     *
     * @throws {@link ForeignFlagError} if one of the parents doesn't belong
     * to the same {@link FlagSet}.
     *
     * @internal
     */
    public constructor(set: FlagSet<unknown, T>, value: T, parents: Flag<T>[]) {
        this.set = set
        this.value = value
        this.parents = parents
        this.children = []

        for (const parent of this.parents) {
            if (!parent.belongsTo(this.set)) {
                throw new ForeignFlagError()
            }

            parent.children.push(this)
        }
    }

    /**
     * `true` when this flag has no value on its own.
     */
    public get isAbstract(): boolean {
        return this.set.isEmpty(this.value)
    }

    private belongsTo(flagSet: FlagSet<unknown, T>): boolean {
        return this.set == flagSet
    }

    /**
     * Add a flag if it is not already present.
     *
     * @param flags - The flags to add it to.
     *
     * @returns A copy of the flags with this flag added.
     */
    public addTo(flags: T): T {
        return this.parents.reduce(
            (current, parent) => parent.addTo(current),
            this.set.union(flags, this.value)
        )
    }

    /**
     * Removes a flag if it is present.
     *
     * @param flags - The flags to remove it from.
     *
     * @returns A copy of the flags with this flag removed.
     */
    public removeFrom(flags: T): T {
        return this.children.reduce(
            (current, child) => child.removeFrom(current),
            this.set.difference(flags, this.value)
        )
    }

    /**
     * Check whether this flag is in `flags`.
     *
     * @param flags - The flags to search in.
     *
     * @returns `true` if this flag belongs to the set of `flags`,
     * otherwise `false`.
     */
    public isIn(flags: T): boolean {
        return (
            this.set.isSupersetOf(flags, this.value) &&
            this.parents.every((parent) => parent.isIn(flags))
        )
    }
}
