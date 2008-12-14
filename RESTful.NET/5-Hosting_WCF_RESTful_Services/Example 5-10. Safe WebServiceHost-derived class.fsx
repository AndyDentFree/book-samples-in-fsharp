#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Description


type SafeCloseWebServiceHost<'T>(baseAddresses : Uri[]) =
    inherit WebServiceHost(typeof<'T>, baseAddresses)
    
    member this.SafeOpen() =
        let mutable openSucceeded = false
        try
            try
                this.Open()
                openSucceeded <- true
            with ex ->
                printfn "ServiceHost failed to open %s" (ex.ToString())
        finally
            if not openSucceeded then this.Abort()
        
        if this.State = CommunicationState.Opened then
            printfn "Service is running..."
            Console.ReadLine() |> ignore
        else
            printfn "Service failed to open"
        
        openSucceeded
        
    member this.SafeClose() =
        let mutable closeSucceeded = false
        try
            try
                this.Close()
                closeSucceeded <- true
            with ex ->
                printfn "ServiceHost failed to close nicely %s" (ex.ToString())
        finally
            if not closeSucceeded then this.Abort()
            
        closeSucceeded


[<ServiceContract>]
type HostingExample() =
    [<WebGet(UriTemplate = "*")>]
    [<OperationContract>]
    member this.Method1() =
        "String Result"
        

let baseAddress = [| new Uri("http://localhost:8080/Hosting") |]
let sh = new SafeCloseWebServiceHost<HostingExample>(baseAddress)
if sh.SafeOpen() then
    printfn "Service is running..."
    Console.ReadLine() |> ignore
    sh.SafeClose() |> ignore
else
    printfn "Service failed to run..."
    