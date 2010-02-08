type Result = Success of float | DivByZero

let divide x y =
    match y with
    | 0.0 -> DivByZero
    | _   -> Success(x / y)

type DefinedBuilder() =

    member this.Bind ((x : Result), (rest : float -> Result)) =
        // If the result is Success(_) then execute the
        // rest of the function. Otherwise terminate it
        // prematurely.
        match x with
        | Success(x) -> rest x
        | DivByZero  -> DivByZero
    
    member this.Return (x : 'a) = x

let defined = DefinedBuilder()

let totalResistance r1 r2 r3 =
    defined.Bind(
        (divide 1.0 r1),
        (fun x ->
            defined.Bind(
                (divide 1.0 r2),
                (fun y ->
                    defined.Bind(
                        (divide 1.0 r3),
                        (fun z ->
                            defined.Return(
                                divide 1.0 (x + y + z)
                            )
                        )
                    )
                )
            )
        )
    )
                           

printfn "%A" (totalResistance 2.0 0.0 4.0)
printfn "%A" (totalResistance 2.0 3.0 4.0)
