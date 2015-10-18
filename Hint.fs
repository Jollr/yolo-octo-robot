module Hint

open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Html

let private head = "<style> body { color: white; }</style><title>de mol</title>"
let private body = "<div>Albert is de mol</div>"
let private hintPage = Page(head, body) 

let hintBindings = [ GET >>= choose [ path "/demolis" >>= OK hintPage ] ]
