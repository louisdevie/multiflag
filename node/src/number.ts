import { FlagSet } from './flagset'

export class NumberBitflagSet extends FlagSet<number> {
    public override empty(): number {
        throw new Error('Method not implemented.')
    }

    public override isEmpty(flags: number): boolean {
        throw new Error('Method not implemented.')
    }

    public override union(first: number, second: number): number {
        throw new Error('Method not implemented.')
    }

    public override difference(first: number, second: number): number {
        throw new Error('Method not implemented.')
    }

    public override isSupersetOf(first: number, second: number): boolean {
        throw new Error('Method not implemented.')
    }
}
