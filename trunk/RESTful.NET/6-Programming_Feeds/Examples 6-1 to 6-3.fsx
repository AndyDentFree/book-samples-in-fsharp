#light
#r "System.ServiceModel.Web"
open System
open System.IO
open System.Xml
open System.Diagnostics
open System.Collections.Generic
open System.ServiceModel.Syndication


// Example 5-1. Creating an EventLog Feed
let el = new EventLog("Application")
let feed = new SyndicationFeed()
feed.Title <- new TextSyndicationContent(sprintf "%s %s EventLog Feed" Environment.MachineName el.Log)
feed.Description <- new TextSyndicationContent("A feed of data from the EventLog")
feed.Authors.Add(new SyndicationPerson(Name = Environment.MachineName))
feed.Id <- "urn:uuid" + Environment.MachineName + el.Log

// Example 5-2. Defining and populating a List of SyndicationItem
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

// Example 6-3. Formatting a feed with SyndicationFeedFormatter
let formatter = new Atom10FeedFormatter(feed)
let atomMs = new MemoryStream()
let xw = XmlWriter.Create(atomMs)
formatter.WriteTo(xw)
xw.Close()
let reader = new StreamReader(atomMs)
atomMs.Position <- 0L
printfn "%s" (reader.ReadToEnd())



