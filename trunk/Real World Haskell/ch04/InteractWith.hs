-- file: ch04/InteractWith.hs

import System.Environment (getArgs)

interactWith function inputFile outputFile = do
    input <- readFile inputFile
    writeLine outputFile (function input)

main = mainWith myFunction
    where mainWith function = do
        args <- getArgs
        case args of
          [input,output] -> interactWith function input output
          _ -> putStrln "error: exactly two arguments needed"
          
        -- replace "id" with the name of our function below
        myFunction = id

