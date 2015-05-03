﻿module app

open Suave
open Suave.Web
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Utils

open System.Net

open PokePages
open Dashboard

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "127.0.0.1" 8082 ] }

let app =
  choose
    [ GET >>= choose
        [ path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" 
          path "/bulbasaur" >>= OK (pokePage "bulbasaur")
          path "/dashboard" >>= OK (dashboardPage 5 5)
          path "/bulbasaur.png" >>= file "img/bulbasaur.png" ] 
      POST >>= choose 
        [ path "/bulbasaur" >>= pokeButton "Bulbasaur"] ]


[<EntryPoint>]
let main args = 
    startWebServer config app
    0