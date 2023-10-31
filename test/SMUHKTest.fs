// SMUHKTest.fs
// Tests the functionality and correctness of the SMUHK type class and its instances

module SMUHKTest

open System
open NUnit.Framework
open FsUnit
open Tesseract
open SMUHK

// A helper function to create a list of lists of lists of lists of n by m by l by k elements using a function
let create4 n m l k f = create n (fun i -> create3 m l k (fun j k l -> f i j k l))

// A helper function to create a tesseract of a given shape with values from 0 to n * m * l * k - 1
let iota (n, m, l, k) = ofFunc (fun i j k l -> i * m * l * k + j * l * k + k * l + l)

// A helper function to create a tesseract of a given shape with a constant value
let const (n, m, l, k) x = ofFunc (fun _ _ _ _ -> x)

// A test fixture for the SMUHK type class
[<TestFixture>]
type SMUHKTests () =

    // A test case for the tesseractSMUHK instance
    [<Test>]
    member this.``tesseractSMUHK should implement the SMUHK type class for the Tesseract type`` () =
        // Arrange
        let t1 = iota (2, 2, 2, 2) // create a tesseract of integers from 0 to 15
        let t2 = const (2, 2, 2, 2) 10 // create a tesseract of integers with 10
        let f x = x * x // define a function that squares its argument
        let g x y = x + y // define a function that adds its arguments
        let init = 0 // define the initial value to use
        // Act
        let t1' = map f t1 tesseractSMUHK // map f over t1 using the tesseractSMUHK instance
        let t2' = fold g init t2 tesseractSMUHK // fold g over t2 using the tesseractSMUHK instance
        let t3 = zip g t1 t2 tesseractSMUHK // zip t1 and t2 with g using the tesseractSMUHK instance
        // Assert
        t1' |> should equal (Tesseract.map f t1) // t1' should be equal to the result of Tesseract.map
        t2' |> should equal (Tesseract.fold g init t2) // t2' should be equal to the result of Tesseract.fold
        t3 |> should equal (Tesseract.zip g t1 t2) // t3 should be equal to the result of Tesseract.zip

    // A test case for the listSMUHK instance
    [<Test>]
    member this.``listSMUHK should implement the SMUHK type class for the List type`` () =
        // Arrange
        let xs = [1; 2; 3; 4] // create a list of integers
        let ys = [10; 20; 30; 40] // create another list of integers
        let f x = x * x // define a function that squares its argument
        let g x y = x + y // define a function that adds its arguments
        let init = 0 // define the initial value to use
        // Act
        let xs' = map f xs listSMUHK // map f over xs using the listSMUHK instance
        let ys' = fold g init ys listSMUHK // fold g over ys using the listSMUHK instance
        let zs = zip g xs ys listSMUHK // zip xs and ys with g using the listSMUHK instance
        // Assert
        xs' |> should equal (List.map f xs) // xs' should be equal to the result of List.map
        ys' |> should equal (List.fold g init ys) // ys' should be equal to the result of List.fold
        zs |> should equal (List.zip xs ys |> List.map (fun (x, y) -> g x y)) // zs should be equal to the result of List.zip and List.map

    // A test case for the optionSMUHK instance
    [<Test>]
    member this.``optionSMUHK should implement the SMUHK type class for the Option type`` () =
        // Arrange
        let x = Some 1 // create an option of integer
        let y = Some 10 // create another option of integer
        let f x = x * x // define a function that squares its argument
        let g x y = x + y // define a function that adds its arguments
        let init = 0 // define the initial value to use
        // Act
        let x' = map f x optionSMUHK // map f over x using the optionSMUHK instance
        let y' = fold g init y optionSMUHK // fold g over y using the optionSMUHK instance
        let z = zip g x y optionSMUHK // zip x and y with g using the optionSMUHK instance
        // Assert
        x' |> should equal (Option.map f x) // x' should be equal to the result of Option.map
        y' |> should equal (Option.fold g init y) // y' should be equal to the result of Option.fold
        z |> should equal (Option.map2 g x y) // z should be equal to the result of Option.map2
test/SMUHKTest.fs
