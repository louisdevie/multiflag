import FlagValue from './flag-value'
import ArraySet from './array-set'

export default abstract class Flag<V, A extends FlagValue<V>> {
    private readonly value: V
    private readonly adapter: A
    private readonly parents: ArraySet<Flag<V, A>>
    private readonly children: ArraySet<Flag<V, A>>

    protected constructor(value: V, parents: Flag<V, A>[]) {
        this.value = value
        this.adapter = this.newAdapter()
        this.parents = ArraySet.From(parents)
        this.children = ArraySet.Empty()

        for (let parent of this.parents.asArray()) {
            parent.children.add(this)
        }
    }

    protected abstract newAdapter(): A

    public addTo(flags: V): V {
        let newValue = this.adapter.union(flags, this.value)
        for (let parent of this.parents.asArray())
            newValue = parent.addTo(newValue)
        return newValue
    }

    public removeFrom(flags: V): V {
        let newValue = this.adapter.subtraction(flags, this.value)
        for (let child of this.children.asArray())
            newValue = child.removeFrom(newValue)
        return newValue
    }

    public in(flags: V): boolean {
        return (
            this.adapter.includes(flags, this.value) &&
            this.parents.asArray().every((parent) => parent.in(flags))
        )
    }

    public get isNothing(): boolean {
        return (
            this.adapter.equivalent(this.value, this.adapter.nothing()) &&
            this.parents.asArray().every((parent) => parent.isNothing)
        )
    }

    public get isConcrete(): boolean {
        return !this.adapter.equivalent(this.value, this.adapter.nothing())
    }

    public get isAbstract(): boolean {
        return (
            this.adapter.equivalent(this.value, this.adapter.nothing()) &&
            this.parents.asArray().some((parent) => !parent.isNothing)
        )
    }

    public positiveEquals(other: Flag<V, A>): boolean {
        let addedThis = this.addTo(this.adapter.nothing())
        let addedOther = other.addTo(this.adapter.nothing())

        return this.adapter.equivalent(addedThis, addedOther)
    }

    public static arePositiveEqual<V, A extends FlagValue<V>>(
        first: Flag<V, A>,
        second: Flag<V, A>
    ): boolean {
        return first.positiveEquals(second)
    }

    private negativeUnionWith(value: V): V {
        value = this.adapter.union(value, this.value)
        for (let child of this.children.asArray()) {
            value = child.negativeUnionWith(value)
        }
        return value
    }

    public negativeEquals(other: Flag<V, A>): boolean {
        let removedThis = this.negativeUnionWith(this.adapter.nothing())
        let removedOther = other.negativeUnionWith(this.adapter.nothing())

        return this.adapter.equivalent(removedThis, removedOther)
    }

    public static areNegativeEqual<V, A extends FlagValue<V>>(
        first: Flag<V, A>,
        second: Flag<V, A>
    ): boolean {
        return first.negativeEquals(second)
    }

    public equals(other: Flag<V, A>): boolean {
        return this.positiveEquals(other) && this.negativeEquals(other)
    }

    public static areEqual<V, A extends FlagValue<V>>(
        first: Flag<V, A>,
        second: Flag<V, A>
    ): boolean {
        return first.equals(second)
    }
}
