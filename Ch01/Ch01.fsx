type RegisteredCustomer = {
    Id: string
}

type UnregisteredCustomer = {
    Id: string
}

type Customer = 
    | EligibleRegistered of RegisteredCustomer
    | Registered  of RegisteredCustomer
    | Guest of UnregisteredCustomer

let calculateTotal customer spend =
    let discount = 
        match customer with
        | EligibleRegistered _ when spend >= 100.0M 
            -> spend * 0.1M
        | _ -> 0.0M
    spend - discount

let john = EligibleRegistered { Id = "John" }
let mary = EligibleRegistered { Id = "Mary" }
let richard = Registered { Id = "Richard" }
let sarah = Guest { Id = "Sarah" }

let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.9M = 99.9M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
