type Doc

val empty  : Doc
val char   : char -> Doc
val text   : string -> Doc
val double : double -> Doc
val line   : Doc
val ( ^^ ) : Doc -> Doc -> Doc
