#light

let identity x = x

let trueMaker x = true

type BoolOrInt =
    | Hot of bool
    | Cold of int

let hotMaker x = Hot

let help f =
    Hot(
        trueMaker(
            if trueMaker( )
                then f
                else trueMaker))

type Chain =
    | Link of int * (int -> Chain)

let rec ints n =
    Link(n+1,ints)

let rec skips n =
    Link(n+2,skips)

let dividesEvenly n c =
    n % c = 0

let isMod5Or7 n =
    if dividesEvenly n 5
        then true
        else dividesEvenly n 7

let rec someInts n =
    if isMod5Or7 (n+1)
        then Link(n+1,someInts)
        else someInts (n+1)

let rec chainItem n = function
    | Link(i,f) ->
        if n = 1
            then i
            else chainItem (n-1) (f i)

let isPrime n =
    let rec hasNoDivisors n c =
        if c = 1
            then true
            else
                if dividesEvenly n c
                    then false
                    else hasNoDivisors n (c-1)
    hasNoDivisors n (n-1)

let rec primes n =
    if isPrime (n+1)
        then Link(n+1,primes)
        else primes (n+1)

let rec fibs n m =
    Link(n+m,fibs m)

let fibs1 m =
    Link(1+m,fibs m)

let fibs2 m =
    Link(2+m,fibs m)
