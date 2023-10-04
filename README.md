<h1 align='center'>
  Multiflag<br/>
  <a href="https://codecov.io/gh/louisdevie/multiflag" > 
    <img src="https://codecov.io/gh/louisdevie/multiflag/graph/badge.svg?token=jzEIGeLIEj"/></a></h1>

Multiflag is a tiny language-agnostic library that makes manipulating bitflags (or any other kind of flag system) easier,
especially if you have flags that depend on each other (for example, if you're managing permissions and
a permission may require other permissions to be allowed).

- [Multiflag.NET](dotnet/README.md)&emsp;[![Nuget Package](https://img.shields.io/nuget/v/Multiflag)](https://www.nuget.org/packages/Multiflag)
- *Multiflag Typescript planned*

## How it works

Multiflag work with two types : flags and flag sets.
A flag set can be any value that can represent a group of boolean properties (integers as bitflags, a list of the enabled flags, ...).
A flag is one of these properties, and can be dependant on other flags. When a flag is added, all of its parent are too. All its parents are also required
for the flag to be considered included in the set. And when it is removed, all of its child flags are removed with it.
There is actually a third type coming into play: the "value adapter". Its job is to bridge the gap between a flag and the value it is based on
by providing the relevant operation on said type.

Two kind of flags are supported out of the box : a bitflag implementation for integers and enums, and a list-based (or rather, set-based) implementation, which covers most of the common needs.
But Multiflag is extensible, you can implement an adapter for any custom type of flag representation you want.

## Why ?

This library was developed to provide the [Gallium+](https://github.com/galliumplus) server and clients with a reliable way to manage permissions. The primary goal
is thus to support C# and Typescipt, but support for other languages (I'm thinking Python, Go, Rust) may come later.

## Licensing

Multiglag is available under the [MIT License](LICENSE). ⓒ 2023 Louis DEVIE.
