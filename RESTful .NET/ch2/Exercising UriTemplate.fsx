#light
#r "System.ServiceModel.Web"
open System

let baseUri = new Uri("http://example.org")
let template = new UriTemplate("/{Domain}/{Kingdom}/{Phylum}/{Class}/{Order}/{Family}/{Genus}/{Species}")

printfn "URI path segments are:"
template.PathSegmentVariableNames
|> Seq.iter (fun pathSeg -> printfn "  %s" pathSeg)

printfn "\ntype in a URI to test"
let uri = Console.ReadLine()
let testUri = new Uri(uri)
let templateMatch = template.Match(baseUri, testUri)

match templateMatch with
| null -> printfn "URI not a match"
| _    ->
    let bound = templateMatch.BoundVariables
    for key in bound.Keys do
        printfn "%A = %A" key bound.[key]
