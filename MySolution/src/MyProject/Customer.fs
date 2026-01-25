namespace MyApplication.Customer

// open System

// The `Customer` type is used in two different modules, so it is
// defined in the scope of the **namespace** and outside **both**
// modules. Because the modules are defined in the namespace, they
// have access to the `Customer` type without needing to import it.
type Customer = {
    Name : string
}

module Domain =
    
    type Name = string
    
    let create (name : Name) =
        { Name = name }
        
module Db =
    
//    open System.IO
    
    let save (_customer: Customer) =
        // Imagine that this function actually talks to a database.
        ()
