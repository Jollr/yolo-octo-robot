module Dashboard

open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Types

open Html
open Puzzle

let puzzleSquare x y = seq { 
    yield "<div class='puzzle-square'><img src='dashboard/" 
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
    let head = seq {
        yield "<title>Gotta catch'em all!</title>"
        yield "<script type='text/javascript' src='jquery'></script>"
        yield "<script type='text/javascript' src='dashboard/dashboard.js'></script>"
        yield "<link type='text/css' href='dashboard/stylesheet.css' rel='stylesheet'>"
    }
    let body = seq {
        yield "<div class='dashboard-container'>"
        yield Seq.reduce (+) (puzzle puzzleWidth puzzleHeight)
        yield "</div>"
    }
    Html.PageSq(head, body)

let imageFilename x y = "img/puzzle" + x.ToString() + y.ToString() + ".png"

let image x y =
    match Puzzle.get x y with
    | true -> file (imageFilename x y)
    | false -> file "img/white.png"

let puzzleState () = 
    let xs = [0 .. Puzzle.width - 1]
    let ys = [0 .. Puzzle.height - 1]
    let state x y = 
        match Puzzle.get x y with
            | true -> "1"
            | false -> "0"
    let rowState y = xs |> List.map (fun x -> state x y)
    ys |> List.collect rowState |> List.reduce (+) |> OK

let dashboardBindings = 
    [ GET >>= choose [ 
        path "/dashboard" >>= OK (dashboardPage Puzzle.width Puzzle.height) 
        pathScan "/dashboard/%d/%d/img" ( fun (x, y) -> image x y ) 
        path "/dashboard/stylesheet.css" >>= file "dashboard.css"
        path "/dashboard/dashboard.js" >>= file "dashboard.js" 
        path "/dashboard/puzzleState" >>= context (fun x -> puzzleState()) ] ]
        