open System

// Computation expression builder which rounds bound computations
// to a fixed number of digits
type RoundingWorkflow(sigDigs : int) =
    
    let round (x : float) = Math.Round(float x, sigDigs)
    
    // Due to a result being constrained to type float, you can only use
    // let! against float values. (Otherwise will get a compiler error.)
    member this.Bind(result : float, rest : float -> float) =
        let result' = round result
        rest result'
    
    member this.Return(x : float) = round x
    
let withPrecision sigDigs = new RoundingWorkflow(sigDigs)


printfn "%-20s | %s" "Actual" "withPrecision 3"
let test =
    withPrecision 3 {
        let! x = 2.0 / 12.0
        printfn "%-20O | %A" (2.0 / 12.0) x
        let! y = 3.5
        printf "%-20O | " (x / y)
        return x / y
    }
    
printfn "%A" test
