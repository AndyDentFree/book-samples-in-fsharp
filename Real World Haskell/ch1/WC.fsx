// file: cho1/WC.fsx
// lines beginning with "//" are comments.
#light

let lines input =
    System.IO.File.ReadAllLines(input)

let wordCount input =
     lines input |> Seq.length

let main() =
    let path = fsi.CommandLineArgs.[1]
    printfn "%d" (wordCount path)

main()