#load "Example 10-9. Implementation of a State Workflow.fsx"

let Add x =
    state {
        let! currentTotal, history = GetState
        do! SetState (currentTotal + x, (sprintf "Added %d" x) :: history)
    }

let Subtract x =
    state {
        let! currentTotal, history = GetState
        do! SetState (currentTotal - x, (sprintf "Subtracted %d" x) :: history)
    }
    
let Multiply x =
    state {
        let! currentTotal, history = GetState
        do! SetState (currentTotal * x, (sprintf "Multiplied by %d" x) :: history)
    }

let Divide x =
    state {
        let! currentTotal, history = GetState
        do! SetState (currentTotal / x, (sprintf "Divided by %d" x) :: history)
    }

let calculatorActions =
    state {
        do! Add 2
        do! Multiply 10
        do! Divide 5
        do! Subtract 8
        
        return "Finished"
    }

let sfResult, finalState = Run calculatorActions (0, [])
printfn "%A" sfResult
printfn "%A" finalState
