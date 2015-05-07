module PokeRegistration

open System

type PokeRegistration(pokeName: string, x: int, y: int, id: Guid) =
    let mutable LastTrigger = DateTime.UtcNow
    
    let Cooldown () = 
        let elapsed = DateTime.UtcNow.Subtract(LastTrigger).Seconds
        if elapsed >= 60
        then 0
        else 60 - elapsed

    member private this.CanTrigger () = Cooldown() = 0

    member this.X = x
    member this.Y = y
    member this.Id = id
    member this.Name = pokeName
    member this.GetCooldown = Cooldown

    member this.Trigger () = 
        if (this.CanTrigger()) 
        then 
            LastTrigger <- DateTime.UtcNow
            true
        else false