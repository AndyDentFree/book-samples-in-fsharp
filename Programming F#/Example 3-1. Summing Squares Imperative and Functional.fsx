let square x = x * x

let imperativeSum numbers =
    let mutable total = 0
    for i in numbers do
        let x = square i
        total <- total + x
    total

let functionalSum numbers =
    numbers
    |> Seq.map square
    |> Seq.sum

printfn "imperativeSum [1; 2; 3; 4] = %d" (imperativeSum [1; 2; 3; 4])

printfn "functionalSum [1; 2; 3; 4] = %d" (functionalSum [1; 2; 3; 4])
