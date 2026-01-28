// # Script for learning about F# lists

// ## Core Functionality

// ### Constructing lists

// An empty list
let emptyListLiteral = []

// Create a list with five items
let fiveItemsListLiteral= [ 1; 2; 3; 4; 5 ]
let items = fiveItemsListLiteral

// Alternatively...
let fiveItemsRangeList = [1..5]

// One can also use a listComprehension
let fiveItemsListComprehensionList = [ for i in 1..5 do i ]

// ### Using the cons operator, `::`

let extendedItems = 6::fiveItemsListLiteral// but could be any

// Use pattern matching on `head::tail` constructs for lists
let readList items =
    match items with
    | [] -> "Empty list"
    | [head] -> $"Head: {head}. (Single item list.)"
    | head::tail -> $"Head: {head} and tail: {tail}."

let emptyList = readList []
let multipleList = readList [ 1; 2; 3; 4; 5 ]
let singleItemList = readList [1]

// Remove second case from previous definition. Still works!
let readListBriefer items =
    match items with
    | [] -> "Empty list"
    | head::tail -> $"Head: {head} and tail: {tail}."

let emptyListBriefer = readListBriefer []
let multipleListBriefer = readListBriefer [ 1; 2; 3; 4; 5 ]
let singleItemListBriefer = readListBriefer [1]

// ### List concatenation
let list1 = [1..5]
let list2 = [3..7]
let emptyListConcat = []

let joined = list1 @ list2
let joinedEmpty = list1 @ emptyListConcat
let emptyJoined = emptyListConcat @ list1

// The function, `List.concat`, produces the same result as `@`.
let joinedConcat = List.concat [ list1; list2 ]

// ### List.filter

let myList = [1..9]

let getEvens items =
    items
    |> List.filter (fun x -> x % 2 = 0)
    
let events = getEvens myList

// ## List.sum (to total up a list of numbers)

let sum (items : int list)=
    items |> List.sum
    
let mySum = sum myList

// Other aggregation functions are similar (but undemonstrated).

// ### List.map

let triple items =
    items
    |> List.map (fun x -> x * 3)
    
let myTriples = triple myList

// ### List.iter
//
// Performs an action on each item in a list - presumably for side effects.

let print items =
    items
    |> List.iter (fun x -> (printfn $"My value is {x}"))
    
// Rider reports that I need not pipe the result to `ignore`.
// print myList |> ignore
print myList

// ### Using `List.map` to change the structure of the resulting list

let originalItems = [
    (1, 0.25M); (5, 0.25M); (1, 2.25M); (1, 125M); (7, 10.9M)
]

let sumTupleItems items =
    items
    // Notice the conversion of an int, `q` to a decimal in the
    // next expression. Additionally, notice how one can use pattern
    // matching of the function argument to extract both elements
    // of the tuple.
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum

let sumOverTupleItems = sumTupleItems originalItems

// However, in this particular case, a simpler implementation uses the
// `List.sumBy` function.

let sumOverTupleItemsUsingSumBy =
    originalItems |> List.sumBy (fun (q, p) -> decimal q * p)
