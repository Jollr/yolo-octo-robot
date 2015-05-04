module Dashboard

open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files

open Puzzle

let refresher = seq {
    yield "<script>"
    yield " var refreshImage = function(image) { " 
    yield "     var d = new Date();"
    yield "     var src = image.attr('src').split('?')[0] + '?' + d.getTime();"
    yield "     image.attr('src', src);"
    yield " };"
    yield " var refreshImages = function() { $('img').each( function(i, elem) { refreshImage($(elem)); }); };"
    yield " $(function() { "
    yield "     window.setInterval(refreshImages, 5000);"
    yield " } );"
    yield "</script>"}

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
    "<html>" +
    "    <head>" +
    "        <title>Gotta catch'em all!</title>" +
    "        <script type='text/javascript' src='jquery'></script>" +
    "        <link type='text/css' href='dashboard/stylesheet.css' rel='stylesheet'>" +
    Seq.reduce (+) refresher +
    "    </head>" +
    "    <body>" +
    "        <div class='dashboard-container'>" +
    Seq.reduce (+) (puzzle puzzleWidth puzzleHeight) +
    "        </div>" +
    "    </body>" +
    "</html>"

let imageFilename x y = "img/puzzle" + x.ToString() + y.ToString() + ".png"

let image x y =
    match Puzzle.get x y with
    | true -> file (imageFilename x y)
    | false -> file "img/white.png"

let dashboardBindings = 
    [ GET >>= choose [ 
        path "/dashboard" >>= OK (dashboardPage Puzzle.width Puzzle.height) 
        pathScan "/dashboard/%d/%d/img" ( fun (x, y) -> image x y ) 
        path "/dashboard/stylesheet.css" >>= file "dashboard.css" ] ]