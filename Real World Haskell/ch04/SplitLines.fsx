#light
open System

let break' (f : 'a -> bool) (xs : 'a list) =
    let i = List.tryfind_index f xs
    match i with
    | None   -> (xs, [])
    | Some i -> (Seq.take i xs |> Seq.to_list, Seq.skip i xs |> Seq.to_list)

let isLineTerminator c = c = '\r' || c = '\n'

module String =
    
    let to_list (s : string) = s.ToCharArray() |> Array.to_list
    
    let of_list (cs : char list) =
        let cs = List.to_array cs
        new String(cs)

let splitLines (lines : string) =
    let lines = String.to_list lines
    
    let rec splitLines' = function
        | [] -> []
        | cs ->
            let (pre, suf) = break' isLineTerminator cs
            pre :: match suf with
                   | ('\r'::'\n'::rest) -> splitLines' rest
                   | ('\r'::rest)       -> splitLines' rest
                   | ('\n'::rest)       -> splitLines' rest
                   | _                  -> []
    splitLines' lines
    |> List.map String.of_list
    
    