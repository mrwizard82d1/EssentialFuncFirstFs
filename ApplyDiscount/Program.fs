// Define a customer. A customer is a _AND_ type.
type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered: bool
}

// Calculates the net price. (That is, the price after applying
// the discount(s).)
let calculateTotal (customer: Customer) (spend: decimal) : decimal =
    let discount =
        if customer.IsEligible && spend >= 100.00M then
            spend * 0.10M
        else
            0.00M
    let total = spend - discount
    total
