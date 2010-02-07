type Result = Success of float | DivByZero

let divide x y =
    match y with
    | 0.0 -> DivByZero
    | _   -> Success(x / y)
    
let let_with_check result restOfComputation =
    match result with
    | DivByZero  -> DivByZero
    | Success(x) -> restOfComputation x

let totalResistance1 r1 r2 r3 =
    let_with_check
        (divide 1.0 r1)
        (fun x ->
            let_with_check
                (divide 1.0 r2)
                (fun y ->
                    let_with_check
                        (divide 1.0 r3)
                        (fun z -> divide 1.0 (x + y + z))
                )
        )
            
let totalResistance2 r1 r2 r3 =
    let_with_check (divide 1.0 r1) (fun x ->
    let_with_check (divide 1.0 r2) (fun y ->
    let_with_check (divide 1.0 r3) (fun z ->
    divide 1.0 (x + y + z) ) ) )

printfn "%A" (totalResistance1 2.0 0.0 4.0)
printfn "%A" (totalResistance1 2.0 3.0 4.0)
