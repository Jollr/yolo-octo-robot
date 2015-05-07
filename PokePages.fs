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

let private buttonElement (pokeName:string) = "<button type='submit'><img src='" + pokeName + "/img'/></button>"
let private timer (pokemon: PokeRegistration) =
    "<script type='text/javascript' src='jquery'></script>\n" +
    "<script>\n" +
    "   $(function() {\n" +
    "       var cooldown = " + (pokemon.GetCooldown()).ToString() + ";\n" +
    "       var countDown = function() { if (cooldown > 0) cooldown--; }\n" +
    "       var timeString = function() { \n" +
    "           if (cooldown >= 60) { return '1:00'; }\n" +
    "           else if (cooldown < 10) { return '0:0' + cooldown; }\n" +
    "           else { return '0:' + cooldown; }\n" +
    "       };\n" +
    "       var tick = function() { countDown(); $('#timer').html(timeString()); };\n" +
    "       window.setInterval(tick, 1000); \n" +
    "   });\n" +
    "</script>\n"

let private timerElement () = "<div id='timer'></div>"
let private pokeImage (pokeName: string) = file ("img/" + pokeName + ".png")
let private buttonPage (pokemon: PokeRegistration) = "<html><head>" + (timer pokemon) + "</head><body><form method='POST'>" + (buttonElement pokemon.Name) + "</form>" + timerElement() + "</body></html>" 

let private getPokePage (id: Guid) =
    match PokeMappings.GetById id with
    | Some pokemon -> OK (buttonPage pokemon)
    | None -> NOT_FOUND ""

let private postPokePage (id: Guid) =
    let trigger (pokemon: PokeRegistration) = 
        Puzzle.trigger pokemon.X pokemon.Y
        getPokePage id
     
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
