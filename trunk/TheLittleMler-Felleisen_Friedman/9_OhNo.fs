#light

type List<'a> =
    | Empty
    | Cons of 'a * List<'a>

type Box =
    | Bacon
    | Ix of int

let isBacon = function
    | Bacon -> true
    | Ix _  -> false

exception NoBacon of int

let rec whereIs = function
    | Empty           -> raise (NoBacon 0)
    | Cons(abox,rest) ->
        if isBacon abox
            then 1
            else 1 + (whereIs rest)

exception OutOfRange

let rec listItem = function
    | _,Empty -> raise OutOfRange
    | n,Cons(abox,rest) ->
        if n = 1
            then abox
            else listItem(n-1,rest)

let rec find (n,boxes) =
    try
        check(n,boxes,listItem(n,boxes))
    with OutOfRange ->
        find(n/2,boxes)
and check = function
    | n,_,Bacon     -> n
    | _,boxes,Ix(i) -> find(i,boxes)

let rec path (n,boxes) =
    try
        Cons(n,
            check2 (boxes,listItem (n,boxes)))
    with OutOfRange -> path (n/2,boxes)
and check2 = function
    | _,Bacon     -> Empty
    | boxes,Ix(i) -> path (i,boxes)
