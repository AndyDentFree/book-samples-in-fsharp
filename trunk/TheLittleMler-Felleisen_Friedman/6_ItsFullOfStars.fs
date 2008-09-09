#light

type Fruit =
    | Peach
    | Apple
    | Pear
    | Lemon
    | Fig

type Tree =
    | Bud
    | Flat of Fruit * Tree
    | Split of Tree * Tree

let rec flatOnly = function
    | Bud        -> true
    | Flat(f,t)  -> flatOnly t
    | Split(s,t) -> false

let rec splitOnly = function
    | Bud -> true
    | Flat(f,t) -> false
    | Split(s,t) ->
        if splitOnly s
            then splitOnly t
            else false

let containsFruit x =
    if splitOnly x
        then false
        else true

let largerOf n m =
    if n < m
        then m
        else n

let rec height = function
    | Bud        -> 0
    | Flat(_,t)  -> 1 + height t
    | Split(s,t) -> 1 + largerOf (height s) (height t)

let eqFruit f1 f2 =
    match f1,f2 with
    | Peach,Peach -> true
    | Apple,Apple -> true
    | Pear,Pear   -> true
    | Lemon,Lemon -> true
    | Fig,Fig     -> true
    | _           -> false

let rec substInTree n a f =
    match n,a,f with
    | _,_,Bud        -> Bud
    | n,a,Flat(f,t)  ->
        if eqFruit f a
            then Flat(n, substInTree n a t)
            else Flat(f, substInTree n a t)
    | n,a,Split(s,t) ->
        Split(substInTree n a s, substInTree n a t)

let rec occurs a t =
    match a,t with
    | _,Bud        -> 0
    | a,Flat(f,t)  ->
        if eqFruit f a
            then 1 + occurs a t
            else occurs a t
    | a,Split(s,t) ->
        occurs a s + occurs a t

type Slist<'a> =
    | Empty
    | Scons of Sexp<'a> * Slist<'a>
and Sexp<'a> =
    | AnAtom of 'a
    | Aslist of Slist<'a>

let eqFruitInAtom a exp =
    match a,exp with
    | a,AnAtom(s) -> eqFruit a s
    | a,Aslist(y) -> false

let rec remFromSlist a ls =
    match a,ls with
    | _,Empty -> Empty
    | a,Scons(AnAtom(b),y) ->
        if eqFruit a b
            then remFromSlist a y
            else Scons(AnAtom(b), remFromSlist a y)
    | a,Scons(Aslist(x),y) ->
        Scons(Aslist(remFromSlist a x), remFromSlist a y)
and remFromSexp a exp =
    match a,exp with
    | a,AnAtom(b) -> AnAtom(b)
    | a,Aslist(y) -> Aslist(remFromSlist a y)

