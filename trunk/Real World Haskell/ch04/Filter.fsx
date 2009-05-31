let odd x = x % 2 = 1

let rec oddList = function
    | x::xs when odd x -> x :: oddList xs
    | x::xs            -> oddList xs
    | _                -> []
