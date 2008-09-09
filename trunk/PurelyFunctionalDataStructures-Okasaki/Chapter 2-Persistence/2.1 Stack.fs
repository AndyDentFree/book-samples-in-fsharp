#light

type Stack<'a> =
    abstract Empty: Stack<'a>
    abstract IsEmpty: bool
    abstract Cons: 'a -> Stack<'a>
    abstract Head: 'a
    abstract Tail: Stack<'a>
    abstract ToString: unit -> string

module StackOps =
    let isEmpty (s: Stack<_>) = s.IsEmpty
    let cons (s: Stack<_>) x = s.Cons(x)
    let head (s: Stack<_>) = s.Head
    let tail (s: Stack<_>) = s.Tail
    let (|Empty|_|) (s: Stack<_>) =
        if isEmpty s then Some Empty
        else None
    let rec (|-|) xs ys =
        match xs with
        | Empty -> ys
        | _     -> cons (tail xs |-| ys) (head xs)
    let rec update (s: Stack<_>) i y =
        match s, i, y with
        | Empty, _, _ -> failwith "empty stack"
        | _,     0, _ -> cons (tail s) y
        | _           -> cons (update (tail s) (i-1) y) (head s)
    let print (s: Stack<_>) = printfn "%A" s
    
let rec ListStack(list) =
    { new Stack<'a> with
        override s.Empty = ListStack([])
        override s.IsEmpty = list = []
        override s.Cons x = ListStack(x :: list)
        override s.Head = List.hd list
        override s.Tail = ListStack(List.tl list)
        override s.ToString() = any_to_string list }

type CustomType<'a> = Nil | Cons of 'a * Stack<'a>

let rec CustomStack(value) =
    { new Stack<'a> with
        override s.Empty = CustomStack(Nil)
        override s.IsEmpty = value = Nil
        override s.Cons x = CustomStack(Cons(x, s))
        override s.Head =
            match value with
            | Nil        -> failwith "empty"
            | Cons(x,s') -> x
        override s.Tail =
            match value with
            | Nil        -> failwith "empty"
            | Cons(x,s') -> s'
        override s.ToString() = any_to_string value }
            

// Empty is not implemented correctly here; should be a static member.

open StackOps
let ls1 = ListStack([1])
let ls2 = cons ls1 2
let ls3 = cons ls2 3

let cs1: Stack<int> = CustomStack(Nil)
let cs2 = cons cs1 1
let cs3 = cons cs2 2

let main() =
    printfn "isEmpty %A = %A" ls1 (isEmpty ls1)
    printfn "head %A = %A" ls2 (head ls2)
    printfn "tail %A = %A" ls3 (tail ls3)

    printfn "isEmpty %A = %A" cs1 (isEmpty cs1)
    printfn "head %A = %A" cs2 (head cs2)
    printfn "tail %A = %A" cs3 (tail cs3)
