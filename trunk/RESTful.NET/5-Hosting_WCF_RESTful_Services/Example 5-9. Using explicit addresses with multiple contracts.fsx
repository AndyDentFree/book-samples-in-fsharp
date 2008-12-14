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

let baseAddress : Uri[] = [| |]
let sh = new WebServiceHost(typeof<ServiceType>, baseAddress)
sh.AddServiceEndpoint(typeof<IWebOne>,
                      new WebHttpBinding(),
                      "http://localhost:8080/Hosting/webone")
sh.AddServiceEndpoint(typeof<IWebTwo>,
                      new WebHttpBinding(),
                      "http://localhost:8080/Hosting/webtwo")                      
sh.Open()
printfn "Service is running...\nVisit the following URI's in your browser for the output:"
printfn "  http://localhost:8080/Hosting/webone/conflict"
printfn "  http://localhost:8080/Hosting/webtwo/conflict"
Console.ReadLine() |> ignore
sh.Close()
