#light
namespace SimpleWebHostingIIS
open System
open System.Web
open System.Diagnostics
open System.ServiceModel.Web
open System.ServiceModel.Activation

type EventHandlingServiceHostFactory() =
    inherit WebServiceHostFactory()
    
    override this.CreateServiceHost(constructorString : string, baseAddresses : Uri[]) =
        let sh = base.CreateServiceHost(constructorString, baseAddresses)
        let wsh = sh :?> WebServiceHost
        wsh.Opened.Add(fun e ->
            Debug.WriteLine("WebServiceHost opened!"))
        wsh.Closed.Add(fun e ->
            Debug.WriteLine("WebServiceHost closed!"))
        sh
        