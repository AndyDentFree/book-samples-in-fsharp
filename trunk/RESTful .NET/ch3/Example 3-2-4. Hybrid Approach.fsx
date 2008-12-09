#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.Runtime.Serialization
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Channels


[<DataContract(Name = "Domain")>]
type Domain =
    { [<DataMember>] mutable Name : string;
      [<DataMember>] mutable Uri : string }


[<CollectionDataContract(Name = "Domains")>]
type DomainList() =
    inherit ResizeArray<Domain>()


[<ServiceContract>]
type IBioTaxService =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    abstract GetRoot : unit -> Message
    

type BioTaxService() =
    interface IBioTaxService with
        member this.GetRoot() =
            let ret = new DomainList()
            [| "Archaea"; "Eubacteria"; "Eukaryota" |]
            |> Seq.iter (fun domain ->
                ret.Add({ Name = domain; Uri = domain }))
            Message.CreateMessage(MessageVersion.None, "*", ret)


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<BioTaxService>, baseAddresses)
sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "http://localhost:8889")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
