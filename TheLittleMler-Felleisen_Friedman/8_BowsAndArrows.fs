#light

type List<'a> =
    | Empty
    | Cons of 'a * List<'a>

type Orapl =
    | Orange
    | Apple

let eqOrapl x y =
    match x,y with
    | Orange,Orange -> true
    | Apple,Apple   -> true
    | _             -> false

let rec substOrapl n a = function
    | Empty     -> Empty
    | Cons(e,t) ->
        if eqOrapl a e
            then Cons(n,substOrapl n a t)
            else Cons(e,substOrapl n a t)

let rec subst rel n a = function
    | Empty     -> Empty
    | Cons(e,t) ->
        if rel a e
            then Cons(n,subst rel n a t)
            else Cons(e,subst rel n a t)

let inRange ((small, large), x) =
    if small < x
        then x < large
        else false

let rec substPred pred n = function
    | Empty     -> Empty
    | Cons(e,t) ->
        if pred e
            then Cons(n,substPred pred n t)
            else Cons(e,substPred pred n t)

let is15 n =
    n = 15

let lessThan15 x =
    x < 15

let inRange11_16 x =
    if 11 < x
        then x < 16
        else false

let inRangeC (small, large) x =
    if small < x
        then x < large
        else false
