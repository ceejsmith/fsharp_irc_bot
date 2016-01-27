open System
open System.Net.Sockets
open System.Threading

open Command
open Reply

[<EntryPoint>]
let main _ = 
    let port = 6667
    let server = "chat.freenode.net"
    let client = new TcpClient(server, port) // Use "new" because class implements IDisposable
    let stream = client.GetStream()
    Console.Write(receive stream)
    stream |> send (Nick "cjsmithie")
    stream |> send (User ("cjsmithie", "Chris Smith"))
    Thread.Sleep 3000
    Console.Write(receive stream)
    stream |> send (Join "#haskell")
    Thread.Sleep 3000
    Console.Write(receive stream)
    stream |> send (Quit "")
    Thread.Sleep 3000
    Console.Write(receive stream)
    0