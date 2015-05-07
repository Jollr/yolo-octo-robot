module PokeMappings

open System
open PokeRegistration

let mutable private Registrations = [| |]

let Initialize () = 
    Registrations <- [|
        new PokeRegistration("Bulbasaur", 0, 0, Guid.Parse("0cf823bb-6907-430b-9851-7ae6cbbc7f46"))
        new PokeRegistration("Squirtle", 0, 1, Guid.Parse("01d544b2-cdfd-481d-8b67-58daef9c1337"))
        new PokeRegistration("Charmander", 0, 2, Guid.Parse("291ba5a0-9f9a-4d57-a05d-69199a9b4c1a"))
        new PokeRegistration("Caterpie", 0, 3, Guid.Parse("099ff636-4b85-419b-8e04-70fb552d9e00"))
        new PokeRegistration("Arbok", 0, 4, Guid.Parse("8fb52255-caba-45d0-ac22-f6b3e7b78fa2"))
        new PokeRegistration("Pikachu", 1, 0, Guid.Parse("b788b51e-6711-47e5-aeb5-97dd78d17d11"))
        new PokeRegistration("Psyduck", 1, 1, Guid.Parse("6c33bb0d-c6b8-42a1-bd7e-293960baaefb"))
        new PokeRegistration("Alakazam", 1, 2, Guid.Parse("0fe7fb04-58ad-460d-a18e-c854d57633ed"))
        new PokeRegistration("Gengar", 1, 3, Guid.Parse("a0b5ab01-8f54-41b6-9b25-6246167840ad"))
        new PokeRegistration("Cubone", 1, 4, Guid.Parse("0f532704-3639-42c3-bee6-255f391e0ca7"))
        new PokeRegistration("Koffing", 2, 0, Guid.Parse("36dba1b8-99ab-4963-bcc1-9cc2837c3896"))
        new PokeRegistration("Jynx", 2, 1, Guid.Parse("ab70396c-e10e-41c7-8108-0d6a64e786cb"))
        new PokeRegistration("Magikarp", 2, 3, Guid.Parse("0dcf68d7-1f28-45b5-9da3-f95b74d8a2d6"))
        new PokeRegistration("Ditto", 2, 4, Guid.Parse("0eaeb147-91f2-4f2b-a6e8-10dd6c977d69"))
        new PokeRegistration("Dragonite", 3, 0, Guid.Parse("5c8a9ede-2229-4312-88c8-e80f705077eb"))
        new PokeRegistration("Mewtwo", 3, 1, Guid.Parse("fe1a5473-6534-4c9f-9f18-e52e328474c3"))
        new PokeRegistration("Mew", 3, 2, Guid.Parse("3a348b56-ae12-4eb2-a26d-f66c7b48e7eb"))
        new PokeRegistration("Chikorita", 3, 3, Guid.Parse("6dfdbcc9-ceec-4f98-8cf8-5f8d9557c824"))
        new PokeRegistration("Cindaquil", 3, 4, Guid.Parse("d9e51bf3-fa8c-46ab-a85a-e1036f17daaa"))
        new PokeRegistration("Toatodile", 4, 0, Guid.Parse("14545496-3219-4fa0-98cb-ed691df0a37c"))
        new PokeRegistration("Togepi", 4, 1, Guid.Parse("adaf9c4b-9dd9-4def-a98d-9e6b0388586b"))
        new PokeRegistration("Raikou", 4, 2, Guid.Parse("49e4003a-e5f2-4759-a9d7-08e420155c80"))
        new PokeRegistration("Entei", 4, 3, Guid.Parse("0ac35371-8f62-4c4f-813e-b5c98e6750a1"))
        new PokeRegistration("Suicune", 4, 4, Guid.Parse("d829058e-8ea3-4017-8954-92ff0d6365ea"))
    |]


let GetById (id: Guid) = 
    Registrations 
    |> Seq.filter (fun r -> r.Id.Equals(id)) 
    |> fun (x) -> 
        if (Seq.isEmpty x) 
            then None 
            else Some (Seq.head x)
