open System.IO

let asyncProcessFile (filePath : string) (processBytes : byte[] -> byte[]) =
    async {
        printfn "Processing file [%s]" (Path.GetFileName(filePath))
        
        let fileStream = new FileStream(filePath, FileMode.Open)
        let bytesToRead = int fileStream.Length
        
        let! data = fileStream.AsyncRead(bytesToRead)
        
        printfn
            "Opened [%s], read [%d] bytes"
            (Path.GetFileName(filePath))
            data.Length
        
        let data' = processBytes data
        
        let resultFile = new FileStream(filePath + ".results", FileMode.Create)
        do! resultFile.AsyncWrite(data', 0, data'.Length)
        printfn "Finished processing file [%s]" <| Path.GetFileName(filePath)
    } |> Async.Start
