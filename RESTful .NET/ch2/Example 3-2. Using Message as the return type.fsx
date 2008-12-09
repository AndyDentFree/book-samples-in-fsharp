#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.IO
open System.Xml
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Channels


[<ServiceContract>]
type IBioTaxService =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    abstract GetRoot : unit -> Message
    

type BioTaxService() =
    interface IBioTaxService with
        member this.GetRoot() =
            let ms = new MemoryStream()
            let xw = XmlDictionaryWriter.CreateTextWriter(ms)
            xw.WriteStartDocument()
            xw.WriteStartElement("Domains")
            [| "Archaea"; "Eubacteria"; "Eukaryota" |]
            |> Seq.iter (fun domain ->
                xw.WriteStartElement("Domain")
                xw.WriteAttributeString("name", domain)
                xw.WriteAttributeString("uri", domain)
                xw.WriteEndElement())
            xw.WriteEndElement()
            xw.WriteEndDocument()
            xw.Flush()
            ms.Position <- 0L
            
            let xdr = XmlDictionaryReader.CreateTextReader(ms, XmlDictionaryReaderQuotas.Max)
            Message.CreateMessage(MessageVersion.None, "*", xdr)


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<BioTaxService>, baseAddresses)
sh.AddServiceEndpoint(typeof<IBioTaxService>, binding, "http://localhost:8889")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
