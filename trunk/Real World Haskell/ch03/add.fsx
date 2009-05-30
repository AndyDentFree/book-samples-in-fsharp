let myNot = function
    | true  -> false
    | false -> true

let rec sumList = function
    | x::xs -> x + sumList xs
    | []    -> 0
