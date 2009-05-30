type Vector = double * double

type Shape = Circle of Vector * double
           | Poly of Vector list
