#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.IO
open System.Runtime.Serialization
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Channels


[<DataContract(Name = "Domain")>]
type Domain(name : string, uri : string) =
    let mutable name' = name
    let mutable uri' = uri
    
    [<DataMember>]
    member this.Name 
        with get() = name'
        and set v = name' <- v
    
    [<DataMember>]
    member this.Uri
        with get() = uri'
        and set v = uri' <- v


[<CollectionDataContract(Name = "Domains")>]
type DomainList() =
    inherit ResizeArray<Domain>()


[<ServiceContract>]
type IBioTaxService =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    abstract GetRoot : unit -> DomainList
    

type BioTaxService() =
    interface IBioTaxService with
        member this.GetRoot() =
            let ret = new DomainList()
            [| "Archaea"; "Eubacteria"; "Eukaryota" |]
            |> Seq.iter (fun domain ->
                ret.Add(Domain(domain, domain)))
            ret


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<BioTaxService>, baseAddresses)
sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "http://localhost:8889")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
