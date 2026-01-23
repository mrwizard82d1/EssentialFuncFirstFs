open System

type Customer = {
    Id: int
    IsVip: bool
    Credit: decimal
}

// Define three functions that can be composed together.

let getPurchases customer =
    let purchase =
        if customer.Id % 2 = 0 then
            120M
        else
            80M
    (customer, purchase)
    
let tryPromoteToVip purchases =
    let (customer, amount) = purchases
    if amount > 100M then
        { customer with IsVip = true }
    else
        customer
    
let increaseCreditIfVip customer =
    let increase =
        if customer.IsVip then
            100M
        else
            50M
    { customer with Credit = customer.Credit + increase }

// F# supports **four** ways to compose these three functions together.

// Composition operator (`>>`)
let upgradeCustomerComposed =
    getPurchases >> tryPromoteToVip >> increaseCreditIfVip
    
// C# style
let upgradeCustomerNested customer =
    increaseCreditIfVip(tryPromoteToVip(getPurchases customer))
    
// Procedural
let upgradeCustomerProcedural customer =
    let customerWithPurchases = getPurchases customer
    let promotedCustomer = tryPromoteToVip customerWithPurchases
    let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
    increasedCreditCustomer
    
// Forward pipe operator (author recommends this "style" as the default)
let upgradeCustomerPiped customer =
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip
    
// All these functions have the same type signature,
// `Customer -> Customer`, and will produce the same result given
// the same input.

// Let's verify the output of the upgrade functions.
let customerVip = { Id = 1; IsVip = true; Credit = 0.0M }
let customerStd = { Id = 2; IsVip = false; Credit = 100.0M }

let assertVip =
    upgradeCustomerComposed customerVip = { Id = 1; IsVip = true; Credit = 100.0M }
let assertStdToVip =
    upgradeCustomerPiped customerStd = { Id = 2; IsVip = true; Credit = 200.0M }
let assertStd =
    upgradeCustomerNested { customerStd with Id = 3; Credit = 50.0M } =
        { Id = 3; IsVip = false; Credit = 100.0M}

// ### Unit

// Defines a **function** that, when evaluated, return the current time.
//
// Although it appears that this function takes no arguments, it
// actually takes **one** argument of type `unit`. 
let now () = DateTime.UtcNow

// Notice that the value `fixedNow` has a single, **unchanging** value.
let fixedNow = DateTime.UtcNow
fixedNow

let do_nothing_log msg =
    // Log a message or a similar task that **does not** return a value.
    ()
    
// ### Partial application

type LogLevel =
    | Error
    | Warning
    | Info
    
let log (level: LogLevel) message =
    printfn $"[%A{level}]: %s{message}"
    ()
    
// Logs an error but waits for the message.
let logError = log Error

let m1 = log Error "Curried function"
let m2 = log Error "Partially applied function"

log Error "Curried function"
logError "Partially applied function"

// Suppressing the result (`unit`)
logError "Error message" |> ignore

// Remember that supplying arguments as **tuples** prevents partial application.
let logTuple (level: LogLevel, message: string) =
    printfn $"[%A{level}]: %s{message}"
    ()
    
// Uncomment to see compiler error.
// logTuple Error

// Requires a tuple argument.
logTuple (Error, "Partially applied function")
