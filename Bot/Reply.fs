module Reply

open System.Net.Sockets
open System.Text

type Reply =
    | Unknown
    | Ping of string // server name
    | Error of string // error message
    | Message of string // TODO: parse reply to determine type of message

let parse (reply: string) =
    if      reply.StartsWith("PING")    then Ping(reply.Substring(5))
    else if reply.StartsWith("ERROR")   then Error(reply.Substring(6))
    else if reply.StartsWith(":")       then Message(reply.Substring(1))
    else                                     Unknown
    
let BUFFERSIZE = 8192
let buffer = Array.zeroCreate<byte> BUFFERSIZE

let receive (stream: NetworkStream) =
    let byteCount = stream.Read(buffer, 0, BUFFERSIZE)
    let text = Encoding.ASCII.GetString(buffer, 0, byteCount)
    (text, parse text)