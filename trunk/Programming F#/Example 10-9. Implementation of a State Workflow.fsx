[<AutoOpen>]
module StateWorkflow
open System

type StatefulFunc<'state, 'result> = StatefulFunc of ('state -> 'result * 'state)

let Run (StatefulFunc f) initialState = f initialState

type StateBuilder() =

    member this.Bind(
                        result : StatefulFunc<'state, 'a>,
                        restOfComputation : 'a -> StatefulFunc<'state, 'b>
                    ) =
                    
        StatefulFunc(fun initialState ->
            let result, updatedState = Run result initialState
            Run (restOfComputation result) updatedState
        )
    
    member this.Combine(
                            partOne : StatefulFunc<'state, unit>,
                            partTwo : StatefulFunc<'state, 'a>
                        ) =
                        
        StatefulFunc(fun initialState ->
            let (), updatedState = Run partOne initialState
            Run partTwo updatedState
        )
    
    member this.Delay(
                        restOfComputation : unit -> StatefulFunc<'state, 'a>
                    ) =
        
        StatefulFunc (fun initialState ->
            Run (restOfComputation()) initialState
        )
    
    member this.For(
                    elements : seq<'a>,
                    forBody : ('a -> StatefulFunc<'state, unit>)
                   ) =
                   
        StatefulFunc(fun initialState ->
            let state = ref initialState
            
            for e in elements do
                let (), updatedState = Run (forBody e) (!state)
                state := updatedState
            
            // Return unit * finalSTate
            (), !state
        )
        
    member this.Return(x : 'a) =
        StatefulFunc(fun initialState -> x, initialState)
    
    member this.Using<'a, 'state, 'b when 'a :> IDisposable>
                    (
                      x : 'a,
                      restOfComputation : 'a -> StatefulFunc<'state, 'b>
                    ) =
        
        StatefulFunc(fun initialState ->
            try
                Run (restOfComputation x) initialState
            finally
                x.Dispose()
        )
        
    member this.TryFinally(
                            tryBlock : StatefulFunc<'state, 'a>,
                            finallyBlock : unit -> unit
                          ) =
                          
        StatefulFunc(fun initialState ->
            try
                Run tryBlock initialState
            finally
                finallyBlock()
        )
    
    member this.TryWith(
                        tryBlock : StatefulFunc<'state, 'a>,
                        exnHandler : exn -> StatefulFunc<'state, 'a>
                        ) =
        
        StatefulFunc(fun initialState ->
            try
                Run tryBlock initialState
            with
            | e ->
                Run (exnHandler e) initialState
        )
        
    member this.While(
                        predicate : unit -> bool,
                        body : StatefulFunc<'state, unit>
                     ) =
                     
        StatefulFunc(fun initialState ->
        
            let state = ref initialState
            while predicate() = true do
                let (), updatedState = Run body (!state)
                state := updatedState
                
            // Return unit * finalState
            (), !state
        )
    
    member this.Zero() =
        StatefulFunc(fun initialState -> (), initialState)

// Declare the state workflow builder
let state = StateBuilder()

// Primitive functions for getting and setting state
let GetState          = StatefulFunc (fun state -> state, state)
let SetState newState = StatefulFunc (fun prevState -> (), newState)


