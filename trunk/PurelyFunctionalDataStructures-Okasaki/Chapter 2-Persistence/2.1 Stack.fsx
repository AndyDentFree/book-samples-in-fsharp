#load "2.1 Stack.fs"
open Stack

// Exercise ListStack
let s1 = ListStack.empty
printfn "ListStack.empty -> %A" s1
printfn "ListStack.isEmpty s1 -> %b" (ListStack.isEmpty s1)

let s2 = ListStack.cons (1, s1)
printfn "ListStack.cons (1, s1) -> %A" s2
let s3 = ListStack.cons (2, s2)
let s4 = ListStack.cons (3, s3)
printfn "s4 = %A" s4

printfn "ListStack.head s4 -> %A" (ListStack.head s4)
printfn "ListStack.tail s4 -> %A" (ListStack.tail s4)
printfn "s3 -||- s4 -> %A" (ListStack.(-||-) s3 s4)
printfn "ListStack.update (s4, 0, -1) -> %A" (ListStack.update (s4, 0, -1))
printfn "ListStack.update (s4, 2, -1) -> %A" (ListStack.update (s4, 2, -1))
printfn "ListStack.suffixes [1; 2; 3; 4] -> %A\n\n" (ListStack.suffixes [1; 2; 3; 4])

// Exercise CustomStack
let s5 = CustomStack.empty
printfn "CustomStack.empty -> %A" s5
printfn "CustomStack.isEmpty s5 -> %b" (CustomStack.isEmpty s5)

let s6 = CustomStack.cons (1, s5)
printfn "CustomStack.cons (1, s5) -> %A" s6
let s7 = CustomStack.cons (2, s6)
let s8 = CustomStack.cons (3, s7)
printfn "s8 = %A" s8

printfn "CustomStack.head s8 -> %A" (CustomStack.head s8)
printfn "CustomStack.tail s8 -> %A" (CustomStack.tail s8)
printfn "s3 -||- s4 -> %A" (CustomStack.(-||-) s7 s8)
printfn "CustomStack.update (s8, 0, -1) -> %A" (CustomStack.update (s8, 0, -1))
printfn "CustomStack.update (s8, 2, -1) -> %A\n\n" (CustomStack.update (s8, 2, -1))
