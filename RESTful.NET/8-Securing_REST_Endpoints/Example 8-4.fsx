#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.ServiceModel
open System.ServiceModel.Web
open System.Security.Permissions


// Example 8-1. Secure Service
[<ServiceContract(Namespace = "")>]
type SecureService() =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/")>]
    [<OperationBehavior(Impersonation = ImpersonationOption.Required)>]
    [<PrincipalPermission(SecurityAction.PermitOnly, Role = "Administrators")>]
    member this.AuthType() =
        let securityCtx = OperationContext.Current.ServiceSecurityContext
        let authType = "No security context."
        
        match securityCtx with
        | null -> "No security context."
        | _    ->
            if securityCtx.IsAnonymous
                then "Anonymous"
                else securityCtx.PrimaryIdentity.Name
        

let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<SecureService>, baseAddresses)
let uri = "http://localhost/wcfrestsecoiis/"
let wb = new WebHttpBinding() // This binding doesn't work for this example but the book doesn't go into which binding to use
wb.Security.Mode = WebHttpSecurityMode.TransportCredentialOnly
wb.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic
sh.AddServiceEndpoint(typeof<SecureService>, wb, uri)
sh.Open()
printfn "Service running"
Console.ReadLine() |> ignore
