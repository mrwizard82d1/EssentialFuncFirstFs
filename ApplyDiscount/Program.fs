// Define a customer. A customer is a _AND_ type.
//
// Now we are going to model eligibility as a domain concept.

// We modify (and simplify) our records for registered and unregistered customers.
type RegisteredCustomer = { Id: string }

type UnregisteredCustomer = { Id: string }

// We model a Customer using a _discriminated union_. This modeling
// captures the idea that a customer can be one of a(n):
//
// EligibleRegistered
// Registered
// Guest
type Customer =
    | EligibleRegistered of RegisteredCustomer
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

// Calculates the discount.
//
let calculateTotal customer spend =
    let discount =
        match customer with
        | EligibleRegistered _ when spend >= 100.00M ->
            spend * 0.10M
        | _ ->
            0.00M
    spend - discount
    
// Create many customers and calculate their discounts
let john = EligibleRegistered { Id = "John"}
let mary = EligibleRegistered { Id = "Mary" }
let richard = Registered { Id = "Richard" }
let sarah = Guest { Id = "Sarah" }

let assertJohn = (calculateTotal john 100.00M = 90.00M)
let assertMary = (calculateTotal mary 99.00M = 99.00M)
let assertRichard = (calculateTotal richard 100.00M = 100.00M)
let assertSarah = (calculateTotal sarah 100.00M = 100.00M)
