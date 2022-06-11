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
        let getCustomerResult = Db.tryGetCustomer customerId
        let converted = 
            match getCustomerResult with
            | Ok c ->
                match c with 
                | Some customer -> Some (convertToEligible customer)
                | None -> None
                |> Ok
            | Error ex -> Error ex 
        let output = 
            match converted with
            | Ok c -> 
                match c with
                | Some customer -> Db.saveCustomer customer
                | None -> Ok ()
            | Error ex -> Error ex
        output
