#light
#r "System.ServiceModel.Web"
open System
open System.Collections.Generic

// From Example 2-11
let makeTemplates (templateStrings : string seq) =
    let templates = new Dictionary<UriTemplate, obj>()
    
    for template in templateStrings do
        let uriTemplate = new UriTemplate(template)
        let mutable segment = "ROOT"
        if uriTemplate.PathSegmentVariableNames.Count > 0 then
            let lastPathSegment = uriTemplate.PathSegmentVariableNames.Count - 1
            segment <- uriTemplate.PathSegmentVariableNames.[lastPathSegment]
        let msg = segment + " MATCH!"
        templates.Add(uriTemplate, msg)
        
    templates

let stemplates = [|
    "/";
    "/{Domain}";
    "/{Domain}/{Kingdom}";
    "/{Domain}/{Kingdom}/{Phylum}";
    "/{Domain}/{Kingdom}/{Phylum}/{Class}";
    "/{Domain}/{Kingdom}/{Phylum}/{Class}/{Order}";
    "/{Domain}/{Kingdom}/{Phylum}/{Class}/{Order}/{Family}";
    "/{Domain}/{Kingdom}/{Phylum}/{Class}/{Order}/{Family}/{Genus}";
    "/{Domain}/{Kingdom}/{Phylum}/{Class}/{Order}/{Family}/{Genus}/{Species}" |]
let templates = makeTemplates stemplates
let baseUri = new Uri("http://example.org")

// create the UriTemplateTable
let tt = new UriTemplateTable(baseUri)

// add all the UriTemplate/Value pairs to it
for kvp in templates do
    tt.KeyValuePairs.Add(kvp)

while true do
    printfn "type in a Uri to test ('Q' to exit)"
    let uri = Console.ReadLine()
    if uri = "Q" then exit 1
    
    let testUri = new Uri(uri)
    let match' = tt.MatchSingle(testUri)
    if match' <> null then
        printfn "%A" match'.Data
    else
        printfn "No match found!"
    