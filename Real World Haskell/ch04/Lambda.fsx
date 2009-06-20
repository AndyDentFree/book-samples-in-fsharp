let safeHead = function
    | x::_ -> Some x
    | _    -> None

let unsafeHead = (fun (x::_) -> x)
