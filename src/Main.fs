// Main.fs
// Contains the main program that uses the Tesseract and SMUHK types to demonstrate their power and elegance

module Main

open System
open Tesseract
open SMUHK

// A function to print a message to the console with a separator
let printMessage msg =
    printfn "===================="
    printfn "%s" msg
    printfn "===================="

// A function to run a test case with a given name and action
let runTest name action =
    printMessage name
    action ()
    printfn ""

[<EntryPoint>]
let main argv =
    // Create some tesseracts with different values and types
    let t1 = iota (2, 2, 2, 2) // a tesseract of integers from 0 to 15
    let t2 = const (2, 2, 2, 2) "Hello" // a tesseract of strings with "Hello"
    let t3 = random (2, 2, 2, 2) // a tesseract of random integers
    let t4 = ofFunc (fun i j k l -> i + j + k + l) // a tesseract of integers that are the sum of the coordinates

    // Print the tesseracts to the console
    runTest "Print tesseracts" (fun () ->
        print t1
        print t2
        print t3
        print t4)

    // Map a function over the tesseracts using the SMUHK type class
    runTest "Map over tesseracts" (fun () ->
        let t1' = map ((+) 1) t1 tesseractSMUHK // add one to each element of t1
        let t2' = map String.length t2 tesseractSMUHK // get the length of each string in t2
        let t3' = map ((*) 2) t3 tesseractSMUHK // multiply each element of t3 by two
        let t4' = map ((-) 10) t4 tesseractSMUHK // subtract ten from each element of t4
        print t1'
        print t2'
        print t3'
        print t4')

    // Fold a function over the tesseracts using the SMUHK type class
    runTest "Fold over tesseracts" (fun () ->
        let sum1 = fold (+) 0 t1 tesseractSMUHK // get the sum of all elements in t1
        let concat2 = fold (+) "" t2 tesseractSMUHK // concatenate all strings in t2
        let max3 = fold max 0 t3 tesseractSMUHK // get the maximum element in t3
        let min4 = fold min 10 t4 tesseractSMUHK // get the minimum element in t4
        printfn "Sum of t1: %d" sum1
        printfn "Concatenation of t2: %s" concat2
        printfn "Maximum of t3: %d" max3
        printfn "Minimum of t4: %d" min4)

    // Zip two tesseracts with a function using the SMUHK type class
    runTest "Zip tesseracts" (fun () ->
        let t12 = zip (+) t1 t2 tesseractSMUHK // add the elements of t1 and t2, converting t2 to integers
        let t34 = zip (*) t3 t4 tesseractSMUHK // multiply the elements of t3 and t4
        print t12
        print t34)

    // Use the SMUHK type class with other types, such as List and Option
    runTest "Use SMUHK with other types" (fun () ->
        let xs = [1; 2; 3; 4] // a list of integers
        let ys = [Some 1; None; Some 3; Some 4] // a list of optional integers
        let zs = zip (*) xs ys listSMUHK // zip the lists with multiplication, using the listSMUHK instance
        let ws = map Option.get zs optionSMUHK // map the Option.get function over the list, using the optionSMUHK instance
        printfn "List xs: %A" xs
        printfn "List ys: %A" ys
        printfn "List zs: %A" zs
        printfn "List ws: %A" ws)

    0 // return an integer exit code
