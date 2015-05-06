﻿module PokePages

open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Types

open Puzzle

let buttonElement pokeName = "<button type='submit'><img src='" + pokeName + ".png'/></button>"
let pokePage pokeName = "<html><head></head><body><form method='POST'>" + (buttonElement pokeName) + "</form></body></html>" 

let pokeButton pokeName = 
    printfn "%s" pokeName
    Puzzle.trigger 1 1
    OK pokeName

let pokeBindings = 
    [ GET >>= choose
        [ path "/bulbasaur" >>= OK (pokePage "bulbasaur")
          path "/bulbasaur.png" >>= file "img/bulbasaur.png" ] 
      POST >>= choose 
        [ path "/bulbasaur" >>= context (fun x -> pokeButton "Bulbasaur") ] ]