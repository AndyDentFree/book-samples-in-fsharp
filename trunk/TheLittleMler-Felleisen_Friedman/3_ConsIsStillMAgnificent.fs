#light

type Pizza =
    | Crust
    | Cheese of Pizza
    | Onion of Pizza
    | Anchovy of Pizza
    | Sausage of Pizza

let rec removeAnchovy = function
    | Crust      -> Crust
    | Cheese(x)  -> Cheese(removeAnchovy x)
    | Onion(x)   -> Onion(removeAnchovy x)
    | Anchovy(x) -> removeAnchovy(x)
    | Sausage(x) -> Sausage(removeAnchovy x)

let rec topAnchovyWithCheese = function
    | Crust      -> Crust
    | Cheese(x)  -> Cheese(topAnchovyWithCheese x)
    | Onion(x)   -> Onion(topAnchovyWithCheese x)
    | Anchovy(x) -> Cheese(Anchovy(topAnchovyWithCheese x))
    | Sausage(x) -> Sausage(topAnchovyWithCheese x)

let substAnchovyByCheese x =
    removeAnchovy(topAnchovyWithCheese x)

let rec substituteAnchovyByCheese = function
    | Crust      -> Crust
    | Cheese(x)  -> Cheese(substituteAnchovyByCheese x)
    | Onion(x)   -> Onion(substituteAnchovyByCheese x)
    | Anchovy(x) -> Cheese(substituteAnchovyByCheese x)
    | Sausage(x) -> Sausage(substituteAnchovyByCheese x)

