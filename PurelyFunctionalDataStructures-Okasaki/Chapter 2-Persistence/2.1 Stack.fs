namespace Stack

module ListStack =
    type 'a Stack = 'a list
    exception Subscript

    let empty = []
    let isEmpty = List.isEmpty
    let cons (x, s) = x :: s
    let head = List.head
    let tail = List.tail
    let rec (-||-) xs ys =
        match xs, ys with
        | []        , _ -> ys
        | (x :: xs'), _ -> x :: (xs' -||- ys)
    let rec update = function
        | []     , _, _ -> raise Subscript
        | _ :: xs, 0, y -> y :: xs
        | x :: xs, i, y -> x :: update (xs, i-1, y)
    let rec suffixes = function
        | []      -> [[]]
        | x :: xs -> [x :: xs] @ (suffixes xs)

module CustomStack =
    type 'a Stack = Nil | Cons of 'a * 'a Stack
    exception Empty
    exception Subscript

    let empty = Nil
    let isEmpty = function
        | Nil -> true
        | _   -> false
    let cons (x, s) = Cons(x, s)
    let head = function
        | Nil         -> raise Empty
        | Cons (x, s) -> x
    let tail = function
        | Nil         -> raise Empty
        | Cons (x, s) -> s
    let rec (-||-) xs ys =
        if isEmpty xs
            then ys
            else cons (head xs, tail xs -||- ys)
    let rec update = function
        | Nil         , _, _ -> raise Subscript
        | Cons (x, xs), 0, y -> Cons (y, xs)
        | Cons (x, xs), i, y -> Cons (x, update (xs, i-1, y))
