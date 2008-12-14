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


let baseAddresses : Uri[] = [| new Uri("http://localhost:8080") |]
let sh = new ServiceHost(typeof<HostingExample>, baseAddresses)
let se = sh.AddServiceEndpoint(typeof<HostingExample>,
                               new WebHttpBinding(), 
                               "Hosting")
se.Behaviors.Add(new WebHttpBehavior())
sh.Open()
printfn "Service is running..."
Console.ReadLine() |> ignore
sh.Close()
