import { Base64BitflagSet } from '@module'

test('Create from an index', () => {
    const flags = new Base64BitflagSet()

    const flag2 = flags.flag(2)
    expect(flag2.addTo('')).toEqual('C')

    expect(() => flags.flag(0)).toThrow(RangeError)
    expect(() => flags.flag(-2)).toThrow(RangeError)
})

test('Union of two base-64 strings', () => {
    const flags = new Base64BitflagSet()

    expect(flags.union('A', 'A')).toEqual('A')
    expect(flags.union('B', 'A')).toEqual('B')
    expect(flags.union('A', 'C')).toEqual('C')
    expect(flags.union('B', 'C')).toEqual('D')
    expect(flags.union('D', 'G')).toEqual('H')
})

test('Difference of two base-64 strings', () => {
    const flags = new Base64BitflagSet()

    expect(flags.difference('A', 'A')).toEqual('A')
    expect(flags.difference('B', 'A')).toEqual('B')
    expect(flags.difference('D', 'G')).toEqual('B')
    expect(flags.difference('G', 'D')).toEqual('E')
    expect(flags.difference('I', 'R')).toEqual('I')
})

test('Intersection of two base-64 strings', () => {
    const flags = new Base64BitflagSet()

    expect(flags.intersection('A', 'A')).toEqual('A')
    expect(flags.intersection('B', 'A')).toEqual('A')
    expect(flags.intersection('B', 'C')).toEqual('A')
    expect(flags.intersection('B', 'D')).toEqual('B')
    expect(flags.intersection('L', 'F')).toEqual('B')
    expect(flags.intersection('L', 'H')).toEqual('D')
})

test('Iterate over a base-64 string', () => {
    const flags = new Base64BitflagSet()

    expect([...flags.iterate('A')]).toEqual([])
    expect([...flags.iterate('B')]).toEqual([1])
    expect([...flags.iterate('C')]).toEqual([2])
    expect([...flags.iterate('D')]).toEqual([1, 2])
    expect([...flags.iterate('L')]).toEqual([1, 2, 4])
    expect([...flags.iterate('kB')]).toEqual([3, 6, 7])
})

test('Normalise to minimum', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2, flag1)
    const flag3 = flags.flag(3, flag1)
    const flag4 = flags.flag(4, flag3)

    expect(flags.minimum('A')).toEqual('A')
    expect(flags.minimum('B')).toEqual('B')
    expect(flags.minimum('C')).toEqual('A')
    expect(flags.minimum('D')).toEqual('D')
    expect(flags.minimum('L')).toEqual('D')
    expect(flags.minimum('N')).toEqual('N')
    expect(flags.minimum('R')).toEqual('B')
})

test('Normalise to maximum', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2, flag1)
    const flag3 = flags.flag(3, flag1)
    const flag4 = flags.flag(4, flag3)

    expect(flags.maximum('A')).toEqual('A')
    expect(flags.maximum('B')).toEqual('B')
    expect(flags.maximum('C')).toEqual('D')
    expect(flags.maximum('D')).toEqual('D')
    expect(flags.maximum('L')).toEqual('P')
    expect(flags.maximum('N')).toEqual('N')
    expect(flags.maximum('R')).toEqual('B')
})

test('Add to base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3)
    const flags2And3 = flags.flag(flag2, flag3)

    expect(flag2.addTo('B')).toEqual('D')
    expect(flag3.addTo('B')).toEqual('F')
    expect(flags2And3.addTo('B')).toEqual('H')
})

test('Remove from base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3, flag1)

    expect(flag1.removeFrom('H')).toEqual('C')
    expect(flag2.removeFrom('H')).toEqual('F')
    expect(flag3.removeFrom('H')).toEqual('D')
})

test('Is in base64 string', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flag3 = flags.flag(3, flag1)

    expect(flag1.isIn('B')).toBe(true)
    expect(flag2.isIn('D')).toBe(true)
    expect(flag3.isIn('E')).toBe(false)
    expect(flag3.isIn('F')).toBe(true)
})

test('Is abstract', () => {
    const flags = new Base64BitflagSet()
    const flag1 = flags.flag(1)
    const flag2 = flags.flag(2)
    const flags1And2 = flags.flag(flag1, flag2)
    const flag3 = flags.flag(3, flags1And2)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flags1And2.isAbstract).toBe(true)
    expect(flag3.isAbstract).toBe(false)
})
