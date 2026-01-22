// Define a customer. A customer is a _AND_ type.
//
// Now we are going to model eligibility as a domain concept.

// We model a Customer using a _discriminated union_. This modeling
// captures the idea that a customer can be one of a(n):
//
// EligibleRegistered
// Registered
// Guest
//
// These types allow us to simplify our types. That is, we no longer
// need the type definitions for registered and unregistered customers.
type Customer =
    | EligibleRegistered of Id: string
    | Registered of Id: string
    | Guest of Id: string

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
let john = EligibleRegistered "John"
let mary = EligibleRegistered "Mary"
let richard = Registered "Richard"
let sarah = Guest "Sarah"

let assertJohn = (calculateTotal john 100.00M = 90.00M)
let assertMary = (calculateTotal mary 99.00M = 99.00M)
let assertRichard = (calculateTotal richard 100.00M = 100.00M)
let assertSarah = (calculateTotal sarah 100.00M = 100.00M)
