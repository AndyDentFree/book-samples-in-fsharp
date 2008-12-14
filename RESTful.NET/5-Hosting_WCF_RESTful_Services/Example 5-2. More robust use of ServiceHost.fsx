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

// flag to check if call to Open succeeded
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
    // call Abort since the object will be in the Faulted state
    if not openSucceeded then sh.Abort()

if openSucceeded then
    printfn "Service is running..."
    Console.ReadLine() |> ignore
    sh.Close()
else
    printfn "Service failed to open"
