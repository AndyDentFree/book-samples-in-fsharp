// file: ch03/BookStore.fsx
#light

type BookInfo = Book of int64 * string * string list

let myInfo = Book(9780135072455L, "Algebra of Programming", ["Oege de Moor"])

type CustomerID = int
type ReviewBody = string

type BookReview = BookReview of BookInfo * CustomerID * string

type BetterReview = BetterReview of BookInfo * CustomerID * ReviewBody

type CardHolder = string
type CardNumber = string
type Address = string list

type BillingInfo =
    | CreditCard of CardNumber * CardHolder * Address
    | CashOnDelivery
    | Invoice of CustomerID

let bookID      (Book(id, title, authors)) = id

let bookTitle   (Book(id, title, authors)) = title

let bookAuthors (Book(id, title, authors)) = authors

