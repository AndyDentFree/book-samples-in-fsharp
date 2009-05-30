open System

let rec square = function
    | (x::xs) -> x*x :: square xs
    | []      -> []

let upperCase (s : string) =
    let rec loop = function
        | (x::xs) -> Char.ToUpper(x) :: loop xs
        | []      -> []
    let result = loop (s.ToCharArray() |> List.of_array) |> List.to_array
    new String(result)

let squareOne x = x * x
let square2 xs = List.map squareOne xs

let upperCase2 xs = List.map Char.ToUpper xs

