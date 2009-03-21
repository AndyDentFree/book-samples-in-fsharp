#light
#r "System.ServiceModel"
#r "System.IdentityModel"
open System.ServiceModel
open System.ServiceModel.Channels


type RESTServiceAuthorizationManager() =
    inherit ServiceAuthorizationManager()
    
    override this.CheckAccessCore(operationContext) =
        let msg = operationContext.RequestContext.RequestMessage
        let uri = msg.Properties.Via.AbsoluteUri
        let http = msg.Properties.[HttpRequestMessageProperty.Name] :?> HttpRequestMessageProperty
        printfn "CheckAccessCore"
        printfn "Resource: %s part of uniform interface: %s" uri http.Method
        
        base.CheckAccessCore(operationContext)
