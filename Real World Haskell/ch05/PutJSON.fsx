#load "SimpleJSON.fsx"
open SimpleJSON

let show x = sprintf "%A" x

let intercalate s xs = System.String.Join(s, List.to_array xs)

let rec renderJValue = function
    | JString s   -> show s
    | JNumber n   -> show n
    | JBool true  -> "true"
    | JBool false -> "false"
    | JNull       -> "null"
    | JObject o   ->
        let renderPair (k,v) = show k ^ ": " ^ renderJValue v
        let pairs = function
            | [] -> ""
            | ps -> intercalate ", " (List.map renderPair ps)
        "{" ^ pairs o ^ "}"
    | JArray a    ->
        let values = function
            | [] -> ""
            | vs -> intercalate ", " (List.map renderJValue vs)
        "[" ^ values a ^ "]"

let putValue v = printfn "%A" (renderJValue v)
