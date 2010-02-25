open System
open System.Threading

let threadBody() =
    for i in 1 .. 5 do
        Thread.Sleep(100)
        printfn "[Thread %d] %d..."
            Thread.CurrentThread.ManagedThreadId
            i

let spawnThread() =
    let thread = new Thread(threadBody)
    thread.Start()

spawnThread()
spawnThread()

Console.ReadKey()
