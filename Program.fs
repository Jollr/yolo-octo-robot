open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Web
open Suave.Types

open System.Net

let pokePage pokeName = "<html><head></head><body><form method=\"POST\"><input type=\"submit\" value=\"" + pokeName + "\"/></form</body></html>" 

let pokeButton pokeName = 
    printf "%s" pokeName
    OK pokeName

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "127.0.0.1" 8085 ] }

let app =
  choose
    [ GET >>= choose
        [ path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" 
          path "/bulbasaur" >>= OK (pokePage "bulbasaur") ] 
      POST >>= choose 
        [ path "/bulbasaur" >>= pokeButton "Bulbasaur"] ]

startWebServer config app