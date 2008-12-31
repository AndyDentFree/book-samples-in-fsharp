#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.Runtime.Serialization
open System.ServiceModel
open System.ServiceModel.Web


[<DataContract(Name = "Domain")>]
type Domain =
    { [<DataMember>] mutable Name : string;
      [<DataMember>] mutable Uri : string }


[<DataContract(Name = "Kingdom")>]
type Kingdom =
    { [<DataMember>] mutable Name : string;
      [<DataMember>] mutable Uri : string }


[<CollectionDataContract(Name = "Domains")>]
type DomainList() =
    inherit ResizeArray<Domain>()


[<CollectionDataContract(Name = "Kingdoms")>]
type KingdomList() =
    inherit ResizeArray<Kingdom>()
    

[<ServiceContract>]
type IBioTaxService =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Xml)>]
    abstract GetRoot : unit -> DomainList
    
    [<OperationContract>]
    [<WebGet(UriTemplate = "/{domain}", ResponseFormat = WebMessageFormat.Xml)>]
    abstract GetDomain : domain : string -> KingdomList

    [<OperationContract>]
    [<WebGet(UriTemplate = "/json", ResponseFormat = WebMessageFormat.Json)>]
    abstract GetRootJSON : unit -> DomainList
    
    [<OperationContract>]
    [<WebGet(UriTemplate = "/{domain}/json", ResponseFormat = WebMessageFormat.Json)>]
    abstract GetDomainJSON : domain : string -> KingdomList
        

type BioTaxService() =
    interface IBioTaxService with
        member this.GetRoot() =
            let ret = new DomainList()
            [| "Archaea"; "Eubacteria"; "Eukaryota" |]
            |> Seq.iter (fun domain ->
                ret.Add({ Name = domain; Uri = domain }))
            ret
        
        member this.GetDomain(domain) =
            let ret = new KingdomList()
            for i in [1..5] do
                ret.Add({ Name = domain + string i; Uri = domain + string i })
            ret
        
        member this.GetRootJSON() = (this :> IBioTaxService).GetRoot()
        
        member this.GetDomainJSON(domain) = (this :> IBioTaxService).GetDomain(domain)


let binding = new WebHttpBinding()
let uri = new Uri("http://localhost/BioService")
let sh = new WebServiceHost(typeof<BioTaxService>, [| uri |])
sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
