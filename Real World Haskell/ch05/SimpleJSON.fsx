let truncate (n : double) = System.Math.Truncate(n)

type JValue = JString of string
            | JNumber of double
            | JBool of bool
            | JNull
            | JObject of (string * JValue) list
            | JArray of JValue list

let getString = function
    | JString s -> Some s
    | _         -> None

let getInt = function
    | JNumber n -> Some (truncate n)
    | _         -> None    
    
let getDouble = function
    | JNumber n -> Some n
    | _         -> None

let getBool = function
    | JBool b -> Some b
    | _       -> None

let getObject = function
    | JObject o -> Some o
    | _         -> None

let getArray = function
    | JArray a -> Some a
    | _        -> None

let isNull v = v = JNull
