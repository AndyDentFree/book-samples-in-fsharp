#light

module Example1 =
    exception TooSmall

    let isZero n =
        n = 0

    let pred n =
        if n = 0
            then raise TooSmall
            else n - 1

    let succ n =
        n + 1

    let rec plus (n,m) =
        if isZero n
            then m
            else succ (plus (pred n, m))

module Example2 =
    type Num =
        | Zero
        | OneMoreThan of Num

    exception TooSmall

    let isZero = function
        | Zero -> true
        | _    -> false
    
    let pred = function
        | Zero          -> raise TooSmall
        | OneMoreThan n -> n
    
    let succ n =
        OneMoreThan n

    let rec plus (n,m) =
        if isZero n
            then m
            else succ (plus (pred n, m))

module Example3 =
    open Example2
    
    exception TooSmall
    
    type 'a N =
        abstract succ : 'a -> 'a
        
        abstract pred : 'a -> 'a
        
        abstract isZero : 'a -> bool


    let numberAsInt =
        { new int N with
            member this.succ n =
                n + 1
                
            member this.pred n =
                if n = 0
                    then raise TooSmall
                    else n - 1
            
            member this.isZero n =
                n = 0 }
    
    let numberAsNum =
        { new Num N with
            member this.succ n =
                OneMoreThan n
            
            member this.isZero n =
                match n with
                | Zero -> true
                | _    -> false
            
            member this.pred n =
                match n with
                | Zero          -> raise TooSmall
                | OneMoreThan n -> n }

    
type 'a N =
    abstract TooSmall : System.Type
    
    abstract number : 'a
    
    
    