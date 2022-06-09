open System

// This implementation is "most verbose"
// let tryParseDateTime (input:string) =
//     let (success, value) = DateTime.TryParse input
//     if success then Some value else None

// One need not surround the deconstructed tuple with parentheses
// let tryParseDateTime (input:string) =
//     let success, value = DateTime.TryParse input
//     if success then Some value else None

// One can also pattern match directly.
let tryParseDateTime (input:string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None 

// Any of these three styles is "acceptable."

let isDate = tryParseDateTime "2019-08-01"
let isNotDate = tryParseDateTime "Hello"

type PersonName = {
    FirstName: string
    MiddleName: string option
    LastName: string
}

let person = { FirstName = "Ian"; MiddleName = None; LastName = "Russel" }
printfn $"{person}"
let person2 = { person with MiddleName = Some "???" }
printfn $"{person2}"

// Interop with .NET

let nullObj:string = null
let nullPri = Nullable<int>()

let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

let resultPM input =
    match input with
    | Some value -> value
    | None -> "------"

resultPM fromNullObj
resultPM (Some "foo")

let result = Option.defaultValue "------" fromNullObj

// Use partial application to set a default value *without* an `Option`
let setUnknownAsDefault = Option.defaultValue "????"

setUnknownAsDefault fromNullObj
let anotherResult = fromNullObj |> setUnknownAsDefault
printfn $"{anotherResult}"
