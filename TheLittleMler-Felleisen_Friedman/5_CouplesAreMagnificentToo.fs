#light

type Pizza<'a> =
    | Bottom
    | Topping of 'a * Pizza<'a>

type Fish =
    | Anchovy
    | Lox
    | Tuna

let rec remAnchovy = function
    | Bottom             -> Bottom
    | Topping(Anchovy,p) -> remAnchovy p
    | Topping(t,p)       -> Topping(t,remAnchovy p)

let rec remTuna = function
    | Bottom             -> Bottom
    | Topping(Tuna,p)    -> remTuna p
    | Topping(t,p)       -> Topping(t,remTuna p)

let eqFish = function
    | (Anchovy,Anchovy) -> true
    | (Lox,Lox)         -> true
    | (Tuna,Tuna)       -> true
    | (_,_)             -> false

let rec remFish= function
    | (_,Bottom)         -> Bottom
    | (x',Topping(t,p')) ->
        if eqFish (t, x') then remFish (x', p')
        else Topping(t,(remFish (x', p')))

let rec remInt (x: int) p =
    match x,p with
    | (_,Bottom)         -> Bottom
    | (x',Topping(t,p')) ->
        if t = x' then remInt x' p'
        else Topping(t,(remInt x' p'))

let rec substFish n a p =
    match n,a,p with
    | (_,_,Bottom)          -> Bottom
    | (n',a',Topping(t,p')) ->
        if eqFish t a' then Topping(n',substFish n' a' p')
        else Topping(t,substFish n' a' p')

let rec substInt n a p =
    match n,a,p with
    | (_,_,Bottom)          -> Bottom
    | (n',a',Topping(t,p')) ->
        if t = a' then Topping(n',substInt n' a' p')
        else Topping(t,substInt n' a' p')
