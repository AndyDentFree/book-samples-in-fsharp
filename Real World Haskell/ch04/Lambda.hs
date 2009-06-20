-- file: ch04/Lambda.hs
safeHead (x:_) = Just x
safeHead _ = Nothing

unsafeHead = \(x:_) -> x
