open System

// Using `Option<'T>` to address parsing errors

let tryParseDateTime (input: string) =
    let success, value = DateTime.TryParse input
    if success then
        Some value
    else
        None

tryParseDateTime "2026-01-23"
tryParseDateTime "2026-01-23T18:08:27.355"
tryParseDateTime "20260123T180827.355"

let tryParseDateTimeMatch (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None

tryParseDateTimeMatch "2026-01-23"
tryParseDateTimeMatch "2026-01-23T18:08:27.355"
tryParseDateTimeMatch "20260123T180827.355"

// Using `Option<'T>' to handle possibly missing data

type PersonName = {
    FirstName: string
    MiddleName: string option
    LastName: string
}

let person = { FirstName = "Ian"; MiddleName = None; LastName = "Russel" }
person

let anotherPerson = { person with MiddleName = Some "James" }
anotherPerson

// Remember that typical .NET (C#) code **allows null**

let nullObj: string = null
nullObj

let nullPrimitive = Nullable<int>()
nullPrimitive

// To convert from a .NET `null` object to an F# Option type
let fromNullObj = Option.ofObj nullObj
fromNullObj

let fromNullPrimitive = Option.ofNullable nullPrimitive
fromNullPrimitive

// Convert from an Option type to something that **does not**
// expect null (or `None`)

// Using a match expression
let patternMatchResult input =
    match input with
    | Some value -> value
    | None -> "------"
    
patternMatchResult (Some "Not null")
patternMatchResult None

// Using Option.defaultValue
let result = Option.defaultValue "------" fromNullObj
result

// Another alternative is to use the forward pipe operator
let pipeResult = fromNullObj |> Option.defaultValue "------"
pipeResult

// Using a simple expression works, but if this usage is common,
// a function is probably better
let setUnknownAsDefault = Option.defaultValue "------"
setUnknownAsDefault fromNullObj

// Or, again, using the forward pipe operator
let pipeUnknownAsDefault = fromNullObj |> setUnknownAsDefault
pipeUnknownAsDefault
