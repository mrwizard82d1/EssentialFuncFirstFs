// Copied from the previous chapter with minor changes
// to illustrate the use of `Result`.
open System

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Emulate getting purchases for a customer from a database /
// data source.
let getPurchases customer =
    try
        // Imagine this function is fetching data from a database.
        let purchases =
            if customer.Id % 2 = 0 then
                (customer, 120M)
            else
                (customer, 80M)
        Ok purchases
    with
    | ex -> Error ex
 
 // Try to promote the customer to a VIP customer.
let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M then
        { customer with IsVip = true }
    else
        customer
        
// Increase the credit limit of the customer if the customer is a VIP.
let increaseCreditIfVip customer =
    try
        // Imagine this function could cause an exception.
        let credit =
            if customer.IsVip then
                100.0M
            else
                50.0M
        Ok { customer with Credit = customer.Credit + credit }
    with
    | ex -> Error ex
    
// Attempt to upgrade a customer
let upgradeCustomer customer =
    customer
    |> getPurchases
    // Compiler problem!
    //
    // The previous expression in the pipeline has the type,
    // `Result<Customer * decimal, exn>`. However, `tryPromoteToVip`
    // expects a `Customer` as input. Consequently, the compiler
    // complains.
    |> tryPromoteToVip // Compiler problem
    |> increaseCreditIfVip
    
let customerVip = { Id = 1; IsVip = true; Credit = 0.0M }
let customerStandard = { Id = 2; IsVip = false; Credit = 100.0M }

let assertVip = upgradeCustomer customerVip =
    Ok { Id = 1; IsVip = true; Credit = 100.0M }
let assertStandardToVip = upgradeCustomer customerStandard =
    Ok { Id = 2; IsVip = true; Credit = 200.0M }
let assertStandard = upgradeCustomer { customerStandard with Id = 3
                                       Credit = 50.0M } =
    Ok { Id = 3; IsVip = false; Credit = 100.0M }
