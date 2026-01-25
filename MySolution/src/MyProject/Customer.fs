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
        
    let saveCustomer (customer: Customer) =
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
            
    // This function contains an error that we will fix
    let upgradeCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> convertToEligible
        |> Db.saveCustomer
