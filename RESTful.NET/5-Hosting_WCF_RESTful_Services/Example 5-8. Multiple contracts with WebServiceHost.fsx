#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Description


[<ServiceContract>]
type IWebOne =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/conflict")>]
    abstract One : unit -> string


[<ServiceContract>]
type IWebTwo =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/conflict")>]
    abstract Two : unit -> string


type ServiceType() =
    interface IWebOne with
        member this.One() =
            "One"

    interface IWebTwo with
        member this.Two() =
            "Two"

let baseAddress = [| new Uri("http://localhost:8080/Hosting") |]
let sh = new WebServiceHost(typeof<ServiceType>, baseAddress)
sh.Open()
printfn "Service is running..."
Console.ReadLine() |> ignore
sh.Close()
