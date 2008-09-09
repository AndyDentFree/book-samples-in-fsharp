#light

type Color = R | B
type Tree<'a> = E | T of Color * Tree<'a> * 'a * Tree<'a>

module Tree =
    let Empty = E
    
    let rec Member x t =
        match x, t with
        | _, E -> false
        | x, T(_,a,y,b) ->
            if x < y then Member x a
            elif y < x then Member x b
            else true

    let Balance = function
        | (B,T(R,T(R,a,x,b),y,c),z,d) | 
          (B,T(R,a,x,T(R,b,y,c)),z,d) |
          (B,a,x,T(R,T(R,b,y,c),z,d)) |
          (B,a,x,T(R,b,y,T(R,c,z,d))) -> T(R,T(B,a,x,b),y,T(B,c,z,d))
        | (c,t1,x,t2)                 -> T(c,t1,x,t2)
        
    let Insert x s =
        let rec ins = function
            | E -> T(R,E,x,E)
            | T(color,a,y,b) as s ->
                if x < y then Balance (color,ins a,y,b)
                elif y < x then Balance (color,a,y,ins b)
                else s
        match ins s with
        | E          -> failwith "empty"
        | T(_,a,y,b) -> T(B,a,y,b)

let t1 = Tree.Insert "x" Tree.Empty
let t2 = Tree.Insert "y" t1
let t3 = Tree.Insert "z" t2

let main() =
    printfn "Tree.Empty = %A" Tree.Empty
    printfn "t3 = %A" t3
    printfn "Tree.Member %A %A = %A" "z" t1 (Tree.Member "z" t1)
    printfn "Tree.Member %A %A = %A" "z" t3 (Tree.Member "z" t3)    
