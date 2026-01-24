open System

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
