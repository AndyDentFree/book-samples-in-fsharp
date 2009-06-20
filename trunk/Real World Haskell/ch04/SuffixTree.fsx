let rec tails = function
    | (_::xs') as xs -> xs :: tails xs'
    | x              -> [x]

let rec suffixes = function
    | (_::xs') as xs -> xs :: suffixes xs'
    | _              -> []

let rec noAsPattern = function
    | x::xs -> (x::xs) :: noAsPattern xs
    | _     -> []

let init xs =
    let arr = List.to_array xs
    Array.sub arr 0 ((List.length xs) - 1)
    |> List.of_array
    
let suffixes2  xs = init (tails xs)

let compose f g x = f (g x)

let suffixes3 xs = compose init tails xs

//let suffixes4 = compose init tails

let (.>) = compose

//let suffixes5 = init .> tail




let words (x : string) = x.Split(' ') |> List.of_array

let isUpper (x : string) = System.Text.RegularExpressions.Regex.IsMatch(x, "^[A-Z]")

let filter = List.filter

let length = List.length

let capCount = length .> filter (isUpper) .> words
