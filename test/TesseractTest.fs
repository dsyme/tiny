// TesseractTest.fs
// Tests the functionality and correctness of the Tesseract type

module TesseractTest

open System
open NUnit.Framework
open FsUnit
open Tesseract

// A helper function to create a list of lists of lists of lists of n by m by l by k elements using a function
let create4 n m l k f = create n (fun i -> create3 m l k (fun j k l -> f i j k l))

// A helper function to create a tesseract of a given shape with values from 0 to n * m * l * k - 1
let iota (n, m, l, k) = ofFunc (fun i j k l -> i * m * l * k + j * l * k + k * l + l)

// A helper function to create a tesseract of a given shape with a constant value
let const (n, m, l, k) x = ofFunc (fun _ _ _ _ -> x)

// A helper function to create a tesseract from a list of eight cubes
// The cubes must have the same shape of 2 by 2 by 2
// The cubes are arranged in the following order:
// 0 1
// 2 3
// 4 5
// 6 7
let ofCubes cubes =
    if hasShape4 (2, 2, 2, 2) cubes then Cubes cubes
    else failwith "Invalid cubes shape"

// A test fixture for the Tesseract type
[<TestFixture>]
type TesseractTests () =

    // A test case for the ofCubes function
    [<Test>]
    member this.``ofCubes should create a valid tesseract from a list of eight cubes`` () =
        // Arrange
        let cubes = create4 2 2 2 2 (fun i j k l -> i * 8 + j * 4 + k * 2 + l) // create a list of eight cubes with values from 0 to 15
        // Act
        let t = ofCubes cubes // create a tesseract from the cubes
        // Assert
        t |> should equal (Cubes cubes) // the tesseract should be equal to the cubes

    // A test case for the ofFunc function
    [<Test>]
    member this.``ofFunc should create a valid tesseract from a function`` () =
        // Arrange
        let f i j k l = i + j + k + l // define a function that returns the sum of the coordinates
        // Act
        let t = ofFunc f // create a tesseract from the function
        // Assert
        t |> should equal (Func f) // the tesseract should be equal to the function

    // A test case for the get function
    [<Test>]
    member this.``get should return the value of a tesseract at a given quadruple of coordinates`` () =
        // Arrange
        let t = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let coords = [(0, 0, 0, 0); (1, 1, 1, 1); (0, 1, 0, 1); (1, 0, 1, 0)] // define some coordinates to test
        let expected = [0; 15; 5; 10] // define the expected values at the coordinates
        // Act
        let actual = List.map (get -1 t) coords // get the actual values at the coordinates, using -1 as the default value
        // Assert
        actual |> should equal expected // the actual values should be equal to the expected values

    // A test case for the get function with out of bounds coordinates
    [<Test>]
    member this.``get should return the default value of a tesseract at out of bounds coordinates`` () =
        // Arrange
        let t = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let coords = [(-1, 0, 0, 0); (2, 2, 2, 2); (0, 2, 0, 1); (1, 0, 1, 2)] // define some out of bounds coordinates to test
        let defaultVal = -1 // define the default value to use
        let expected = List.replicate 4 defaultVal // define the expected values at the coordinates, which should all be the default value
        // Act
        let actual = List.map (get defaultVal t) coords // get the actual values at the coordinates, using the default value
        // Assert
        actual |> should equal expected // the actual values should be equal to the expected values

    // A test case for the map function
    [<Test>]
    member this.``map should apply a function to each value of a tesseract`` () =
        // Arrange
        let t = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let f x = x * x // define a function that squares its argument
        let expected = create4 2 2 2 2 (fun i j k l -> f (i * 8 + j * 4 + k * 2 + l)) // create the expected tesseract by applying f to each value
        // Act
        let actual = map f t // create the actual tesseract by using the map function
        // Assert
        actual |> should equal expected // the actual tesseract should be equal to the expected tesseract

    // A test case for the fold function
    [<Test>]
    member this.``fold should reduce a tesseract to a cube by applying a function along the first dimension`` () =
        // Arrange
        let t = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let f x y = x + y // define a function that adds its arguments
        let init = 0 // define the initial value to use
        let expected = create3 2 2 2 (fun j k l -> f (j * 4 + k * 2 + l) (j * 4 + k * 2 + l + 8)) // create the expected cube by applying f along the first dimension
        // Act
        let actual = fold f init t // create the actual cube by using the fold function
        // Assert
        actual |> should equal expected // the actual cube should be equal to the expected cube

    // A test case for the zip function
    [<Test>]
    member this.``zip should combine two tesseracts with a function`` () =
        // Arrange
        let t1 = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let t2 = const (2, 2, 2, 2) 10 // create a tesseract of integers with 10
        let f x y = x * y // define a function that multiplies its arguments
        let expected = create4 2 2 2 2 (fun i j k l -> f (i * 8 + j * 4 + k * 2 + l) 10) // create the expected tesseract by applying f to each pair of values
        // Act
        let actual = zip f t1 t2 // create the actual tesseract by using the zip function
        // Assert
        actual |> should equal expected // the actual tesseract should be equal to the expected tesseract
