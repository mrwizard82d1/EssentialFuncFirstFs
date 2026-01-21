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
    
// Create a customer and calculate their discount
let john = {
    Id = "John"
    IsEligible = true
    IsRegistered = true
}

let assertJohn = (calculateTotal john 100.00M = 90.00M)
