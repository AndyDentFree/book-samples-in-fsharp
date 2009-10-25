#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Channels


[<ServiceContract>]
type SimpleHTTPService() =
    [<OperationContract(Action = "*", ReplyAction = "*")>]
    member this.AllURIs(msg : Message) =
        "Simple Response String"

let binding = new WebHttpBinding()
let host = new WebServiceHost(typeof<SimpleHTTPService>)
host.AddServiceEndpoint(typeof<SimpleHTTPService>, binding, "http://localhost:8889/TestHttp")
host.Open()

printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine() |> ignore
