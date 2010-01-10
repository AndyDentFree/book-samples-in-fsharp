open System
open System.Collections.Generic
open System.Threading

let messages = new Queue<string>()
let addMsg msg =
    lock messages
        (fun () -> messages.Enqueue(msg))

let myParallelFor inclusiveLowerBound exclusiveUpperBound body =
    // Determine the number of iterations to be processed, the number of
    // cores to use, and the approximate number of iterations to process in
    // each thread
    let size = exclusiveUpperBound - inclusiveLowerBound
    let numProcs = Environment.ProcessorCount
    printfn "%d processors found" numProcs
    let range = size / numProcs
    
    // Keep track of number of threads remaining to complete
    let remaining = ref numProcs
    use mre = new ManualResetEvent(false)
    for p = 0 to (numProcs - 1) do
        let start = p * range + inclusiveLowerBound
        let end' = 
            if p = (numProcs - 1)
                then exclusiveUpperBound
                else start + range
        ThreadPool.QueueUserWorkItem(fun _ ->
            for i = start to (end' - 1) do body(i)
            if Interlocked.Decrement(remaining) = 0 then mre.Set() |> ignore)
        |> ignore
    
    // Wait for all threads to complete
    mre.WaitOne()

myParallelFor 0 10 (fun i -> addMsg (sprintf "%d : {thread %-2d}" i Thread.CurrentThread.ManagedThreadId))
for msg in messages do
    printfn "%s" msg