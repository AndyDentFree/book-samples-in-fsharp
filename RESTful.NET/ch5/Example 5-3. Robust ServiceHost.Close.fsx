#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Description


[<ServiceContract>]
type HostingExample() =
    [<WebGet(UriTemplate = "*")>]
    [<OperationContract>]
    member this.Method1() =
        "String Result"


let baseAddresses : Uri[] = [| |]
let sh = new ServiceHost(typeof<HostingExample>, baseAddresses)
let mutable openSucceeded = false

try
    let se = sh.AddServiceEndpoint(typeof<HostingExample>,
                                   new WebHttpBinding(), 
                                   "http://localhost:8080/Hosting")
    se.Behaviors.Add(new WebHttpBehavior())
    try
        sh.Open()
        openSucceeded <- true
    with ex ->
        printfn "ServiceHost failed to open %s" (ex.ToString())
finally
    if not openSucceeded then sh.Abort()

if openSucceeded then
    printfn "Service is running..."
    Console.ReadLine() |> ignore
    
    // Robust Close Example
    let mutable closeSucceeded = false
    try 
        try
            sh.Close()
            closeSucceeded <- true
        with ex ->
            printfn "ServiceHost failed to close %s" (ex.ToString())
    finally
        if not closeSucceeded then sh.Abort()
else
    printfn "Service failed to open"
