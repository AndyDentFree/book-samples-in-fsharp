#light

type Tree<'a> =
    | E
    | T of Tree<'a> * 'a * Tree<'a>

module Set =
    let rec Member t1 t2 =
        match t1, t2 with
        | _, E        -> false
        | x, T(a,y,b) ->
            if x < y then Member x a
            elif x > y then Member x b
            else true
    
    let rec Insert v t =
        match v, t with
        | x, E               -> T(E,x,E)
        | x, (T(a,y,b) as s) ->
            if x < y then T(Insert x a,y,b)
            elif x > y then T(a,y,Insert x b)
            else s

let t1 = T(E, 1, E)
let t2 = T(t1, 2, E)
let t3 = T(t2, 3, E)

let t4 = T(E, "a", E)
let t5 = Set.Insert "c" t4
let t6 = Set.Insert "b" t5

let main() =
    printfn "Set.Member %d %A = %A" 1 t1 (Set.Member 1 t1)
    printfn "Set.Member %d %A = %A" 3 t1 (Set.Member 3 t1)
    printfn "Set.Member %d %A = %A" 3 t3 (Set.Member 3 t3)
    
    printfn "Set.Member \"%s\" %A = %A" "a" t4 (Set.Member "a" t4)
    printfn "Set.Member \"%s\" %A = %A" "c" t4 (Set.Member "c" t4)
    printfn "Set.Member \"%s\" %A = %A" "c" t6 (Set.Member "c" t6)
