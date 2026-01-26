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
    
    let convertToEligible customer =
        if not customer.IsEligible then
            { customer with IsEligible = true }
        else
            customer

    let trySaveCustomer customer =
        match customer with
        | Some c -> Db.saveCustomer c
        | None -> Ok()
            
    let upgradeCustomer customerId =
        let getCustomerResult = Db.tryGetCustomer customerId
        let converted =
            match getCustomerResult with
            | Ok (Some c) -> Ok (Some (convertToEligible c))
            | Ok None -> Ok None
            | Error ex -> Error ex
            
        let result =
            match converted with
            | Ok c -> trySaveCustomer c
            | Error ex -> Error ex
        result 
