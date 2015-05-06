module PokePages

open System
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.RequestErrors
open Suave.Http.Files
open Suave.Types

open GuidScan
open Puzzle
open PokeMappings

let private buttonElement (pokeName:string) = "<button type='submit'><img src='" + pokeName + "/img'/></button>"
let private buttonPage (pokemon: PokeRegistration) = "<html><head></head><body><form method='POST'>" + (buttonElement pokemon.Name) + "</form></body></html>" 
let private pokeImage (pokeName: string) = "img/" + pokeName + ".png"

let private getPokePage (id: Guid) =
    match PokeMappings.GetById id with
    | Some pokemon -> OK (buttonPage pokemon)
    | None -> NOT_FOUND ""

//let private trigger pokeName = 
//    printfn "%s" pokeName
//    Puzzle.trigger 1 1
//    OK pokeName

let private postPokePage (id: Guid) =
    let str = id.ToString()
    printfn "%s" str
    let trigger (pokemon: PokeRegistration) = 
        printfn "%s" pokemon.Name
        Puzzle.trigger pokemon.X pokemon.Y
        OK pokemon.Name
     
    match PokeMappings.GetById id with
    | Some pokemon -> trigger pokemon
    | None -> NOT_FOUND ""


let pokeBindings = 
    [ GET >>= choose
        [ pathScan "/pokemon/%s/img" ( fun(pokeName) -> file (pokeImage pokeName) )
          guidScan "/pokemon/" getPokePage ] 
      POST >>= choose 
        [ guidScan "/pokemon/" postPokePage] ]
