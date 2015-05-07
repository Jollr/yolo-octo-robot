module PokeHtml

open PokeRegistration

let private title (pokemon: PokeRegistration) = seq {
    yield "<title>"
    yield pokemon.Name
    yield "</title>" }

let private buttonElement (pokeName:string) = seq {
    yield "<button type='submit'><img src='" 
    yield pokeName 
    yield "/img'/></button>" }

let private pokeForm (pokemon: PokeRegistration) = seq {
    yield "<form method='POST'>" 
    yield (buttonElement pokemon.Name) |> Seq.reduce (+)
    yield "</form>" }

let private timer (pokemon: PokeRegistration) = seq {
    yield "<script type='text/javascript' src='jquery'></script>" 
    yield "<script>" 
    yield "   $(function() {" 
    yield "       var cooldown = " + (pokemon.Cooldown()).ToString() + ";" 
    yield "       var countDown = function() { if (cooldown > 0) {cooldown--;} };" 
    yield "       var timeString = function() { " 
    yield "           if (cooldown >= 60) { return '1:00'; }" 
    yield "           else if (cooldown <= 0) { return ''; }" 
    yield "           else if (cooldown < 10) { return '0:0' + cooldown; }" 
    yield "           else { return '0:' + cooldown; }" 
    yield "       };" 
    yield "       var updateTimer = function() {$('#timer').html(timeString());};"
    yield "       updateTimer();"
    yield "       var tick = function() { countDown(); updateTimer(); };" 
    yield "       window.setInterval(tick, 1000); " 
    yield "   });" 
    yield "</script>" }

let private timerElement () = "<div id='timer'></div>"

let ButtonPage (pokemon: PokeRegistration) = seq {
    yield "<html>"
    yield "   <head>" 
    yield (title pokemon) |> Seq.reduce (+)
    yield (timer pokemon)  |> Seq.reduce (+)
    yield "   </head>" 
    yield "   <body>"
    yield (pokeForm pokemon) |> Seq.reduce (+)
    yield timerElement() 
    yield "   </body>" 
    yield "</html>" }