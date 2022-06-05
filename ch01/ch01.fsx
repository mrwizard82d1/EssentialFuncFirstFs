type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered : bool
}

// Take advantage of type inference
let calculateTotal customer spend =
    let discount = 
        if customer.IsEligible && spend >= 100.0M
            then spend * 0.1M else spend
    spend - discount
