module PokePages

open System
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Redirection
open Suave.Http.RequestErrors
open Suave.Http.Files
open Suave.Types

open GuidScan
open Puzzle
open PokeRegistration
open PokeMappings
open PokeHtml

let private pokeImage (pokeName: string) = file ("img/" + pokeName + ".png")

let private getPokePage (id: Guid) =
    match PokeMappings.GetById id with
    | Some pokemon -> OK ((ButtonPage pokemon) |> Seq.reduce (+))
    | None -> NOT_FOUND ""

let private postPokePage (id: Guid) =
    let trigger (pokemon: PokeRegistration) = 
        if (pokemon.Trigger()) then Puzzle.trigger pokemon.X pokemon.Y
        Redirection.FOUND ("/pokemon/" + pokemon.Id.ToString())
     
    match PokeMappings.GetById id with
    | Some pokemon -> trigger pokemon
    | None -> NOT_FOUND ""

let pokeBindings = 
    [ GET >>= choose
        [ path "/pokemon/jquery" >>= file "jquery-2.1.3.js"
          pathScan "/pokemon/%s/img" pokeImage
          guidScan "/pokemon/" getPokePage ] 
      POST >>= choose 
        [ guidScan "/pokemon/" postPokePage] ]
