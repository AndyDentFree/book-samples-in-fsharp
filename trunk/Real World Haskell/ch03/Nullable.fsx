type 'a Maybe = Just of 'a
              | Nothing

let someBool = Just true

let someString = Just "something"

let wrapped = Just (Just "wrapped")