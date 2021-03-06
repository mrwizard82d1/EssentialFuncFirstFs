Feature: applying a discount

Scenario Outline: Eligible registered customers get 10% discount when they spend £100 or more

Given the following registered customers
| Customer Id | Is Eligible |
| John        | true        |
| Mary        | true        |
| Richard     | false       |
When <Customer Id> spends <Spend>
Then their order total will be <Total>

Examples:
| Customer Id | Spend  | Total  |
| Mary        | 99.00  | 99.00  |
| John        | 100.00 | 90.00  |
| Richard     | 100.00 | 100.00 |
| Sarah       | 100.00 | 100.00 |
