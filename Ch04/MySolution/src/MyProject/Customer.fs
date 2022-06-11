namespace MyProject


type Customer = {
    CustomerId: string
    IsRegistered: bool
    IsEligible: bool
}

// Model a phony datastore
module Db =
    // String -> Result<Customer option, ex>
    let tryGetCustomer customerId = 
        try
            [
                { CustomerId = "John"; IsRegistered = true; IsEligible = true }
                { CustomerId = "Mary"; IsRegistered = true; IsEligible = true }
                { CustomerId = "Richard"; IsRegistered=true; IsEligible=false }
                { CustomerId = "Sarah"; IsRegistered = false; IsEligible = false }
            ]
            |> List.tryFind (fun c -> c.CustomerId = customerId)
            |> Ok
        with 
        | ex -> Error ex

    // Customer -> Result<unit, ex>
    let saveCustomer (customer:Customer) =
        try
            // In real implementation, actually save customer.
            Ok ()
        with
        | ex -> Error ex

module Domain =
    // Customer -> Customer
    let convertToEligible customer =
        if not customer.IsEligible then { customer with IsEligible = true }
        else customer

    // CustomerId -> Result
    let upgradeCustomer customerId =
        customerId
        // To fix the two issues below, we remove the pipeline 
        |> Db.tryGetCustomer
        // This implementation has a problem. The previous function in the pipelien returns a 
        // Result<Customer option, exn> but `convertToEligible` expects a `Customer`. In the 
        // world of Scott Wlaschin, `Db.tryGetCustomer` moves from "normal world" to 
        // "Result world," but `convertToEligible` is maps from "normal world" to "normal world."
        // We need an "adapter block." 
        |> convertToEligible
        // Note that once we fix the issue with `convertToEligible` ("Result world" to 
        // "Result world"), we will have a problem with `Db.saveCustomer` which maps from 
        // "real world" to "Result world."
        |> Db.saveCustomer
