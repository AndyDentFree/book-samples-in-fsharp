let lines input =
    System.IO.File.ReadAllLines(input)

let wordCount input =
     lines input |> Seq.length

let main() =
    let path = fsi.CommandLineArgs.[1]
    printfn "%d" (wordCount path)

main()