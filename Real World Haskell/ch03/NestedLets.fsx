let foo = let a = 1
          in let b = 2
             in a + b

let bar = let x = 1
          in ((let x = "foo" in x), x)

let quux a = let a = "foo"
             in a ^ "eek!"
