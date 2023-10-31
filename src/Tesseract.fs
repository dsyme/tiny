// Tesseract.fs
// Defines a type Tesseract<'a> that represents a four-dimensional hypercube of values of type 'a

module Tesseract

open System

// A tesseract is a cube of cubes of cubes of cubes
// It can be constructed from a list of eight cubes, or from a function that maps four coordinates to a value
type Tesseract<'a> =
    | Cubes of 'a list list list list
    | Func of (int -> int -> int -> int -> 'a)

// A helper function to check if a list has a given length
let hasLength n xs = List.length xs = n

// A helper function to check if a list of lists has a given shape
let hasShape (m, n) xss = hasLength m xss && List.forall (hasLength n) xss

// A helper function to check if a list of lists of lists has a given shape
let hasShape3 (l, m, n) xsss = hasLength l xsss && List.forall (hasShape (m, n)) xsss

// A helper function to check if a list of lists of lists of lists has a given shape
let hasShape4 (k, l, m, n) xssss = hasLength k xssss && List.forall (hasShape3 (l, m, n)) xssss

// A helper function to create a list of n elements using a function
let rec create n f =
    match n with
    | 0 -> []
    | _ -> f (n - 1) :: create (n - 1) f

// A helper function to create a list of lists of n by m elements using a function
let create2 n m f = create n (fun i -> create m (fun j -> f i j))

// A helper function to create a list of lists of lists of n by m by l elements using a function
let create3 n m l f = create n (fun i -> create2 m l (fun j k -> f i j k))

// A helper function to create a list of lists of lists of lists of n by m by l by k elements using a function
let create4 n m l k f = create n (fun i -> create3 m l k (fun j k l -> f i j k l))

// A helper function to get the value at a given index in a list, or a default value if out of bounds
let getValueOrDefault defaultVal i xs =
    if i < 0 || i >= List.length xs then defaultVal
    else List.item i xs

// A helper function to get the value at a given pair of indices in a list of lists, or a default value if out of bounds
let getValueOrDefault2 defaultVal (i, j) xss =
    getValueOrDefault defaultVal i xss |> getValueOrDefault defaultVal j

// A helper function to get the value at a given triple of indices in a list of lists of lists, or a default value if out of bounds
let getValueOrDefault3 defaultVal (i, j, k) xsss =
    getValueOrDefault defaultVal i xsss |> getValueOrDefault2 defaultVal (j, k)

// A helper function to get the value at a given quadruple of indices in a list of lists of lists of lists, or a default value if out of bounds
let getValueOrDefault4 defaultVal (i, j, k, l) xssss =
    getValueOrDefault defaultVal i xssss |> getValueOrDefault3 defaultVal (j, k, l)

// A function to get the value of a tesseract at a given quadruple of coordinates
// If the coordinates are out of bounds, returns a default value
let get defaultVal (i, j, k, l) t =
    match t with
    | Cubes xssss -> getValueOrDefault4 defaultVal (i, j, k, l) xssss
    | Func f -> f i j k l

// A function to create a tesseract from a list of eight cubes
// The cubes must have the same shape of 2 by 2 by 2
// The cubes are arranged in the following order:
// 0 1
// 2 3
// 4 5
// 6 7
let ofCubes cubes =
    if hasShape4 (2, 2, 2, 2) cubes then Cubes cubes
    else failwith "Invalid cubes shape"

// A function to create a tesseract from a function that maps four coordinates to a value
let ofFunc f = Func f

// A function to create a tesseract of a given shape with a constant value
let const (n, m, l, k) x = ofFunc (fun _ _ _ _ -> x)

// A function to create a tesseract of a given shape with values from 0 to n * m * l * k - 1
let iota (n, m, l, k) = ofFunc (fun i j k l -> i * m * l * k + j * l * k + k * l + l)

// A function to create a tesseract of a given shape with random values
let random (n, m, l, k) =
    let rnd = Random ()
    ofFunc (fun _ _ _ _ -> rnd.Next ())

// A function to map a function over the values of a tesseract
let map f t = ofFunc (fun i j k l -> f (get Unchecked.defaultof<_> (i, j, k, l) t))

// A function to fold a function over the values of a tesseract along the first dimension
let fold f init t =
    let n = match t with
            | Cubes xssss -> List.length xssss
            | Func _ -> failwith "Cannot fold over infinite dimension"
    create2 n 2 2 (fun j k l ->
        create n (fun i -> get Unchecked.defaultof<_> (i, j, k, l) t)
        |> List.fold f init)

// A function to zip two tesseracts with a function
let zip f t1 t2 = ofFunc (fun i j k l -> f (get Unchecked.defaultof<_> (i, j, k, l) t1) (get Unchecked.defaultof<_> (i, j, k, l) t2))

// A function to print a tesseract to the console in a human-readable format
let print t =
    let n = match t with
            | Cubes xssss -> List.length xssss
            | Func _ -> 2 // assume a default size of 2 for infinite dimension
    for i = 0 to n - 1 do
        printfn "Cube %d:" i
        let cube = get Unchecked.defaultof<_> (i, 0, 0, 0) t // get the i-th cube
        match cube with
        | Cubes xss -> xss |> List.iter (printfn "%A") // print the cube as a list of lists
        | Func f -> // print the cube as a 2 by 2 by 2 matrix
            for j = 0 to 1 do
                for k = 0 to 1 do
                    printf "["
                    for l = 0 to 1 do
                        printf "%A " (f i j k l)
                    printfn "]"
                printfn ""
        printfn ""
