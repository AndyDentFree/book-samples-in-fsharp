open System
open System.Collections.Generic
open System.Threading

let messages = new Queue<string>()
let addMsg msg =
    lock messages
        (fun () -> messages.Enqueue(msg))

let myParallelFor inclusiveLowerBound exclusiveUpperBound body =
    let size = exclusiveUpperBound - inclusiveLowerBound
    let numProcs = Environment.ProcessorCount
    printfn "%d processors found" numProcs
    let range = size / numProcs
    
    let threads = new List<Thread>(numProcs)
    for p = 0 to (numProcs - 1) do
        let start = p * range + inclusiveLowerBound
        let end' = 
            if p = (numProcs - 1)
                then exclusiveUpperBound
                else start + range
        threads.Add(new Thread(fun () ->
            for i = start to (end' - 1) do body(i)))
    
    threads
    |> Seq.iter (fun t -> t.Start())
    threads
    |> Seq.iter (fun t -> t.Join())

myParallelFor 0 10 (fun i -> addMsg (sprintf "%d : {thread %-2d}" i Thread.CurrentThread.ManagedThreadId))
for msg in messages do
    printfn "%s" msg