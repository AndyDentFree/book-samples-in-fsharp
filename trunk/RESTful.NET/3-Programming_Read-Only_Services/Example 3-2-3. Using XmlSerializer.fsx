#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.Xml.Serialization
open System.ServiceModel
open System.ServiceModel.Web

[<XmlRoot(Namespace = "", ElementName = "Domain")>]
type Domain =
    { [<XmlAttribute(AttributeName = "name")>] mutable Name : string;
      [<XmlAttribute(AttributeName = "uri")>] mutable Uri : string }


[<XmlRoot(Namespace = "", ElementName = "Domains")>]
type DomainList() =
    inherit ResizeArray<Domain>()


[<ServiceContract>]
type IBioTaxService =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    [<XmlSerializerFormat()>]
    abstract GetRoot : unit -> DomainList
    

type BioTaxService() =
    interface IBioTaxService with
        member this.GetRoot() =
            let ret = new DomainList()
            [| "Archaea"; "Eubacteria"; "Eukaryota" |]
            |> Seq.iter (fun domain ->
                ret.Add({ Name = domain; Uri = domain }))
            ret


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<BioTaxService>, baseAddresses)
sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "http://localhost:8889")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
