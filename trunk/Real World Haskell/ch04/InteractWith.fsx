let putStrln (s : string) = System.Console.WriteLine(s)
let readFile file         = System.IO.File.ReadAllLines(file)
let writeFile file lines  = System.IO.File.WriteAllLines(file, lines)
let getArgs               = fsi.CommandLineArgs

let interactWith func inputFile outputFile = do
    let input = readFile inputFile
    writeFile outputFile (func input)

let mainWith func = do
    let args = getArgs
    match args with
    | [| _; input; output |] -> interactWith func input output
    | _                      -> putStrln "error: exactly two arguments needed"    

let addToEnd (ss : string[]) = [| for s in ss do yield s + "_end_" |]

let main() = mainWith addToEnd
main()
