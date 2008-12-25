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

let createLinkForItem (item : SyndicationItem) =
    let d = item.PublishDate
    let theUri = sprintf "/%d/%d/%d/%s" d.Year d.Month d.Day item.Title.Text
    new Uri(theUri, UriKind.Relative)

let mutable item : SyndicationItem = null
let mutable theLink : SyndicationLink = null

for i in [1..10] do
    item <- new SyndicationItem(
        Title = new TextSyndicationContent("Blog entry #" + string i),
        Content = new TextSyndicationContent("This is the content of the blog entry numbered " + string i),
        PublishDate = DateTimeOffset.Now,
        LastUpdatedTime = DateTimeOffset.Now,
        Id = "urn:uuid:" + Guid.NewGuid().ToString())
    theLink <- new SyndicationLink(createLinkForItem item)
    item.Links.Add(theLink)

let feed = new SyndicationFeed()
feed.Items <- [ item ]
let formatter = new Atom10FeedFormatter(feed)
let atomMs = new MemoryStream()
let xw = XmlWriter.Create(atomMs, new XmlWriterSettings(Indent = true))
formatter.WriteTo(xw)
xw.Close()
let reader = new StreamReader(atomMs)
atomMs.Position <- 0L
printfn "%s" (reader.ReadToEnd())
