open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Web
open Suave.Types

open System.Net

let config =
  { defaultConfig with
     bindings = [ HttpBinding.mk' HTTP "192.168.1.12" 80 ] }

let app =
  choose
    [ GET >>= choose
        [ path "/kevin" >>= OK "Hallo Kevin!"
          path "/ryanne" >>= OK "Hallo Ryanne!" ] ]

startWebServer config app