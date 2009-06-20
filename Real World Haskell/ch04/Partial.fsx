let isInfixOf x (y : string) = y.Contains(x)

let any = List.exists

let isInAny needle haystack =
    let inSequence s = isInfixOf needle s
    any inSequence haystack

let isInAny2 needle haystack = any (fun s -> isInfixOf needle s) haystack
