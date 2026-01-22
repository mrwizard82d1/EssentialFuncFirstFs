// Define a customer. A customer is a _AND_ type.
//
// This implementation allows an invalid state: a customer is eligible
// but not registered. Our next commit will address this issue.

// We create distinct records for registered and unregistered customers.
type RegisteredCustomer = {
    Id: string
    IsEligible: bool
}

type UnregisteredCustomer = {
    Id: string
}

// We model a Customer using a _discriminated union_. This modeling
// captures the idea that a customer can be either a registered
// customer or a guest (**not both**).
type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

// Calculates the discount.
//
// Simplify the logic with a guard clause.
let calculateTotal customer spend =
    let discount =
        match customer with
        | Registered c when c.IsEligible && spend >= 100.00M ->
            spend * 0.10M
        | _ ->
            0.00M
    spend - discount
    
// Create many customers and calculate their discounts
let john = Registered { Id = "John"; IsEligible = true }
let mary = Registered { Id = "Mary"; IsEligible = true }
let richard = Registered { Id = "Richard"; IsEligible = false }
let sarah = Guest { Id = "Sarah" }

let assertJohn = (calculateTotal john 100.00M = 90.00M)
let assertMary = (calculateTotal mary 99.00M = 99.00M)
let assertRichard = (calculateTotal richard 100.00M = 100.00M)
let assertSarah = (calculateTotal sarah 100.00M = 100.00M)
