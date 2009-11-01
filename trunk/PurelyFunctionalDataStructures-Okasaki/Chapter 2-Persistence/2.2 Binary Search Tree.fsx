#load "2.2 Binary Search Tree.fs"
open BinarySearchTree
open BinarySearchTree.Set

let t1 = T(E, 1, E)
let t2 = T(t1, 2, E)
let t3 = T(t2, 3, E)

let t4 = T(E, "a", E)
let t5 = Set.insert ("c", t4)
let t6 = Set.insert ("b", t5)

printfn "Set.Member %d %A = %A" 1 t1 (Set.isMember (1, t1))
printfn "Set.Member %d %A = %A" 3 t1 (Set.isMember (3, t1))
printfn "Set.Member %d %A = %A" 3 t3 (Set.isMember (3, t3))

printfn "Set.Member \"%s\" %A = %A" "a" t4 (Set.isMember ("a", t4))
printfn "Set.Member \"%s\" %A = %A" "c" t4 (Set.isMember ("c", t4))
printfn "Set.Member \"%s\" %A = %A" "c" t6 (Set.isMember ("c", t6))
