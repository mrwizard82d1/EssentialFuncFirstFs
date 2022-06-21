// Experimenting with `List`

let emptyItems = []

// Individual items
let items = [ 1; 2; 3; 4; 5 ]

// Using a range
let shortItems = [ 1..5  ]

// Or a list comprehension
let comprehensionItems = [ for x in 1..5 do x ]

// Use the cons operator, `::` to add an item to the head of the list
let extendedItems = 6 :: items

// The value bound to `items` is unaffected
items

// Define a function to "read" a list
let readList items =
    match items with
    | [] -> "Empty list"
    | [head] -> $"Head: {head}"
    | head::tail -> $"Head: %A{head} and Tail: %A{tail}"
    
let emptyList = readList emptyItems

let multipleList = readList items

let singleItemList = readList [1]

// We can remove the match for the single item and the code will still work
let shortReadList items =
    match items with
    | [] -> "Empty List"
    | head::tail -> $"Head: %A{head} and Tail: %A{tail}"
    
let shortEmptyList = shortReadList emptyItems

let shortMultipleList = shortReadList items

let shortSingleItemList = shortReadList [1]

// One can join (concatenate) two lists together

let list1 = [ 1..5 ]
let list2 = [ 3..7 ]

let joined = list1 @ list2
let joinedEmpty = list1 @ emptyItems
let emptyJoined = emptyItems @ list2

// The function `List.concat` does the same job as the `@` operator (but requires a `list` argument)
let functionJoined = List.concat([ list1; list2 ])

// One can filter a `list` using a predicate
let myList = [ 1..9 ]

let getEvens items =
    items
    |> List.filter (fun x -> x % 2 = 0)
    
let evens = getEvens myList

// One can add all the items in a list using `List.sum`
let sum items =
    items |> List.sum
    
let mySum = sum myList

// Use `List.map` to perform an operation on each `list` item returning a new `list`
let triple items =
    items
    |> List.map (fun x -> x * 3)
    
let myTriples = triple myList

// If we *do not* want to return a new `list`, use `List.iter`
let print items =
    items
    |> List.iter (fun x -> printfn $"My value is %i{x}")
    
print myList |> ignore

// The `List.map` function can change the structure of the input `list`
let lineItems = [ (1, 0.25M); (5, 0.25M); (1, 2.25M); (1, 125M); (7, 10.9M)  ]

let sumListItems items =
    items
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum
    
let myLineItemSum = sumListItems lineItems
// In this particular case, an easier way exists to perform the calculation in one step
let easierSumLineItems items =
    items
    |> List.sumBy (fun (q, p) -> decimal q * p)
    
let myEasierLineItemSum = easierSumLineItems lineItems

// Folding

// The following function uses a version of `List.fold` that has a type of
// `(decimal -> (int * decimal) -> decimal) -> decimal -> decimal`; that is, a function and a `decimal` value
// and returns a `decimal` value where the function accepts a `decimal` value and a tuple of type,
// `int` * `decimal`, and returns a `decimal` value.
let getTotal items =
    items
    |> List.fold (fun acc (q, p) -> acc + decimal q * p) 0M
    
let total = getTotal lineItems

// Although the book describes an alternative implementation of `List.fold` that accepts a "state" argument
// consisting of the accumulator and the list to fold; I do not believe this overload still exists. Further,
// I do not believe that the operator `||>` still exists.

// Grouping Data and Uniqueness

let myListWithDuplicates = [ 1; 2; 3; 4; 5; 7; 6; 5; 4; 3 ]

// Transforms initial list into a list of tuples (key * int list) where `key` is every unique value in the
// original list and `int list` is a list of all occurrences of key in the original list.
let groupByResult = myListWithDuplicates |> List.groupBy id

// Once grouped by each item in the list, we can use `List.map` to "extract" the keys
let unique items =
    items
    |> List.groupBy id
    |> List.map fst  // supplied function extracts the "key" (the first element of the tuple)
    
let uniqueResult = unique myListWithDuplicates

// Although using the `groupBy` and `map` combination works, F# also provides `List.distinct`  that  does
// all this work for us.
let distinct = myListWithDuplicates |> List.distinct

// Finally, the built-in type, `Set` provides similar functionality; however, `Set` sorts the keys.
let uniqueSet items =
    items
    |> Set.ofList
    
let setResult = uniqueSet myListWithDuplicates