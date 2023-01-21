# Constraint Rule Comparison
## Global
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| disabled               |✅|✅|✅|✅|✅|0|

## Numerics
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| const                  |✅|✅|✅|✅|✅|0|
| lt/lte/gt/gte          |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|

## Bools
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| const                  |✅|✅|✅|✅|✅|0|

## Strings
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| const                  |✅|✅|✅|✅|✅|0|
| len/min\_len/max_len   |✅|✅|✅|✅|✅|0|
| min\_bytes/max\_bytes  |✅|✅|✅|✅|✅|0|
| pattern                |✅|✅|✅|✅|✅|0|
| prefix/suffix/contains |✅|✅|✅|✅|✅|0|
| contains/not_contains  |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|
| email                  |✅|✅|❌|✅|✅|0|
| hostname               |✅|✅|✅|✅|✅|0|
| address                |✅|✅|✅|✅|✅|0|
| ip                     |✅|✅|✅|✅|✅|0|
| ipv4                   |✅|✅|✅|✅|✅|0|
| ipv6                   |✅|✅|✅|✅|✅|0|
| uri                    |✅|✅|❌|✅|✅|0|
| uri_ref                |✅|✅|❌|✅|✅|0|
| uuid                   |✅|✅|✅|✅|✅|0|
| well_known_regex       |✅|✅|✅|✅|✅|0|

## Bytes
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| const                  |✅|✅|✅|✅|✅|0|
| len/min\_len/max_len   |✅|✅|✅|✅|✅|0|
| pattern                |✅|✅|✅|✅|✅|0|
| prefix/suffix/contains |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|
| ip                     |✅|✅|❌|✅|✅|0|
| ipv4                   |✅|✅|❌|✅|✅|0|
| ipv6                   |✅|✅|❌|✅|✅|0|

## Enums
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| const                  |✅|✅|✅|✅|✅|0|
| defined_only           |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|

## Messages
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| skip                   |✅|✅|✅|✅|✅|0|
| required               |✅|✅|✅|✅|✅|0|

## Repeated
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| min\_items/max_items   |✅|✅|✅|✅|✅|0|
| unique                 |✅|✅|✅|✅|✅|0|
| items                  |✅|✅|❌|✅|✅|0|

## Maps
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| min\_pairs/max_pairs   |✅|✅|✅|✅|✅|0|
| no_sparse              |✅|✅|❌|❌|❌|0|
| keys                   |✅|✅|❌|✅|✅|0|
| values                 |✅|✅|❌|✅|✅|0|

## OneOf
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| required               |✅|✅|✅|✅|✅|0|

## WKT Scalar Value Wrappers
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| wrapper validation     |✅|✅|✅|✅|✅|0|

## WKT Any
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| required               |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|

## WKT Duration
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| required               |✅|✅|✅|✅|✅|0|
| const                  |✅|✅|✅|✅|✅|0|
| lt/lte/gt/gte          |✅|✅|✅|✅|✅|0|
| in/not_in              |✅|✅|✅|✅|✅|0|

## WKT Timestamp
| Constraint Rule | Go | GoGo | C++ | Java | Python | C# |
| ---| :---: | :---: | :---: | :---: | :---: | :---: |
| required               |✅|✅|❌|✅|✅|0|
| const                  |✅|✅|❌|✅|✅|0|
| lt/lte/gt/gte          |✅|✅|❌|✅|✅|0|
| lt_now/gt_now          |✅|✅|❌|✅|✅|0|
| within                 |✅|✅|❌|✅|✅|0|
