type Doc = Empty
         | Char of char
         | Text of string
         | Line
         | Concat of Doc * Doc
         | Union of Doc * Doc

let empty = Empty

let char c = Char c

let text = function
    | "" -> Empty
    | s  -> Text s

let double (d : double) = text (string d)

let line = Line

let (^^) x y =
    match x, y with
    | Empty, y -> y
    | x, Empty -> x
    | x, y     -> Concat(x, y)
