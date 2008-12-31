#light
open System
open System.Runtime.Serialization
open System.ServiceModel
open System.ServiceModel.Description
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
    [<WebGet>]
    abstract GetRoot : unit -> DomainList
    
    [<OperationContract>]
    [<WebGet>]
    abstract GetDomain : domain : string -> KingdomList


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
        
        
let uri : Uri[] = [| |]
let binding = new WebHttpBinding()
let sh = new WebServiceHost(typeof<BioTaxService>, uri)
let se = sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "http://localhost/webtest/")
se.Behaviors.Add(new WebScriptEnablingBehavior())
sh.Open()
for ep in sh.Description.Endpoints do
    printfn "%A" ep.ListenUri
        

printfn "Service started...\nPress any key to end..."
Console.ReadKey(true) |> ignore
sh.Close()
