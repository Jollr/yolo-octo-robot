module Dashboard

let puzzleSquare x y = "<div class='puzzle-square'><img src='puzzle/" + x.ToString() + "/" + y.ToString() + "/img'/></div>"

let puzzleRow width y = 
    "<div class='puzzle-row'>" +
    (seq { 0 .. (width - 1)} |> Seq.map ( fun(i) -> puzzleSquare i y) |> Seq.reduce (+)) +
    "</div>"

let puzzle width height = 
    "<div class='puzzle'>" +
    (seq { 0 .. (height - 1)} |> Seq.map ( fun(i) -> puzzleRow width i) |> Seq.reduce (+)) +
    "</div>" 

let dashboardPage puzzleWidth puzzleHeight =
    "<html>" +
    "    <head>" +
    "        <title>Gotta catch'em all!</title>" +
    "    </head>" +
    "    <body>" +
    "        <div class='dashboard-container'>" +
    puzzle puzzleWidth puzzleHeight +
    "        </div>" +
    "    </body>" +
    "</html>"
