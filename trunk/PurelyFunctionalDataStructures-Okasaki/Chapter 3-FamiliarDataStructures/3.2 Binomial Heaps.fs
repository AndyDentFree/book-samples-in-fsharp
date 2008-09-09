#light

type Tree<'a> = Node of int * 'a * List<Tree<'a>>

type Heap<'a> = List<Tree<'a>>

module Heap =
    let Empty: Heap<'a> = []
    
    let IsEmpty ts = ts = Empty
    
    let Rank = function
        | Node(r,_,_) -> r
    
    let Root = function
        | Node(_,x,_) -> x
    
    let Link t1 t2 =
        match t1, t2 with
        | Node(r,x1,c1), Node(_,x2,c2) ->
            if x1 < x2 then Node(r+1,x1,t2::c1)
            else Node(r+1,x2,t1::c2)
    
    let rec InsTree t = function
        | []            -> [t]
        | t'::ts' as ts ->
            if Rank t < Rank t' then t::ts
            else InsTree (Link t t') ts'
    
    let Insert x ts =
        InsTree (Node(0,x,[])) ts
    
    let rec Merge ts1 ts2 =
        match ts1, ts2 with
        | _, [] -> ts1
        | [], _ -> ts2
        | t1::ts1', t2::ts2' ->
            if Rank t1 < Rank t2 then t1::Merge ts1' ts2
            elif Rank t2 < Rank t1 then t2:: Merge ts1 ts2'
            else InsTree (Link t1 t2) (Merge ts1' ts2')
    
    let rec RemoveMinTree = function
        | []    -> failwith "empty Tree"
        | [t]   -> (t,[])
        | t::ts ->
            let t',ts' = RemoveMinTree ts
            if Root t < Root t' then (t,ts)
            else (t',t::ts')
    
    let FindMin ts =
        let t,_ = RemoveMinTree ts
        Root t
    
    let DeleteMin ts =
        match RemoveMinTree ts with
        | Node(_,x,ts1),ts2 ->
            Merge (List.rev ts1) ts2

let t1 = Node(0, 1, [])
let t2 = Node(0, 2, [])
let h1 = Heap.Insert 1 Heap.Empty
let h2 = Heap.Insert 10 h1
let h3 = Heap.Insert 5 h2

let main() =
    printfn "Heap.Empty = %A" Heap.Empty
    printfn "Heap.IsEmpty %A = %A" Heap.Empty (Heap.IsEmpty Heap.Empty)
    printfn "Heap.IsEmpty %A = %A" h1 (Heap.IsEmpty h1)
    printfn "Heap.FindMin %A = %A" h3 (Heap.FindMin h3)
    printfn "Heap.Rank %A = %A" t1 (Heap.Rank t1)
    printfn "Heap.Root %A = %A" t1 (Heap.Root t1)
    printfn "Heap.Link %A %A = %A" t1 t2 (Heap.Link t1 t2)
    printfn "Heap.InsTree %A %A = %A" t2 [t1] (Heap.InsTree t2 [t1])
    printfn "Heap.Insert %A %A = %A" (-1) [t1] (Heap.Insert (-1) [t1])
    printfn "Heap.Merge %A %A = %A" [t1] [t2] (Heap.Merge [t1] [t2])
    printfn "Heap.RemoveMinTree %A = %A" h3 (Heap.RemoveMinTree h3)
    printfn "Heap.FindMin %A = %A" h3 (Heap.FindMin h3)
    printfn "Heap.DeleteMin %A = %A" h3 (Heap.DeleteMin h3)
