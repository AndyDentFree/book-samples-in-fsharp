#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
#r "System.Runtime.Serialization"
open System
open System.Runtime.Serialization
open System.ServiceModel
open System.ServiceModel.Web


[<DataContract(Name = "user", Namespace = "")>]
type User =
    { [<DataMember(Name = "id", Order = 1)>]
      mutable UserId : string;
      
      [<DataMember(Name = "firstname", Order = 2)>]
      mutable FirstName : string;
      
      [<DataMember(Name = "lastname", Order = 3)>]
      mutable LastName : string;
      
      [<DataMember(Name = "email", Order = 4)>]
      mutable Email : string } with
        static member Default =
            { UserId = ""; FirstName = ""; LastName = ""; Email = "" }


[<CollectionDataContract(Name = "users", Namespace = "")>]
type Users() =
    inherit ResizeArray<User>()


[<AutoOpen>]
module Services =
    let users = new Users()
    
    [<ServiceContract>]
    type UserService() =
        [<WebGet(UriTemplate = "/users")>]
        [<OperationContract>]
        member this.GetAllUsers() =
            users
        
        [<WebInvoke(UriTemplate = "/users", Method = "POST")>]
        [<OperationContract>]
        member this.AddNewUser(u : User) =
            u.UserId <- string (Guid.NewGuid())
            users.Add(u)
            u
        
        [<WebGet(UriTemplate = "/users/{userId}")>]
        [<OperationContract>]
        member this.GetUser(userId) =
            let u = 
                users
                |> Seq.tryfind (fun x -> x.UserId = userId)
            match u with
            | Some user -> user
            | None      -> User.Default
            
        [<WebInvoke(UriTemplate = "/users/{userId}", Method = "PUT")>]
        [<OperationContract>]
        member this.UpdateUser(userId, update : User) =
            let user = this.GetUser(userId)
            user.FirstName <- update.FirstName
            user.LastName <- update.LastName
            user.Email <- update.Email

        [<WebInvoke(UriTemplate = "/users/{userId}", Method = "DELETE")>]
        [<OperationContract>]
        member this.DeleteUser(userId) =
            let user = this.GetUser(userId)
            users.Remove(user) |> ignore


let binding = new WebHttpBinding()
let baseAddresses : Uri[] = [| |]
let sh = new WebServiceHost(typeof<UserService>, baseAddresses)
sh.AddServiceEndpoint(typeof<UserService>, binding, "http://localhost:8889")
sh.Open()
printfn "Read/Write HTTP Service Listening"
printfn "Press enter to stop service"
Console.ReadLine()
