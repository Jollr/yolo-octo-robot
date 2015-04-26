open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Web
open Suave.Types

open System.Net

let pokePage pokeName = "<html><head></head><body><form method=\"POST\"><input type=\"submit\" value=\"" + pokeName + "\"/></form</body></html>" 

let pokeButton pokeName = 
    do sprintf pokeName
    let result = OK "Koning Bauke Jan is de mol"
    result

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "192.168.1.12" 80 ] }

let app =
  choose
    [ GET >>= choose
        [ path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" 
          path "/bulbasaur" >>= OK (pokePage "bulbasaur") ] 
      POST >>= choose 
        [ path "/bulbasaur" >>= OK "You pressed Bulbasaur!"] ]

startWebServer config app