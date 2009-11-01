namespace BinarySearchTree

module Set =
    type 'a Tree when 'a : comparison =
        | E
        | T of 'a Tree * 'a * 'a Tree
    
    let empty = E
    let rec isMember = function
        | _, E        -> false
        | x, T(a,y,b) ->
            if x < y then isMember (x, a)
            elif x > y then isMember (x, b)
            else true
    let rec insert = function
        | x, E               -> T(E,x,E)
        | x, (T(a,y,b) as s) ->
            if x < y then T(insert (x, a),y,b)
            elif x > y then T(a,y,insert (x, b))
            else s
