#load "Tree.fsx"
open Tree

let bad_nodesAreSame node1 node2 =
    match node1, node2 with
    | Node(a, _, _), Node(a, _, _) -> Some a
    | _            , _             -> None

let nodesAreSame node1 node2 =
    match node1, node2 with
    | Node(a, _, _), Node(b, _, _) when a = b -> Some a
    | _            , _                        -> None
