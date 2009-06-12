let base' = 65521

let adler32 (xs : char list) = 
    let rec helper a b = function
        | (x::xs) ->
            let a' = (a + (int x &&& 0xff)) % base'
            let b' = (a' + b) % base'
            in helper a' b' xs
        | _       ->
           (b <<< 16) ||| a
    helper 1 0 xs
