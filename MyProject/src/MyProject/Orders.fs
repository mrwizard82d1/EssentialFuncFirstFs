namespace MyProject

type Item = {
    ProductId: int
    Quantity: int
}

type Order = {
    Id : int
    Items : Item list
}
    
module Domain =
    
    let addItem item order =
        // Append item to order
        let items = item::order.Items
        // Count how many of each product we have
        // New product = 1 entry; existing product = 2 entries
        // Sort items in productId order to simplify equality
        // Update order with new items
        { order with Items = items }
        
    // Let's create a couple of helpers bindings and some simple
    // assertions to help us test functionality in FSI.
    let order = { Id = 1; Items = [ {
        ProductId = 1
        Quantity = 1
        } ]
    }
    let newItemExistingProduct = { ProductId = 1; Quantity = 1 }
    let newItemNewProduct = { ProductId = 2; Quantity = 2 }
    
    let t1 = (addItem newItemNewProduct order = {
        Id = 1
        Items = [
            { ProductId = 2; Quantity = 2 }
            { ProductId = 1; Quantity = 1 }
        ]
    })
    printfn $"{t1}"
    
    let t2 = (addItem newItemExistingProduct order = {
       Id = 1
       Items= [
           { ProductId = 1; Quantity = 2 }
       ]
    })
    printfn $"{t2}"
