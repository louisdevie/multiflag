/**
 * Error thrown when a flag is associated with another one that was created from
 * a different {@link FlagSet}.
 */
export class ForeignFlagError extends Error {
    /** @internal */
    public constructor() {
        super(
            'Cannot create a dependency between two flags created in different sets.'
        )
    }
}

/**
 * Error thrown by `FlagSet`s that store the flags using a binary format when a
 * flag value isn't a power of two.
 */
export class InvalidBitflagValueError extends Error {
    /** @internal */
    public constructor() {
        super('Flag values for bit flags must be powers of two.')
    }
}

/**
 * Error thrown by `FlagSet`s that store the flags using collections when a flag
 * value does not contain exactly one element.
 */
export class InvalidCollectionValueError extends Error {
    /** @internal */
    public constructor() {
        super('Flag values for collections must contain exactly one value.')
    }
}

/**
 * Error thrown if the {@link FlagSet.flag} method is called with a value that
 * was already used for another flag in the same `FlagSet`.
 */
export class ReusedFlagValueError extends Error {
    /** @internal */
    public constructor(value: any) {
        super(`The flag value ${value} is already being used for another flag.`)
    }
}
