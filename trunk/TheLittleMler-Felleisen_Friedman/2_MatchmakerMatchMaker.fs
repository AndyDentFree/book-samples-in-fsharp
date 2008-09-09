#light

type ShishKebab =
    | Skewer
    | Onion of ShishKebab
    | Lamb of ShishKebab
    | Tomato of ShishKebab

let rec onlyOnions = function
    | Skewer    -> true
    | Onion(x)  -> onlyOnions x
    | Lamb(x)   -> false
    | Tomato(x) -> false

let rec isVegetarian = function
    | Skewer    -> true
    | Onion(x)  -> isVegetarian x
    | Lamb(x)   -> false
    | Tomato(x) -> isVegetarian x

type Shish<'a> =
    | Bottom of 'a
    | Onion of Shish<'a>
    | Lamb of Shish<'a>
    | Tomato of Shish<'a>

type Rod =
    | Dagger
    | Fork
    | Sword

type Plate =
    | GoldPlate
    | SilverPlate
    | BrassPlate

let rec isVeggie = function
    | Bottom(_) -> true
    | Onion(x)  -> isVeggie x
    | Lamb(_)   -> false
    | Tomato(x) -> isVeggie x

let rec whatBottom = function
    | Bottom(x) -> x
    | Onion(x)  -> whatBottom x
    | Lamb(x)   -> whatBottom x
    | Tomato(x) -> whatBottom x

