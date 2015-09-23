module GuidScan

open System
open Suave
open Suave.Http
open Suave.Types

let guidScan (prefix: string) (h : Guid ->  WebPart) : WebPart =
    let scan (url: string) =
        let prefixPart = url.Substring(0, prefix.Length)

        if (prefix = prefixPart)
        then 
            try
                let idPart = url.Substring(prefix.Length, 36)
                let id = Guid.Parse(idPart)
                Some id
            with _ -> None
        else None

    let F (r:HttpContext) =
        match scan r.request.url.AbsolutePath with
        | Some p ->
            let part = h p
            part r 
        | None -> 
            fail

    F