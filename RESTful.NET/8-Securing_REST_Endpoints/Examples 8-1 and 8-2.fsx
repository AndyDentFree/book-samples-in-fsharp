#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.ServiceModel
open System.ServiceModel.Web


// Example 8-1. Secure Service
[<ServiceContract(Namespace = "")>]
type SecureService() =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    member this.AuthType() =
        let securityCtx = OperationContext.Current.ServiceSecurityContext
        match securityCtx with
        | null -> "No security context."
        | _    ->
            if securityCtx.IsAnonymous
                then "Anonymous"
                else securityCtx.PrimaryIdentity.Name
        

// Example 8-2. Hosting and adding an endpoint for SecureService
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<SecureService>, baseAddresses)
let uri = "http://localhost/wcfrestsecoiis/"
let wb = new WebHttpBinding()
sh.AddServiceEndpoint(typeof<SecureService>, wb, uri)
sh.Open()
printfn "Service running"
Console.ReadLine() |> ignore
