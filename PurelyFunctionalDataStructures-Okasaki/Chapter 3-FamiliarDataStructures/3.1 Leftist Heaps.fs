#light

type Heap<'a> =
    | E
    | T of int * 'a * Heap<'a> * Heap<'a> with
    member h.Rank =
        match h with
        | E -> 0
        | T(r,_,_,_) -> r

module Heap =
    let Make (x: 'a) (a: Heap<'a>) (b: Heap<'a>) =
        if a.Rank >= b.Rank then T(b.Rank + 1,x,a,b)
        else T(a.Rank + 1,x,b,a)
    
    let Empty = E
    
    let IsEmpty = function
        | E -> true
        | _ -> false
    
    let rec Merge a b =
        match a, b with
        | h, E -> h
        | E, h -> h
        | (T(_,x,a1,b1) as h1), (T(_,y,a2,b2) as h2) ->
            if x < y then Make x a1 (Merge b1 h2)
            else Make y a2 (Merge h1 b2)
    
    let Insert x h =
        Merge (T(1,x,E,E)) h
    
    let FindMin h =
        match h with
        | E          -> failwith "empty Heap"
        | T(_,x,a,b) -> x
    
    let DeleteMin h =
        match h with
        | E          -> failwith "empty Heap"
        | T(_,x,a,b) -> Merge a b
        
let h1A = T(0, 10, E, E)
let h1B = T(0, 1, E, E)
let h2 = Heap.Insert 5 h1A
let h3 = Heap.Insert 3 h2
let h4 = Heap.Insert 2 h3
let h5 = Heap.Insert 9 h4

let main() =
    printfn "Heap.Make %A %A %A = %A" 1 h1A h1B (Heap.Make 1 h1A h1B)
    printfn "Heap.IsEmpty %A = %A" Heap.Empty (Heap.IsEmpty Heap.Empty)
    printfn "Heap.IsEmpty %A = %A" h1A (Heap.IsEmpty h1A)
    printfn "Heap.Merge %A %A = %A" h1A h1B (Heap.Merge h1A h1B)
    printfn "Heap.Insert %A %A = %A" 5 h1A (Heap.Insert 5 h1A)
    printfn "Heap.FindMin %A = %A" h5 (Heap.FindMin h5)
    printfn "Heap.DeleteMin %A = %A" h5 (Heap.DeleteMin h5)
