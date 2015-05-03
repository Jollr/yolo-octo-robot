module Puzzle

let width = 5
let height = 4

let mutable puzzleGrid : bool[,] = array2D [ [true; false; true; true; true]; [true; false; true; true; true]; [false; true; false; true; true]; [false; true; false; true; true] ]

let get (x:int) (y:int) : bool = puzzleGrid.GetValue(x, y) :?> bool

let private flip x y = puzzleGrid.SetValue( not (get x y), x, y)

let private flipUp x y = flip x (y-1)
let private flipRight x y = flip (x+1) y
let private flipLeft x y = flip (x-1) y
let private flipDown x y = flip x (y+1)

let trigger x y =
    if not (x <= 0) then flipLeft x y
    if not (x >= width) then flipRight x y    
    if not (y <= 0) then flipUp x y
    if not (y >= height) then flipDown x y

