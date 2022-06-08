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
