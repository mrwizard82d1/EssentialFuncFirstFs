// Handling Exceptions

open System

// decimal -> decimal -> decimal
let ambiguousTryDivide (x:decimal) (y:decimal) =
    try
        x / y
    with
    | :? DivideByZeroException as ex -> raise ex

// The `ambiguousTryDivide` function signature "lies"; that is, it does not
// always return a value of type `decimal`. If the value bound to `y` is
// 0 (zero), the function raises an exception instead of returning a value.

// F# provides the `Result` type to handle this situation. The `Result` type
// is a discriminated union of `Ok` and `Error` values.
// decimal -> decimal -> Result(decimal, 'a)
let tryDivide (x:decimal) (y:decimal) =
    try
        Ok (x / y)
    with
    | :? DivideByZeroException as dbze -> Error dbze

tryDivide 1m 2m
tryDivide 1m 0m

let tryDivideAlt (x:decimal) (y:decimal) =
    try Ok (x/y) with
    | :? DivideByZeroException as dbze -> Error dbze

tryDivideAlt 1m 2m
tryDivideAlt 1m 0m

// Note that `tryDivideAlt` is *not* idiomatic F# code; the implementation
// of `tryDivide` *is* idiomatic F# code.

let badDivide = tryDivide 1m 0m
let goodDivide = tryDivide 1m 2m