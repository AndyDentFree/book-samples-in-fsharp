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
let se = sh.AddServiceEndpoint(typeof<HostingExample>,
                               new WebHttpBinding(), 
                               "http://localhost:8080/Hosting")
se.Behaviors.Add(new WebHttpBehavior())
sh.Open()

printfn "Service is running..."
Console.ReadLine() |> ignore
sh.Close()
