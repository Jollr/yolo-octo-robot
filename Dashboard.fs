module Dashboard

open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful

let puzzleSquare x y = seq { 
    yield "<div class='puzzle-square'><img src='puzzle/" 
    yield x.ToString() 
    yield "/" 
    yield y.ToString() 
    yield "/img'/></div>" }

let puzzleRow width y = seq {
    yield "<div class='puzzle-row'>" 
    yield! (seq { 0 .. (width - 1)} |> Seq.map ( fun(i) -> puzzleSquare i y) |> Seq.collect id)
    yield "</div>" }

let puzzle width height = seq {
    yield "<div class='puzzle'>"
    yield! (seq { 0 .. (height - 1) } |> Seq.map ( fun(i) -> puzzleRow width i) |> Seq.collect id)
    yield "</div>" }

let dashboardPage puzzleWidth puzzleHeight =
    "<html>" +
    "    <head>" +
    "        <title>Gotta catch'em all!</title>" +
    "    </head>" +
    "    <body>" +
    "        <div class='dashboard-container'>" +
    Seq.reduce (+) (puzzle puzzleWidth puzzleHeight) +
    "        </div>" +
    "    </body>" +
    "</html>"

let dashboardBindings = 
    [ GET >>= choose [ path "/dashboard" >>= OK (dashboardPage 5 5) ] ]