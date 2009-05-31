let mySum xs =
    let rec helper acc = function
        | (x::xs) -> helper (acc + x) xs
        | _       -> acc
    helper 0 xs
