type Customer = {
    Id: string
    IsEligible: bool
    IsRegistered : bool
}

let fred = {
    Id = "Fred"
    IsEligible = true
    IsRegistered = true
}
printfn $"{fred}"
