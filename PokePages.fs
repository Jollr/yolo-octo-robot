module PokePages

open Suave.Http.Successful

let buttonElement pokeName = 
    "<button type='submit'><img src='" + pokeName + ".png'/></button>"

let pokePage pokeName = "<html><head></head><body><form method='POST'>" + (buttonElement pokeName) + "</form></body></html>" 

let pokeButton pokeName = 
    printf "%s" pokeName
    OK pokeName