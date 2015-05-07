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

let private pokeImage (pokeName: string) = file ("img/" + pokeName + ".png")

let private buttonElement (pokeName:string) = seq {
    yield "<button type='submit'><img src='" 
    yield pokeName 
    yield "/img'/></button>" }

let private pokeForm (pokeName:string) = seq {
    yield "<form method='POST'>" 
    yield (buttonElement pokeName) |> Seq.reduce (+)
    yield "</form>" }

let private timer (pokemon: PokeRegistration) = seq {
    yield "<script type='text/javascript' src='jquery'></script>" 
    yield "<script>" 
    yield "   $(function() {" 
    yield "       var cooldown = " + (pokemon.GetCooldown()).ToString() + ";" 
    yield "       var countDown = function() { if (cooldown > 0) {cooldown--;} };" 
    yield "       var timeString = function() { " 
    yield "           if (cooldown >= 60) { return '1:00'; }" 
    yield "           else if (cooldown < 10) { return '0:0' + cooldown; }" 
    yield "           else if (cooldown <= 0) { return ''; }" 
    yield "           else { return '0:' + cooldown; }" 
    yield "       };" 
    yield "       var updateTimer = function() {$('#timer').html(timeString());};"
    yield "       updateTimer();"
    yield "       var tick = function() { countDown(); updateTimer(); };" 
    yield "       window.setInterval(tick, 1000); " 
    yield "   });" 
    yield "</script>" }

let private timerElement () = "<div id='timer'></div>"

let private buttonPage (pokemon: PokeRegistration) = seq {
    yield "<html>"
    yield "   <head>" 
    yield (timer pokemon)  |> Seq.reduce (+)
    yield "   </head>" 
    yield "   <body>"
    yield (pokeForm pokemon.Name) |> Seq.reduce (+)
    yield timerElement() 
    yield "   </body>" 
    yield "</html>" }

let private getPokePage (id: Guid) =
    match PokeMappings.GetById id with
    | Some pokemon -> OK ((buttonPage pokemon) |> Seq.reduce (+))
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
