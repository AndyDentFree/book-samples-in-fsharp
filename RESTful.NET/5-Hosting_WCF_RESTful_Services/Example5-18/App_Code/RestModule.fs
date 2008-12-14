#light
namespace AspNetCompatibility
open System.Web

type RestModule() =
    interface IHttpModule with
        member this.Dispose() = ()
        
        member this.Init(app : HttpApplication) =
            app.BeginRequest.Add(fun e ->
                let ctx = HttpContext.Current
                let path = ctx.Request.AppRelativeCurrentExecutionFilePath
                
                let i = path.IndexOf('/', 2)
                if i > 0 then
                    let svc = path.Substring(0, i) + ".svc"
                    let rest = path.Substring(i, path.Length - i)
                    let qs = ctx.Request.QueryString.ToString()
                    ctx.RewritePath(svc, rest, qs, false))