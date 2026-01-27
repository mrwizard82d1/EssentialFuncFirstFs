namespace MyProject

// The `Customer` type is used in two different modules, so it is
// defined in the scope of the **namespace** and outside **both**
// modules. Because the modules are defined in the namespace, they
// have access to the `Customer` type without needing to import it.
type Customer = {
    CustomerId : string
    IsRegistered : bool
    IsEligible : bool
}
            
module Db =
    
    let tryGetCustomer customerId =
        try
            [
                {
                    CustomerId = "John"
                    IsRegistered = true
                    IsEligible = true
                }
                {
                    CustomerId = "Mary"
                    IsRegistered = true
                    IsEligible = true
                }
                {
                    CustomerId = "Richard"
                    IsRegistered = true
                    IsEligible = false
                }
                {
                    CustomerId = "Sarah"
                    IsRegistered = false
                    IsEligible = false
                }
            ]
            |> List.tryFind (fun c -> c.CustomerId = customerId)
            |> Ok
        with
        | ex -> Error ex
        
    let saveCustomer (_customer: Customer) =
        try
            // Save customer
            Ok ()
        with
        | ex -> Error ex
            
module Domain =

    let trySaveCustomer customer =
        match customer with
        | Some c -> Db.saveCustomer c
        | None -> Ok()
    
    let convertToEligible customer =
        if not customer.IsEligible then
            { customer with IsEligible = true }
        else
            customer
            
    let upgradeCustomer customerId =
        // Implicitly returns the upgraded customer as a result of
        // the pipeline
        customerId
        |> Db.tryGetCustomer
        |> Result.map (Option.map convertToEligible)
        |> Result.bind trySaveCustomer

    let createCustomer customerId = {
        CustomerId = customerId
        IsRegistered = true
        IsEligible = false
    }
    
    // We need to create a new function, `tryCreateCustomer` to integrate
    // `Db.tryGetCustomer` and `createCustomer`.
    let tryCreateCustomer customerId (customer: Customer option) =
        try
            match customer with
            | Some _ -> raise (exn $"Customer '{customerId}' already exists.")
            | None -> Ok (createCustomer customerId)
        with
        | ex -> Error ex
    
    let registerCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.bind (tryCreateCustomer customerId)
        // We have repaired the issues integrating `createCustomer`, but
        // not `Db.saveCustomer` has an issue the result of the pipeline.
        |> Db.saveCustomer
