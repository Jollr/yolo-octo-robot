module PokeRegistration

open System

type PokeRegistration(pokeName: string, x: int, y: int, id: Guid) =
    let mutable LastTrigger = DateTime.UtcNow
    
    member this.Cooldown () = 
        let elapsed = DateTime.UtcNow.Subtract(LastTrigger).TotalSeconds
        if elapsed >= 60.0
        then 0
        else 60 - (int)elapsed

    member private this.CanTrigger () = this.Cooldown() <= 0

    member this.X = x
    member this.Y = y
    member this.Id = id
    member this.Name = pokeName

    member this.Trigger () = 
        if (this.CanTrigger()) 
        then 
            LastTrigger <- DateTime.UtcNow
            true
        else false