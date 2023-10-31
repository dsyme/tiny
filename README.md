# tiny666
Tiny666 is a project that explores the power and elegance of functional programming in F#.

The goal of this project is to demonstrate how to use advanced features of F#, such as higher-kinded types, type classes, and generic programming, to create expressive and concise abstractions.

The main feature of this project is the implementation of the "Supra mega ultra higher-ist kinded types" (SMUHK), which are types that can have any number of type parameters, even infinite ones. SMUHKs allow us to define and manipulate complex structures, such as four-dimensional hypercubes, with ease and elegance.

The project consists of the following files:

* `src/Tesseract.fs`: Defines a type `Tesseract<'a>` that represents a four-dimensional hypercube of values of type `'a`. A tesseract is a cube of cubes of cubes of cubes. It can be seen as a generalization of the two-dimensional square and the three-dimensional cube to higher dimensions. A tesseract can be constructed from a list of eight cubes, or from a function that maps four coordinates to a value.
* `src/SMUHK.fs`: Defines a type class `SMUHK<'a>` that represents the SMUHKs and provides some operations on them, such as mapping, folding, and zipping. A type class is a way of defining a set of types that share some common behavior, and providing generic functions that work on any type that belongs to the class. A SMUHK is a type that can have any number of type parameters, even infinite ones. For example, a tesseract is a SMUHK with four type parameters, one for each dimension. A SMUHK can also be seen as a type constructor that takes a type and returns another type. For example, the type constructor `Tesseract` takes a type `'a` and returns the type `Tesseract<'a>`.
* `src/Main.fs`: Contains the main program that uses the `Tesseract` and `SMUHK` types to demonstrate their power and elegance. The program creates some tesseracts with different values and types, and performs some operations on them using the SMUHK type class. The program also prints the results to the console in a human-readable format.
* `test/TesseractTest.fs`: Tests the functionality and correctness of the `Tesseract` type. The tests use the FsUnit framework to assert the expected properties and behaviors of the tesseract type and its constructors and methods.
* `test/SMUHKTest.fs`: Tests the functionality and correctness of the `SMUHK` type class and its instances. The tests use the FsUnit framework to assert the expected properties and behaviors of the SMUHK type class and its generic functions, as well as the instances of the type class for some concrete types, such as `Tesseract`, `List`, and `Option`.
