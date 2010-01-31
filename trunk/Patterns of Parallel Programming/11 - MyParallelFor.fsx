open System
open System.Collections.Generic
open System.Threading

let messages = new Queue<string>()
let addMsg msg =
    lock messages
        (fun () -> messages.Enqueue(msg))

let myParallelFor (inclusiveLowerBound : int) exclusiveUpperBound body =
    // Get the number of processors, initialize the number of remaining
    // threads, and set the starting point for the iteration
    let numProcs = Environment.ProcessorCount
    printfn "%d processors found" numProcs
    let remainingWorkItems = ref numProcs
    let nextIteration = ref inclusiveLowerBound
    
    use mre = new ManualResetEvent(false)
    for p = 0 to numProcs - 1 do
        ThreadPool.QueueUserWorkItem(fun _ ->
            let index = ref (Interlocked.Increment(nextIteration) - 1)
            while !index < exclusiveUpperBound do
                body(!index)
                index := Interlocked.Increment(nextIteration) - 1
            if Interlocked.Decrement(remainingWorkItems) = 0 then mre.Set() |> ignore)
        |> ignore
    mre.WaitOne()

myParallelFor 0 10 (fun i -> addMsg (sprintf "%d : {thread %-2d}" i Thread.CurrentThread.ManagedThreadId))
for msg in messages do
    printfn "%s" msg