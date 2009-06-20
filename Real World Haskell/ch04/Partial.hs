-- file: ch04/Partial.hs
isInAny needle haystack = any inSequence haystack
    where inSequence s = needle `isInfixOf` s

isInAny2 needle haystack = any (\s -> needle `isInfixOf` s) haystack
