type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered : bool
}

let calculateTotal (customer: Customer) (spend: decimal): decimal =
    let discount =
        if customer.IsEligible && spend >= 100.0M
        then (spend * 0.1M)
        else 0.0M
    let total = spend - discount
    total