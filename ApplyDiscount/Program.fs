// Define a customer. A customer is a _AND_ type.
type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered: bool
}

// Construct an instance of a customer (for example).
let fred = {
    Id = "Fred"
    IsEligible = true
    IsRegistered = true
}
fred
