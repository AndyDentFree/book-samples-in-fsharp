let rec foldl step zero = function
    | x::xs -> foldl step (step zero x) xs
    | []    -> zero

let step acc x = acc + x
let foldSum xs = foldl step 0 xs

