module Puzzle

let width = 5
let height = 3

let mutable puzzleGrid : bool[,] = Array2D.init<bool> width height (fun x y -> false)

let get (x:int) (y:int) : bool = puzzleGrid.GetValue(x, y) :?> bool

let private flip x y = puzzleGrid.[x, y] <- not (get x y)

let private flipUp x y = flip x (y-1)
let private flipRight x y = flip (x+1) y
let private flipLeft x y = flip (x-1) y
let private flipDown x y = flip x (y+1)

let trigger x y =
    if x < 0 then raise (System.ArgumentException("x"))
    if y < 0 then raise (System.ArgumentException("y"))
    if x >= width then raise (System.ArgumentException("x"))
    if y >= height then raise (System.ArgumentException("y"))
    if x > 0 then flipLeft x y
    if y > 0 then flipUp x y
    if x < width - 1 then flipRight x y    
    if y < height - 1 then flipDown x y