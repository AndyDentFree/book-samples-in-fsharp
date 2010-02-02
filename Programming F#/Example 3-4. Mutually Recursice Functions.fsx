
// Error: Can't define isOdd without isEven, and vice versa
let isOdd  x = if x = 1 then true else not (isEven (x - 1))
let isEven x = if x = 0 then true else not (isOdd (x - 1))

let rec isOdd  n = (n = 1) || isEven (n - 1)
and     isEven n = (n = 1) || isOdd (n - 1)
