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

let build = function
    | Nick(name) -> "NICK " + name + newline
    | User(nickname, fullname) -> "USER " + nickname + " 8 * : " + fullname + newline
    | Join(channel) -> "JOIN " + channel + newline
    | Pong(servername) -> "PONG " + servername + newline
    | Quit(reason) -> "QUIT " + reason + newline

let send command (stream: NetworkStream) =
    let text = build command
    let bytes = Encoding.ASCII.GetBytes(text)
    Console.Write(text)
    stream.Write(bytes, 0, bytes.Length)