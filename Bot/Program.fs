open System
open System.Net.Sockets
open System.Threading

open Command
open Reply

let output (stream: NetworkStream) =
    Thread.Sleep 3000
    let (text, reply) = receive stream
    Console.WriteLine(text)

[<EntryPoint>]
let main _ = 
    let port = 6667
    let server = "chat.freenode.net"
    let client = new TcpClient(server, port) // Use "new" because class implements IDisposable
    let stream = client.GetStream()
    
    output stream

    stream |> send (Nick "cjsmithie")
    stream |> send (User ("cjsmithie", "Chris Smith"))
    
    output stream

    stream |> send (Join "#haskell")
    
    // TODO: Why do the names not come back with this read?
    output stream

    stream |> send (Quit "")
    
    output stream

    0