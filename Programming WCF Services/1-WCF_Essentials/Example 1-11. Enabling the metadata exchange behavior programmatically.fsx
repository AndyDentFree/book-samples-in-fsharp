#light
#r "System.ServiceModel"
open System
open System.ServiceModel
open System.ServiceModel.Description


[<ServiceContract>]
type MyService() =
    [<OperationContract>]
    member this.MyMethod() =
        printfn "MyService.MyMethod()"


let baseAddresses : Uri[] = [| new Uri("http://localhost:8080") |]
let host = new ServiceHost(typeof<MyService>, baseAddresses)
host.AddServiceEndpoint(typeof<MyService>, new BasicHttpBinding(), "")

let metadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>()
if metadataBehavior = null then
    let metadataBehavior = new ServiceMetadataBehavior()
    metadataBehavior.HttpGetEnabled <- true
    host.Description.Behaviors.Add(metadataBehavior)

host.Open()
printfn "Open the following URL in a browser: http://localhost:8080\nPress any key to end..."
Console.ReadKey(true) |> ignore
host.Close()