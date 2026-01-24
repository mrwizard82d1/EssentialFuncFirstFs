open System

// Try to perform division but fail by raising an exception
let tryDivide (x: decimal) (y: decimal) =
    try
        x / y
    with
    | :? DivideByZeroException as ex ->
        raise ex
        
tryDivide 3.0M 4.0M
tryDivide 3.0M (1.0M - 1.0M)

// Try to perform a division operation but return a `Result`
//
// This code using an "expanded" `try...with` expression is idiomatic F#
let tryDivideResult (x: decimal) (y: decimal) =
    try
        Ok (x / y)
    with
    | :? DivideByZeroException as ex ->
        Error ex
        
tryDivideResult 3.0M 4.0M
tryDivideResult 3.0M (1.0M - 1.0M)
