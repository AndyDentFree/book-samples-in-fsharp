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
    [<OperationContract()>]
    [<WebGet(UriTemplate = "*")>]
    member this.AllURIs(msg : Message) =
        let webCtx = WebOperationContext.Current
        let incomingCtx = webCtx.IncomingRequest
        let uri = incomingCtx.UriTemplateMatch.RequestUri.ToString()
        printfn "Request to %s" uri
        
        if incomingCtx.Method <> "GET" then
            printfn "Incoming Message %s with method of %s" (msg.GetReaderAtBodyContents().ReadOuterXml()) incomingCtx.Method
        else
            printfn "GET Request - no message Body"
        
        let query = incomingCtx.UriTemplateMatch.QueryParameters
        if query.Count > 0 then
            printfn "QueryString:"
            for n in query do
                printfn "%A = %A" n query.[n]

        let response = Message.CreateMessage(MessageVersion.None, "*", "Simple response string")
        let outCtx = webCtx.OutgoingResponse
        outCtx.Headers.Add("CustomHeader", "Value")
        response        


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<SimpleHTTPService>, baseAddresses)
sh.AddServiceEndpoint(typeof<SimpleHTTPService>, binding, "http://localhost:8889/TestHttp")
sh.Open()
printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
