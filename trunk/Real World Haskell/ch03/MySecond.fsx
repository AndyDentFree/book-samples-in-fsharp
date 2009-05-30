let mySecond xs = if List.tl xs = []
                  then failwith "list too short"
                  else List.hd (List.tl xs)

let safeSecond = function
    | [] -> None
    | xs -> if List.tl xs = []
            then None
            else Some (List.hd (List.tl xs))

let tidySecond = function
    | (_::x::_) -> Some x
    | _         -> None