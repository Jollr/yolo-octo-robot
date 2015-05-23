module Reset

open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Redirection
open Suave.Http.Successful
open Suave.Http.Applicatives

open Html
open Puzzle

let private resetPage () =
    let head = seq { yield "<title>Reset</title>" }
    let body = seq {
        yield "<form method='POST'>"
        yield " <input type='password' name='password'/> " 
        yield " <br/>"
        yield " <input type='submit' value='RESET'/>" 
        yield "</form>"
    }
    Html.PageSq(head, body)

let private postReset () = 
    Puzzle.Reset()
    Redirection.FOUND "/reset"

let resetBindings = 
    [ GET >>= choose
        [ path "/reset" >>= OK (resetPage()) ] 
      POST >>= choose 
        [ path "/reset" >>= context (fun x -> postReset()) ] ]