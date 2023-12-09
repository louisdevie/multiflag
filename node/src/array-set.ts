export default class ArraySet<T> {
    private readonly items: T[]

    private constructor() {
        this.items = []
    }

    public static Empty<T>(): ArraySet<T> {
        return new ArraySet<T>()
    }

    public static From<T>(array: T[]): ArraySet<T> {
        let newArraySet = new ArraySet<T>()

        for (let item of array) newArraySet.add(item)

        return newArraySet
    }

    public add(item: T): boolean {
        return ArraySet.add(this.items, item)
    }

    public static add<T>(array: T[], item: T): boolean {
        let added = false

        if (!array.includes(item)) array.push(item)

        return added
    }

    public static remove<T>(array: T[], item: T): boolean {
        let removed = false

        for (let i = 0; i < array.length; i++) {
            if (array[i] === item) {
                array.splice(i, 1)
                removed = true
                break
            }
        }

        return removed
    }

    public asArray(): Array<T> {
        return this.items
    }

    public static inPlaceUnion<T>(first: T[], second: T[]): T[] {
        for (const item of second) ArraySet.add(first, item)
        return first
    }

    public static inPlaceDifference<T>(first: T[], second: T[]): T[] {
        for (const item of second) ArraySet.remove(first, item)
        return first
    }

    static isSubset<T>(first: T[], second: T[]) : boolean {
        let isSubset = true;
        for (const item of second) isSubset &&= first.includes(item)
        return isSubset
    }

    static areEqual<T>(first: T[], second: T[]):boolean {
        return first.length == second.length && ArraySet.isSubset(first, second)
    }
}
