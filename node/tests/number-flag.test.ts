import { NumberFlag } from '../src'

test('Addition of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2)
    let flag4 = NumberFlag.withValue(4, flag2)

    let flags = 0
    expect(flag1.addTo(flags)).toBe(0)
    expect(flag2.addTo(flags)).toBe(1)
    expect(flag3.addTo(flags)).toBe(2)
    expect(flag4.addTo(flags)).toBe(5)

    flags = 2
    expect(flag1.addTo(flags)).toBe(2)
    expect(flag2.addTo(flags)).toBe(3)
    expect(flag3.addTo(flags)).toBe(2)
    expect(flag4.addTo(flags)).toBe(7)

    flags = 255
    expect(flag1.addTo(flags)).toBe(255)
    expect(flag2.addTo(flags)).toBe(255)
    expect(flag3.addTo(flags)).toBe(255)
    expect(flag4.addTo(flags)).toBe(255)
})

test('Subtraction of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2)
    let flag4 = NumberFlag.withValue(4, flag2)

    let flags = 0
    expect(flag1.removeFrom(flags)).toBe(0)
    expect(flag2.removeFrom(flags)).toBe(0)
    expect(flag3.removeFrom(flags)).toBe(0)
    expect(flag4.removeFrom(flags)).toBe(0)

    flags = 3
    expect(flag1.removeFrom(flags)).toBe(3)
    expect(flag2.removeFrom(flags)).toBe(2)
    expect(flag3.removeFrom(flags)).toBe(1)
    expect(flag4.removeFrom(flags)).toBe(3)

    flags = 255
    expect(flag1.removeFrom(flags)).toBe(255)
    expect(flag2.removeFrom(flags)).toBe(250)
    expect(flag3.removeFrom(flags)).toBe(253)
    expect(flag4.removeFrom(flags)).toBe(251)
})

test('Inclusion of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2)
    let flag4 = NumberFlag.withValue(4, flag2)

    let flags = 0
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(false)
    expect(flag3.in(flags)).toBe(false)
    expect(flag4.in(flags)).toBe(false)

    flags = 6
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(false)
    expect(flag3.in(flags)).toBe(true)
    expect(flag4.in(flags)).toBe(false)

    flags = 7
    expect(flag1.in(flags)).toBe(true)
    expect(flag2.in(flags)).toBe(true)
    expect(flag3.in(flags)).toBe(true)
    expect(flag4.in(flags)).toBe(true)
})

test('Properties of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2, flag1)
    let flag4 = NumberFlag.withoutValue(flag1)
    let flag5 = NumberFlag.withValue(4, flag2)
    let flag6 = NumberFlag.withoutValue(flag2)
    let flag7 = NumberFlag.withoutValue(flag4, flag6)

    expect(flag1.isNothing).toBe(true)
    expect(flag2.isNothing).toBe(false)
    expect(flag3.isNothing).toBe(false)
    expect(flag4.isNothing).toBe(true)
    expect(flag5.isNothing).toBe(false)
    expect(flag6.isNothing).toBe(false)
    expect(flag7.isNothing).toBe(false)

    expect(flag1.isConcrete).toBe(false)
    expect(flag2.isConcrete).toBe(true)
    expect(flag3.isConcrete).toBe(true)
    expect(flag4.isConcrete).toBe(false)
    expect(flag5.isConcrete).toBe(true)
    expect(flag6.isConcrete).toBe(false)
    expect(flag7.isConcrete).toBe(false)

    expect(flag1.isAbstract).toBe(false)
    expect(flag2.isAbstract).toBe(false)
    expect(flag3.isAbstract).toBe(false)
    expect(flag4.isAbstract).toBe(false)
    expect(flag5.isAbstract).toBe(false)
    expect(flag6.isAbstract).toBe(true)
    expect(flag7.isAbstract).toBe(true)
})

test('Positive equality of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2, flag2)
    let flag4 = NumberFlag.withoutValue(flag2)

    expect(flag1.positiveEquals(flag1)).toBe(true)
    expect(flag1.positiveEquals(flag2)).toBe(false)
    expect(flag1.positiveEquals(flag3)).toBe(false)
    expect(flag1.positiveEquals(flag4)).toBe(false)

    expect(flag2.positiveEquals(flag1)).toBe(false)
    expect(flag2.positiveEquals(flag2)).toBe(true)
    expect(flag2.positiveEquals(flag3)).toBe(false)
    expect(flag2.positiveEquals(flag4)).toBe(true)

    expect(flag3.positiveEquals(flag1)).toBe(false)
    expect(flag3.positiveEquals(flag2)).toBe(false)
    expect(flag3.positiveEquals(flag3)).toBe(true)
    expect(flag3.positiveEquals(flag4)).toBe(false)

    expect(flag4.positiveEquals(flag1)).toBe(false)
    expect(flag4.positiveEquals(flag2)).toBe(true)
    expect(flag4.positiveEquals(flag3)).toBe(false)
    expect(flag4.positiveEquals(flag4)).toBe(true)
})

test('Negative equality of NumberFlags', () => {
    let flag1 = NumberFlag.withoutValue()
    let flag2 = NumberFlag.withValue(1)
    let flag3 = NumberFlag.withValue(2, flag2)
    let flag4 = NumberFlag.withoutValue(flag2)

    expect(flag1.negativeEquals(flag1)).toBe(true)
    expect(flag1.negativeEquals(flag2)).toBe(false)
    expect(flag1.negativeEquals(flag3)).toBe(false)
    expect(flag1.negativeEquals(flag4)).toBe(true)

    expect(flag2.negativeEquals(flag1)).toBe(false)
    expect(flag2.negativeEquals(flag2)).toBe(true)
    expect(flag2.negativeEquals(flag3)).toBe(false)
    expect(flag2.negativeEquals(flag4)).toBe(false)

    expect(flag3.negativeEquals(flag1)).toBe(false)
    expect(flag3.negativeEquals(flag2)).toBe(false)
    expect(flag3.negativeEquals(flag3)).toBe(true)
    expect(flag3.negativeEquals(flag4)).toBe(false)

    expect(flag4.negativeEquals(flag1)).toBe(true)
    expect(flag4.negativeEquals(flag2)).toBe(false)
    expect(flag4.negativeEquals(flag3)).toBe(false)
    expect(flag4.negativeEquals(flag4)).toBe(true)
})
