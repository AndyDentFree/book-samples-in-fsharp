let myDumbExample xs = if List.length xs > 0
                       then List.hd xs
                       else 'Z'

let mySmartExample xs = if not (List.isEmpty xs)
                        then List.hd xs
                        else 'Z'

let myOtherExample = function
    | (x::_) -> x
    | []     -> 'Z'