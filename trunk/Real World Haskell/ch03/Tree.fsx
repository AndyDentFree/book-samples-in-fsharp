#light

type 'a Tree = Node of 'a * 'a Tree * 'a Tree
             | Empty

let bad_nodesAreSame node1 node2 =
    match node1, node2 with
    | Node(a, _, _), Node(a, _, _) -> Some a
    | _            , _             -> None
    