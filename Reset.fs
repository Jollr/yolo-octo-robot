module Reset

open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Authentication
open Suave.Http.Redirection
open Suave.Http.Successful
open Suave.Http.Applicatives

open Html
open Puzzle

let private resetPage () =
    let head = seq { yield "<title>Reset</title>" }
    let body = seq {
        yield "<form method='POST'>"
        yield " <br/>"
        yield " <input type='submit' value='RESET'/>" 
        yield "</form>"
    }
    Html.PageSq(head, body)

let private postReset (x: HttpContext) =
    Puzzle.Reset()
    Redirection.FOUND "/reset"

let auth = authenticateBasic (fun (user, pass) -> pass = "Deventer123" )

let resetBindings = 
    [ GET >>= choose
        [ path "/reset" >>= OK (resetPage()) ] 
      POST >>= choose 
        [ auth
          path "/reset" >>= context postReset ] ]