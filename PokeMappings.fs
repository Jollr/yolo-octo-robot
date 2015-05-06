module PokeMappings

open System

type PokeRegistration(pokeName: string, x: int, y: int, id: Guid) =
    let mutable LastTrigger = DateTime.UtcNow
    member this.X = x
    member this.Y = y
    member this.Id = id
    member this.Name = pokeName
    
    member this.Trigger () = 
        let triggered = DateTime.UtcNow.Subtract(LastTrigger) > TimeSpan.FromMinutes(1.0)
        if (triggered) then LastTrigger <- DateTime.UtcNow
        triggered

let private Registrations = seq {
        yield new PokeRegistration("bulbasaur", 1, 1, Guid.Parse("0cf823bb-6907-430b-9851-7ae6cbbc7f46"))
        yield new PokeRegistration("charizard", 2, 1, Guid.Parse("01d544b2-cdfd-481d-8b67-58daef9c1337"))
    }

let GetById (id: Guid) = 
    Registrations 
    |> Seq.filter (fun r -> r.Id.Equals(id)) 
    |> fun (x) -> 
        if (Seq.isEmpty x) 
            then None 
            else Some (Seq.head x)

//0cf823bb-6907-430b-9851-7ae6cbbc7f46
//01d544b2-cdfd-481d-8b67-58daef9c1337
//291ba5a0-9f9a-4d57-a05d-69199a9b4c1a
//099ff636-4b85-419b-8e04-70fb552d9e00
//8fb52255-caba-45d0-ac22-f6b3e7b78fa2
//b788b51e-6711-47e5-aeb5-97dd78d17d11
//6c33bb0d-c6b8-42a1-bd7e-293960baaefb
//0fe7fb04-58ad-460d-a18e-c854d57633ed
//a0b5ab01-8f54-41b6-9b25-6246167840ad
//0f532704-3639-42c3-bee6-255f391e0ca7
//36dba1b8-99ab-4963-bcc1-9cc2837c3896
//ab70396c-e10e-41c7-8108-0d6a64e786cb
//0dcf68d7-1f28-45b5-9da3-f95b74d8a2d6
//0eaeb147-91f2-4f2b-a6e8-10dd6c977d69
//5c8a9ede-2229-4312-88c8-e80f705077eb
//fe1a5473-6534-4c9f-9f18-e52e328474c3
//3a348b56-ae12-4eb2-a26d-f66c7b48e7eb
//6dfdbcc9-ceec-4f98-8cf8-5f8d9557c824
//d9e51bf3-fa8c-46ab-a85a-e1036f17daaa
//14545496-3219-4fa0-98cb-ed691df0a37c
//adaf9c4b-9dd9-4def-a98d-9e6b0388586b
//49e4003a-e5f2-4759-a9d7-08e420155c80
//0ac35371-8f62-4c4f-813e-b5c98e6750a1
//d829058e-8ea3-4017-8954-92ff0d6365ea
//7cbd690e-4428-418c-b794-02b687f89d17