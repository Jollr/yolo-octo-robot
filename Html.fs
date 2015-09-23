module Html

let private constructPage (head: string, body: string) = seq {
    yield "<html>"
    yield " <head>"
    yield head
    yield " </head>"
    yield " <body>"
    yield body
    yield " </body>"
    yield "</html>"
}

let Page (head: string, body: string) = 
    constructPage(head, body)
    |> Seq.reduce (+) 

let PageSq (head: seq<string>, body: seq<string>) = 
    constructPage(head |> (Seq.reduce (+)), body |> (Seq.reduce (+)))
    |> Seq.reduce (+) 