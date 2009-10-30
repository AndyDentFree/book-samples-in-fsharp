namespace Stack

module ListStack =
    type 'a Stack = 'a list

    let empty = []
    let isEmpty = List.isEmpty
    let cons (x, s) = x :: s
    let head = List.head
    let tail = List.tail

module CustomStack =
    type 'a Stack = Nil | Cons of 'a * 'a Stack
    exception Empty

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
