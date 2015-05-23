module app

open Suave
open Suave.Web
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.RequestErrors
open Suave.Http.Files
open Suave.Utils

open PokePages
open Dashboard

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "127.0.0.1" 8082 ] }

let globalBindings = 
    [ GET >>= choose
        [ path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" 
          path "/jquery" >>= file "jquery-2.1.3.js"
          path "/favicon.ico" >>= NOT_FOUND ""] ]

let allBindings = List.append (List.append globalBindings dashboardBindings) pokeBindings

let app = choose allBindings

[<EntryPoint>]
let main args = 
    PokeMappings.Initialize()
    startWebServer config app
    0