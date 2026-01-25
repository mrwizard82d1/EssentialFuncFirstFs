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
            
    // Let's fix this function.
    //
    // We start by expanding our pipeline and exposing the
    // intermediate values.
    let upgradeCustomer customerId =
        let getCustomerResult = Db.tryGetCustomer customerId
        // Here lies the problem. Let's deconstruct the Result and
        // the Option from `getCustomerResult`
        // 
        // The following code from the book produces a compiler
        // error in F# 6. 
        // let convertedCustomer =
        //     match getCustomerResult with
        //     | Ok c ->
        //         match c with
        //         | Some c ->
        //             Some (convertToEligible c)
        //         | None -> None
        //     | Error ex -> Error ex
            
        // Google Gemini suggested the following code, which works.
        // I think it has the same semantics but "we will see."
        let convertedCustomer =
            match getCustomerResult with
            | Ok (Some c) -> Ok (Some (convertToEligible c))
            | Ok None -> Ok None
            | Error ex -> Error ex
            
        // Of course, we now have an error passing `convertedCustomer`
        // to `Db.saveCustomer`.
        //
        // Let's fix this error.
        let result =
            match convertedCustomer with
            | Ok (Some c) ->
                Db.saveCustomer c
            | Ok None -> Ok()
            | Error ex -> Error ex
        result 

// Finally, the code in this file compiles without error.
