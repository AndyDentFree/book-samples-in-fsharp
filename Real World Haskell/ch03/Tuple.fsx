let third (a, b, c) = c

// F# gives compiler warnings on incomplete matches
let complicated (true, a, x::xs, 5) = (a, xs)
