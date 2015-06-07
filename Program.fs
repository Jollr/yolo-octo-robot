module app

open Suave
open Suave.Web
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.RequestErrors
open Suave.Http.Files
open System.Net

open PokePages
open Dashboard
open Reset

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "192.168.1.13" 8082 ] }

let globalBindings = 
    [ GET >>= choose
        [ path "/" >>= OK "Server is up and running!"
          path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" 
          path "/jquery" >>= file "jquery-2.1.3.js"
          path "/favicon.ico" >>= NOT_FOUND ""] ]

let allBindings = 
    pokeBindings
    |> List.append dashboardBindings
    |> List.append globalBindings
    |> List.append resetBindings

let app = choose allBindings

[<EntryPoint>]
let main args = 
    PokeMappings.Initialize()
    startWebServer config app
    0