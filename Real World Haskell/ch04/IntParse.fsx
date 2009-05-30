open System

let digitToInt (x : char) = Int32.Parse(x.ToString())

let asInt (s : String) =
    let cs = s.ToCharArray() |> Seq.to_list
    let rec loop acc = function
        | []    -> acc
        | x::xs -> let acc' = acc * 10 + digitToInt x
                   in loop acc' xs
    loop 0 cs
