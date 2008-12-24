#light
#r "System.ServiceModel"
open System
open System.ServiceModel
open System.ServiceModel.Description


[<ServiceContract>]
type IMyContract =
    [<OperationContract>]
    abstract MyMethod : unit -> unit


type MyService() =
    interface IMyContract with
        member this.MyMethod() =
            printfn "MyService.MyMethod()"


let baseAddresses : Uri[] = [| |]
let host = new ServiceHost(typeof<MyService>, baseAddresses)

let wsBinding = new WSHttpBinding()
let tcpBinding = new NetTcpBinding()

host.AddServiceEndpoint(typeof<IMyContract>, wsBinding, "http://localhost:8000/MyService")
host.AddServiceEndpoint(typeof<IMyContract>, tcpBinding, "net.tcp://localhost:8001/MyService")
host.AddServiceEndpoint(typeof<IMyContract>, tcpBinding, "net.tcp://localhost:8002/MyService")

host.Open()
printfn "Service Opened, press any key to end..."
Console.ReadKey(true) |> ignore
host.Close()