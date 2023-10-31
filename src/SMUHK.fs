// SMUHK.fs
// Defines a type class SMUHK<'a> that represents the Supra mega ultra higher-ist kinded types and provides some operations on them

module SMUHK

open System

// A type class is a way of defining a set of types that share some common behavior, and providing generic functions that work on any type that belongs to the class
// A SMUHK is a type that can have any number of type parameters, even infinite ones
// A SMUHK can also be seen as a type constructor that takes a type and returns another type
// For example, the type constructor Tesseract takes a type 'a and returns the type Tesseract<'a>
type SMUHK<'a> =
    abstract member Map : ('a -> 'b) -> 'a -> 'b
    abstract member Fold : ('a -> 'b -> 'b) -> 'b -> 'a -> 'b
    abstract member Zip : ('a -> 'b -> 'c) -> 'a -> 'b -> 'c

// A helper function to create a SMUHK instance from its methods
let smuhk map fold zip =
    { new SMUHK<'a> with
        member __.Map f x = map f x
        member __.Fold f init x = fold f init x
        member __.Zip f x y = zip f x y }

// A SMUHK instance for the Tesseract type
let tesseractSMUHK =
    smuhk Tesseract.map Tesseract.fold Tesseract.zip

// A SMUHK instance for the List type
let listSMUHK =
    smuhk List.map List.fold List.zip

// A SMUHK instance for the Option type
let optionSMUHK =
    smuhk Option.map Option.fold Option.map2

// A generic function to map a function over a SMUHK value
let map f x (smuhk : SMUHK<_>) = smuhk.Map f x

// A generic function to fold a function over a SMUHK value
let fold f init x (smuhk : SMUHK<_>) = smuhk.Fold f init x

// A generic function to zip two SMUHK values with a function
let zip f x y (smuhk : SMUHK<_>) = smuhk.Zip f x y
