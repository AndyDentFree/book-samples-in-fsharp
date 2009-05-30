-- file: ch04/Map.hs
square :: [Double] -> [Double]

square (x:xs) = x*x : square xs
square []     = []

import Data.Char (toUpper)

upperCase :: String -> String

upperCase (x:xs) = toUpper x : upperCase xs
upperCase []     = []

square2 xs = map squareOne xs
    where squareOne x = x * x

upperCase2 xs = map toUpper xs
