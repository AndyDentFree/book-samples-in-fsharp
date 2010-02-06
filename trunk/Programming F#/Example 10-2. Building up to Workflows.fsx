type Result = Success of float | DivByZero

let divide x y =
    match y with
    | 0.0 -> DivByZero
    | _   -> Success(x / y)

let totalResistance r1 r2 r3 =
    let r1Result = divide 1.0 r1
    match r1Result with
    | DivByZero
        -> DivByZero
    | Success(x)
        ->  let r2Result = divide 1.0 r2
            match r2Result with
            | DivByZero
                -> DivByZero
            | Success(y)
                ->  let r3Result = divide 1.0 r3
                    match r3Result with
                    | DivByZero
                        -> DivByZero
                    | Success(z)
                        ->  let finalResult = divide 1.0 (x + y + z)
                            finalResult

printfn "%A" (totalResistance 2.0 0.0 4.0)
printfn "%A" (totalResistance 2.0 3.0 4.0)
