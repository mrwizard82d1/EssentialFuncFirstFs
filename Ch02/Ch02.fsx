// In Practice

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Customer -> (Customer * decimal)
let getPurchases customer =
    let purchases = if customer.Id % 2 = 0 then 120M else 80M
    (customer, purchases)
    
// (Customer * decimal) -> Customer
let tryPromoteToVip purchases =
    let (customer, amount) = purchases
    if amount > 100M then { customer with IsVip = true } else customer
    
// Customer -> Customer
let increaseCreditIfVip customer =
    let increase = if customer.IsVip then 100M else 50M
    { customer with Credit = customer.Credit + increase }
     
// Composition operator
let upgradeCustomerComposed =
    getPurchases >> tryPromoteToVip >> increaseCreditIfVip // Customer -> Customer
    
// C# style
let upgradeCustomerNested customer =
    increaseCreditIfVip(tryPromoteToVip(getPurchases customer)) // Customer -> Customer
    
// Procedural
let upgradeCustomerProcedural customer = // Customer -> Customer
    let customerWithPurchase = getPurchases customer
    let promotedCustomer = tryPromoteToVip customerWithPurchase
    let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
    increasedCreditCustomer
    
// Forward pipe operator (preferred - according to this author)
let upgradeCustomerPiped customer = // Customer -> Customer
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip
    
// Test code
let customerVip = { Id = 1; IsVip = true; Credit = 0.0M }
let customerStd = { Id = 2; IsVip = false; Credit = 100.0M }

let assertVipComposed =
    upgradeCustomerComposed customerVip = { Id = 1; IsVip = true; Credit = 100.0M }

let assertStdToVipComposed =
    upgradeCustomerComposed customerStd = { Id = 2; IsVip = true; Credit = 200M }
    
let assertStdComposed =
    upgradeCustomerComposed {customerStd with Id = 3; Credit = 50.0M } = { Id = 3; IsVip = false; Credit = 100.0M }

let assertVipPiped =
    upgradeCustomerPiped customerVip = { Id = 1; IsVip = true; Credit = 100.0M }

let assertStdToVipPiped =
    upgradeCustomerPiped customerStd = { Id = 2; IsVip = true; Credit = 200M }
    
let assertStdPiped =
    upgradeCustomerPiped {customerStd with Id = 3; Credit = 50.0M } = { Id = 3; IsVip = false; Credit = 100.0M }

// Unit

open System

// unit -> System.DateTime
let now () = DateTime.UtcNow

// 'a -> unit
let logUnit msg =
    // Log a message or a similar task that *does not* return a value
    ()

now
now ()

// If you forget to include unit, `()`, in the function definition, you get a fixed value for `now`
let fixedNow = DateTime.UtcNow

fixedNow

// Wait a few seconds before executing the following linea. You will still (and always) get he same value.
fixedNow

// Partial Application
type LogLevel =
    | Error
    | Warning
    | Info

// LogLevel -> string -> unit
let log (level: LogLevel) message  =
    printfn $"[%A{level}]: %s{message}"

log Warning "This is a warning"

// To partially apply this function, supply a LogLevel
let logError = log Error // string -> unit

logError "This is an error"

let m1 = log Error "Curried function"
let m2 = logError "Partially applied function"

// Because the return type is unit, we need *not* let bind the result to a value
log Error "Curried function"
logError "Partially applied function"

// Partial application is only possible with *curried* parameters. It is *not possible* with tuple parameters
// because one must supply *all* the parameters at call time.

// (LogLevel * message) -> ()
let logTuple (level: LogLevel, message: string) =
    printfn $"[%A{level}]: %s{message}"

logTuple (Error, "C# function")

// Produces an error
let logErrorTuple = logTuple Error
