-- file: ch04/Fold.hs
foldl :: (a -> b -> a) -> a -> [b] -> a
foldl step zero (x:xs) = foldl step (step zero x) xs
foldl _    zero []     = zero

foldSum xs = foldl step 0 xs
    where step acc x = acc + x

niceSum :: [Integer] -> Integer
niceSum xs = foldl (+) 0 xs    

foldr :: (a -> b -> b) -> b -> [a] -> b
foldr step zero (x:xs) = step x (foldr step zero xs)
foldr _    zero []     = zero

filter :: (a -> Bool) -> [a] -> [a]
filter p []   = []
filter p (x:xs)
    | p x       = x : filter p xs
    | otherwise = filter p xs

myFilter p xs = foldr step [] xs
    where step x ys | p x       = x : ys
                    | otherwise = ys

myMap :: (a -> b) -> [a] -> [b]
myMap f xs = foldr step [] xs
    where step x ys = f x : ys

myFoldl :: (a -> b -> a) -> a -> [b] -> a
myFoldl f z xs = foldr step id xs z
    where step x g a = g (f a x)

identity :: [a] -> [a]
identity xs = foldr (:) ys xs

append :: [a] -> [a] -> [a]
append xs ys = foldr (:) ys xs
