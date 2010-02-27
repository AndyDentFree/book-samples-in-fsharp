open System
open System.IO

let processFileAsync (filePath : string) (processBytes : byte[] -> byte[]) =
    
    // This is the callback from when the AsyncWrite completes
    let asyncWriteCallback =
        new AsyncCallback(fun (iar : IAsyncResult) ->
            // Get state from the async result
            let writeStream = iar.AsyncState :?> FileStream
            
            // End the async write operation by calling EndWrite
            let bytesWritten = writeStream.EndWrite(iar)
            writeStream.Close()
            
            printfn
                "Finished processing file [%s]"
                (Path.GetFileName(writeStream.Name))
        )
    
    // This is the callback from when the AsyncRead completes
    let asyncReadCallback =
        new AsyncCallback(fun (iar : IAsyncResult) ->
            let readStream, data = iar.AsyncState :?> (FileStream * byte[])
            
            // End the async read by calling EndRead
            let bytesRead = readStream.EndRead(iar)
            readStream.Close()
            
            // Process the result
            printfn
                "Processing file [%s], read [%d] bytes"
                (Path.GetFileName(readStream.Name))
                bytesRead
            
            let updatedBytes = processBytes data
            
            let resultFile = new FileStream(readStream.Name + ".result",
                                            FileMode.Create)
            
            let _ =
                resultFile.BeginWrite(
                    updatedBytes,
                    0, updatedBytes.Length,
                    asyncWriteCallback,
                    resultFile)
            
            ()
        )
    
    // Begin the async read, whose callback will begin the async write
    let fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read,
                                    FileShare.Read, 2048,
                                    FileOptions.Asynchronous)
    
    let fileLength = int fileStream.Length
    let buffer = Array.zeroCreate fileLength
    
    // State passed into the async read
    let state = (fileStream, buffer)
    
    printfn "Processing file [%s]" (Path.GetFileName(filePath))
    let _ = fileStream.BeginRead(buffer, 0, buffer.Length,
                                 asyncReadCallback, state)
    ()

processFileAsync __SOURCE_FILE__ (fun b -> b)
