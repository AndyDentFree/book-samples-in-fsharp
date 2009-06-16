let rec foldl step zero = function
    | x::xs -> foldl step (step zero x) xs
    | []    -> zero


let foldSum xs =
    let step acc x = acc + x
    foldl step 0 xs

let niceSum xs = foldl (+) 0 xs

let rec foldr step zero = function
    | x::xs -> step x (foldr step zero xs)
    | []    -> zero

let rec filter p = function
    | []             -> []
    | x::xs when p x -> x :: filter p xs
    | x::xs          -> filter p xs
        
let myFilter p xs =
    let step x = function
        | ys when p x -> x :: ys
        | ys          -> ys
    foldr step [] xs

let myMap f xs =
    let step x ys = f x :: ys
    foldr step [] xs

let myFoldl f z xs =
    let step x g a = g (f a x)
    foldr step id xs z

let identity xs = foldr List.append [] xs

let append xs ys = foldr List.append ys xs
