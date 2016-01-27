module Command

open System
open System.Net.Sockets
open System.Text

type Command =
    | Nick of string
    | User of string * string
    | Join of string
    | Pong of string
    | Quit of string

let newline = "\r\n"

let build command = 
    let text =
        match command with
            | Nick(name) -> "NICK " + name
            | User(nickname, fullname) -> "USER " + nickname + " 8 * : " + fullname
            | Join(channel) -> "JOIN " + channel
            | Pong(servername) -> "PONG " + servername
            | Quit(reason) -> "QUIT " + reason
    text + "\r\n"

let send command (stream: NetworkStream) =
    let text = build command
    let bytes = Encoding.ASCII.GetBytes(text)
    Console.Write(text)
    stream.Write(bytes, 0, bytes.Length)