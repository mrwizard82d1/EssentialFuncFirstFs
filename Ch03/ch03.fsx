// Function Composition with Result

// Repeat the code from chapter 01 using the `Result` type instead of 
// raising exceptions.

open System

type Customer = {
    Id: int
    IsVip: bool
    Credit: decimal
}

// Customer -> Result<Customer * decimal, exc>
let getPurchases customer = 
    try
        // Imagine this function fetching a Customer from a database
        let purchases =
            if customer.Id % 2 = 0
            then (customer, 120M)
            else (customer, 80M)
        Ok purchases
        with
        | ex -> Error ex

// Customer * decimal -> Customer
let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M
    then { customer with IsVip = true }
    else customer

// Customer -> Result<Customer, exc)>
let increaseCreditIfVip customer =
    try
        // Imagine that this function could cause an exeption
        let credit = 
            if customer.IsVip then 100.0M else 50.0M
        Ok { customer with Credit = customer.Credit + credit }
        with
        | ex -> Error ex

// Note the "squiggles" beneath `tryPromoteToVip` because of the Type
// mismatch (between the functions due to the use of `Result`).
let badUpgradeCustomer1 customer =
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip

let customerVip = { Id = 1; IsVip = true; Credit = 0.0M }
let customerStd = { Id = 2; IsVip = false; Credit = 100.0M}

let assertVip = badUpgradeCustomer1 customerVip = Ok { Id = 1; IsVip = true; Credit = 100.0M }
let assertStd = badUpgradeCustomer1 customerStd = Ok { Id = 2; IsVip = false; Credit = 50.M }

// Because the function signatures of `getPurchase`, `tryPromoteToVip`, and 
// `increaseCreditIfVip` do not match, `upgradeCustomer` does not compile.
// We will fix this issue by using a higher-order function.

// Scott Wlaschin describes using `Result` as "Railway Oriented Programming"
// or ROP. In this style, imagine code as proceeding along one of two tracks:
// the `Ok` track or the `Error` track. In his terminology, he describes the
// `tryPromoteToVip` function as a "one-track function" because it *does not*
// return a value of type `Result` and will *only* execute on the `Ok` track.

// To "fix" this issue, we create a function that converts a non-`Result`,
// one-track function into a `Result`, two-track function. something like
// the following.
// ('a -> 'b) -> Result<'a, 'c> -> Result<'b, 'c>
let map oneTrackFunction resultInput =
    match resultInput with
    | Ok s -> Ok (oneTrackFunction s)
    | Error f -> Error f

let badUpgradeCustomer2 customer =
    customer
    |> getPurchases
    |> map tryPromoteToVip
    |> increaseCreditIfVip

// The function, `badUpgradeCustomer2`, does not compile either because 
// `increaseCreditIfVip` expects a `Customer` as input but is now receiving
// a `Result<Customer, exc>`. Using the ROP terminology, `increaseCreditIfVip`
// is a switch function; that is, it has a `Result` output but *does not*
// handle the `Error` case. We will again solve this issue by a higher-order
// function.

// ('a -> Result<'b, 'c>) -> Result<'a, 'c> -> Result<'b, 'c>
let bind switchFunction resultInput =
    match resultInput with
    | Ok s -> switchFunction s
    | Error f -> Error f

let upgradeCustomerByHand customer =
    customer
    |> getPurchases
    |> map tryPromoteToVip
    |> bind increaseCreditIfVip

// The code now has *no* compiler warings, and the previous assert 
// expressions now work.

let assertVip = upgradeCustomerByHand customerVip = Ok { Id = 1; IsVip = true; Credit = 100.0M }
let assertStd = upgradeCustomerByHand customerStd = Ok { Id = 2; IsVip = false; Credit = 50.M }

// Although we wrote `map` and `bind` ourselves, the built-in `Result` type
// has these methods as members.

let upgradeCustomer customer =
    customer
    |> getPurchases
    |> Result.map tryPromoteToVip
    |> Result.bind increaseCreditIfVip

let assertVip = upgradeCustomerByHand customerVip = Ok { Id = 1; IsVip = true; Credit = 100.0M }
let assertStd = upgradeCustomerByHand customerStd = Ok { Id = 2; IsVip = false; Credit = 50.M }