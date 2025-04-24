<h1 align='center'>
  Multiflag<br/>
  <a href="https://github.com/louisdevie/multiflag/actions/workflows/tests.yml" >
    <img src="https://github.com/louisdevie/multiflag/actions/workflows/tests.yml/badge.svg" alt="Tests badge"/></a>
  <a href="https://codecov.io/gh/louisdevie/multiflag" > 
    <img src="https://codecov.io/gh/louisdevie/multiflag/graph/badge.svg?token=jzEIGeLIEj" alt="Codecov badge"/></a>
</h1>

Multiflag is a tiny language-agnostic library that makes manipulating bitflags (or any other kind of flag system) easier,
especially if you have flags that depend on each other (for example, if you're managing permissions).

- [Multiflag .NET](dotnet/README.md)&emsp;[![Nuget Package](https://img.shields.io/nuget/v/Multiflag)](https://www.nuget.org/packages/Multiflag)
- [Multiflag JavaScript/TypeScript](node/README.md)&emsp;[![npm](https://img.shields.io/npm/v/multiflag)](https://https://www.npmjs.com/package/multiflag)


## What it does

The Multiflag API exposes two kind of classes : `FlagSet`s and `Flag`s.
A flag set is a group of unique `Flag`s of the same type that can have dependencies on each other. 
A flag represent a distinct value in the set, and can require other "parent" flags. A flag is considered present in a
value only when its parents are present too. When a flag is added, all of its parents are added with it, and when it is
removed, all of its child flags are removed with it.

Two kind of flags are supported out of the box : a bitflag implementation for unsigned integers and enums,
a hashset-based implementation and a bitflag implementation that works with base64 strings.
This covers most of the common needs, but you can subclass `FlagSet` to work with any custom type you want.

## Compatible flag types

|                             .NET                              | JS | .NET (v1)  |   JS (v1)    |
|:-------------------------------------------------------------:|:--:|:----------:|:------------:|
|  `U8BitflagSet` and <br/> `EnumBitflagSet` backed by `byte`   | ✗  |  `Flag8`   | `NumberFlag` |
| `U16BitflagSet` and <br/> `EnumBitflagSet` backed by `ushort` | ✗  |  `Flag16`  | `NumberFlag` |
|  `U32BitflagSet` and <br/> `EnumBitflagSet` backed by `uint`  | ✗  |  `Flag32`  | `NumberFlag` |
| `U64BitflagSet` and <br/> `EnumBitflagSet` backed by `ulong`  | ✗  |  `Flag64`  |      ✗       |
|                      `DynamicBitflagSet`                      | ✗  |     ✗      |      ✗       |
|                      `Base64BitflagSet`                       | ✗  |     ✗      |      ✗       |
|                        `HashedFlagSet`                        | ✗  | `FlagSet`  | `ArrayFlag`  |
|                               ✗                               | ✗  | `FlagEnum` | `NumberFlag` |

## Why it exists

This library was developed to provide the [Gallium+](https://github.com/galliumplus) server and clients with a reliable way to manage permissions.
The primary goal is to support C# and Typescipt, but support for other languages (I'm thinking Python, Go, Rust)
may come later.

## Licensing

Multiglag is available under the [MIT License](LICENSE). ⓒ 2023-2025 Louis DEVIE.
