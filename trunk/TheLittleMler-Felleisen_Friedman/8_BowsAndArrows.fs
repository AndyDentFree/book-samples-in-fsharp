#light

type List<'a> =
    | Empty
    | Cons of 'a * List<'a>

type Orapl =
    | Orange
    | Apple

let eqOrapl = function
    | Orange,Orange -> true
    | Apple,Apple   -> true
    | _             -> false

let rec substOrapl = function
    | n,a,Empty     -> Empty
    | n,a,Cons(e,t) ->
        if eqOrapl (a, e)
            then Cons(n,substOrapl (n, a, t))
            else Cons(e,substOrapl (n, a, t))

let rec subst = function
    | rel,n,a,Empty     -> Empty
    | rel,n,a,Cons(e,t) ->
        if rel (a,e)
            then Cons(n,subst (rel, n, a, t))
            else Cons(e,subst (rel, n, a, t))

let inRange ((small, large), x) =
    if small < x
        then x < large
        else false

let rec substPred = function
    | pred,n,Empty     -> Empty
    | pred,n,Cons(e,t) ->
        if pred e
            then Cons(n,substPred (pred, n, t))
            else Cons(e,substPred (pred, n, t))

let is15 n =
    n = 15

let lessThan15 x =
    x < 15

let inRange11_16 x =
    if 11 < x
        then x < 16
        else false

let inRangeC (small, large) =
    let inRange' x =
        if small < x
            then x < large
            else false
    inRange'

let inRangeC11_16 x =
    if 11 < x
        then x < 16
        else false

let rec substC pred =
    let substC' = function
    | n,Empty     -> Empty
    | n,Cons(e,t) ->
        if pred e
            then Cons(n,substC pred (n,t))
            else Cons(e,substC pred (n,t))
    substC'

let rec substCInRange11_16 = function
    | n,Empty     -> Empty
    | n,Cons(e,t) ->
        if inRange11_16 e
            then Cons(n,substCInRange11_16 (n,t))
            else Cons(e,substCInRange11_16 (n,t))

let rec combine = function
    | Empty,l2      -> l2
    | Cons(a,l1),l2 ->
        Cons(a,combine (l1,l2))

let rec combineC x =
    let combineC' l2 =
        match x with
        | Empty      -> l2
        | Cons(a,l1) ->
            Cons(a,combineC l1 l2)
    combineC'

let prefixer123 l2 =
    Cons(1,
        Cons(2,
            Cons(3,
                l2)))

let waitingPrefixer123 l2 =
    Cons(1,
        combineC(
            Cons(2,
                Cons(3,
                    Empty)))
            l2)

let base' l2 =
    l2

let rec combineS = function
    | Empty      -> base'
    | Cons(a,l1) ->
        makeCons(a,combineS(l1))
and makeCons (a,f) =
    let makeCons' l2 =
        Cons(a,f l2)
    makeCons'

let prefix3 l2 =
    Cons(3,base' l2)
    
let prefix23 l2 =
    Cons(2,prefix3 l2)

let prefix123 l2 =
    Cons(1,prefix23 l2)
