#light

type Meza =
    | Shrimp
    | Calamari
    | Escargots
    | Hummus

type Main =
    | Steak
    | Ravioli
    | Chicken
    | Eggplant

type Salad =
    | Green
    | Cucumber
    | Greek

type Dessert =
    | Sundae
    | Mousse
    | Torte

let addASteak (x: Meza) = (x,Steak)

let eqMain = function
    | (Steak,Steak)       -> true
    | (Ravioli,Ravioli)   -> true
    | (Chicken,Chicken)   -> true
    | (Eggplant,Eggplant) -> true
    | (_,_)               -> false

let hasSteak = function
    | (_:Meza,Steak,_:Dessert) -> true
    | (_,_,_)                  -> false
