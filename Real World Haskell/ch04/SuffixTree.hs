-- file: ch04/SuffixTree.hs
suffixes :: [a] -> [[a]]
suffixes :: xs@(_:xs') = xs : suffixes xs'
suffixes _ = []

noAsPattern :: [a] -> [[a]]
noAsPattern (x:xs) = (x:xs) : noAsPattern xs
noAsPattern _ = []

suffixes2 xs = init (tail xs)

compose :: (b -> c) -> (a -> b) -> a -> c
compose f g x = f (g x)


