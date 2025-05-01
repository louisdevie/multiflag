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
    private readonly _children: Set<Flag<T>>
    private readonly _parents: Set<Flag<T>>
    private readonly _set: FlagSet<unknown, T>

    /**
     * Creates a new flag.
     * @param set - The set this flag belongs to.</param>
     * @param parents - The parent flags.</param>
     *
     * @throws {@link ForeignFlagError} if one of the parents doesn't belong
     * to the same {@link FlagSet}.
     *
     * @internal
     */
    public constructor(set: FlagSet<unknown, T>, parents: Set<Flag<T>>) {
        this._set = set
        this._parents = parents
        this._children = new Set()

        for (const parent of this._parents) {
            if (!parent.belongsTo(this._set)) {
                throw new ForeignFlagError()
            }

            parent._children.add(this)
        }
    }

    private belongsTo(flagSet: FlagSet<unknown, T>): boolean {
        return this._set == flagSet
    }

    protected get set(): FlagSet<unknown, T> {
        return this._set
    }

    /**
     * `true` when this flag has no value on its own.
     */
    public readonly isAbstract: boolean = true

    /**
     * Add a flag if it is not already present.
     *
     * @param flags - The flags to add it to.
     *
     * @returns A copy of the flags with this flag added.
     */
    public addTo(flags: T): T {
        for (const parent of this._parents) {
            flags = parent.addTo(flags)
        }
        return flags
    }

    /**
     * Removes a flag if it is present.
     *
     * @param flags - The flags to remove it from.
     *
     * @returns A copy of the flags with this flag removed.
     */
    public removeFrom(flags: T): T {
        for (const child of this._children) {
            flags = child.removeFrom(flags)
        }
        return flags
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
        for (const parent of this._parents) {
            if (!parent.isIn(flags)) {
                return false
            }
        }
        return true
    }
}

/** @intenal */
export class ValueFlag<T> extends Flag<T> {
    private readonly _value: T

    /**
     * Creates a new flag with a value.
     * @param set - The set this flag belongs to.</param>
     * @param value - The value of the flag.</param>
     * @param parents - The parent flags.</param>
     *
     * @throws {@link ForeignFlagError} if one of the parents doesn't belong
     * to the same {@link FlagSet}.
     *
     * @internal
     */
    public constructor(
        set: FlagSet<unknown, T>,
        parents: Set<Flag<T>>,
        value: T
    ) {
        super(set, parents)
        this._value = value
    }

    public override readonly isAbstract = false

    public override addTo(flags: T): T {
        return super.addTo(this.set.union(flags, this._value))
    }

    public override removeFrom(flags: T): T {
        return super.removeFrom(this.set.difference(flags, this._value))
    }

    public override isIn(flags: T): boolean {
        return this.set.isSupersetOf(flags, this._value) && super.isIn(flags)
    }
}
