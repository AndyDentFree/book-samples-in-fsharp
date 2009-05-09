#light

let lend amount balance = let reserve = 100
                          let newBalance = balance - amount
                          in if balance < reserve
                             then None
                             else Some newBalance
