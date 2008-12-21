#light
#r "System.ServiceModel"
#r "System.ServiceModel.Web"
open System
open System.IO
open System.Xml
open System.Diagnostics
open System.Collections.Generic
open System.ServiceModel
open System.ServiceModel.Web
open System.ServiceModel.Syndication

[<ServiceContract>]
type IEventLogFeed =
    [<OperationContract>]
    [<WebGet(UriTemplate = "/{log}/feed.rss")>]
    abstract GetRSS : log : string -> Rss20FeedFormatter
    
    [<OperationContract>]
    [<WebGet(UriTemplate = "/{log}/feed.atom")>]
    abstract GetAtom: log : string -> Atom10FeedFormatter    


let getFeed log =
    let el = new EventLog(log)
    let feed = new SyndicationFeed()
    feed.Title <- new TextSyndicationContent(sprintf "%s %s EventLog Feed" Environment.MachineName el.Log)
    feed.Description <- new TextSyndicationContent("A feed of data from the EventLog")
    feed.Authors.Add(new SyndicationPerson(Name = Environment.MachineName))
    feed.Id <- "urn:uuid" + Environment.MachineName + el.Log

    let items = new List<SyndicationItem>()
    feed.Items <- items

    for e in el.Entries do
        if e.TimeGenerated > DateTime.Now.AddDays(-1.0) then
            items.Add(new SyndicationItem(
                        Title = new TextSyndicationContent(sprintf "%s:%s:%A" e.Source e.Category e.EntryType),
                        Content = new TextSyndicationContent(e.Message),
                        PublishDate = new DateTimeOffset(e.TimeGenerated),
                        LastUpdatedTime = new DateTimeOffset(e.TimeGenerated),
                        Id = "urn:uuid" + string e.Index))
    feed
    

type EventLogFeed() =
    interface IEventLogFeed with
        member this.GetRSS(log) =
            let feed = getFeed log
            new Rss20FeedFormatter(feed)
        
        member this.GetAtom(log) =
            let feed = getFeed log
            new Atom10FeedFormatter(feed)


let baseAddress = [| new Uri("http://localhost:8080/EventLogFeed") |]
let sh = new WebServiceHost(typeof<EventLogFeed>, baseAddress)

sh.Open()
for ep in sh.Description.Endpoints do
    printfn "%A" ep.ListenUri
printfn "Service is running..."
Console.ReadLine() |> ignore
sh.Close()
