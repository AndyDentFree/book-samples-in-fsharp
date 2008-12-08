#light
#r "System.ServiceModel"
#r "System.Runtime.Serialization"
open System
open System.ServiceModel
open System.ServiceModel.Channels


[<ServiceContract>]
type SimpleHTTPService() =
    [<OperationContract(Action = "*", ReplyAction = "*")>]
    member this.AllURIs(msg : Message) =
        let rqPropName = HttpRequestMessageProperty.Name
        let rpPropName = HttpResponseMessageProperty.Name
        let msgProps = msg.Properties.[rqPropName] :?> HttpRequestMessageProperty
        
        let uri = msg.Headers.To.AbsolutePath
        printfn "Request to %s" uri
        
        if msgProps.Method <> "GET" then
            printfn "Incoming Message %s with method of %s" (msg.GetReaderAtBodyContents().ReadOuterXml()) msgProps.Method
        else
            printfn "GET Request - no message Body"
        
        if msgProps.QueryString <> null then
            printfn "QueryString = %s" msgProps.QueryString
        
        let rp = Message.CreateMessage(MessageVersion.None, "*", "Simple Response String")
        let rpProps = new HttpResponseMessageProperty()
        rpProps.Headers.Add("CustomHeader", "Value")
        // The book did not include the following line but the header is
        //   not added to the response without it
        rp.Properties.[rpPropName] <- rpProps
        rp


let b = new CustomBinding()
let encoder = new TextMessageEncodingBindingElement(MessageVersion = MessageVersion.None)
b.Elements.Add(encoder)
let binding = new HttpTransportBindingElement()
b.Elements.Add(binding)
let baseAddresses : Uri[] = [| |]
let host = new ServiceHost(typeof<SimpleHTTPService>, baseAddresses)
host.AddServiceEndpoint(typeof<SimpleHTTPService>, b, "http://localhost:8889/TestHTTP")
host.Open()

printfn "Simple HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine() |> ignore



