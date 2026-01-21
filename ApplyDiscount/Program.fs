// Define a customer. A customer is a _AND_ type.
type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered: bool
}

// Calculates the discount.
let calculateTotal customer spend =
    let discount =
        if customer.IsEligible && spend >= 100.00M then
            spend * 0.10M
        else
            0.00M
    let total = spend - discount
    total
    
// Create many customers and calculate their discounts
let john = { Id = "John"; IsEligible = true; IsRegistered = true }
let mary = { Id = "Mary"; IsEligible = true; IsRegistered = true }
let richard = { Id = "Richard"; IsEligible = false; IsRegistered = true }
let sarah = { Id = "Sarah"; IsEligible = false; IsRegistered = false }

let assertJohn = (calculateTotal john 100.00M = 90.00M)
let assertMary = (calculateTotal mary 99.00M = 99.00M)
let assertRichard = (calculateTotal richard 100.00M = 100.00M)
let assertSarah = (calculateTotal sarah 100.00M = 100.00M)
