# FilterTor
Complex filtering library to define, store, parse, and apply various types of filters on different entities.  
 
FilterTor library introduces different possible filters and stores them using JSON semantics. Then it provides the restore, validation, and required mechanisms to apply defined filters on DB.  

### Main Features

- Using a document-based model (JSON-based), it can be used to define various types of filters. Moreover, to store various filters, there is no need to create different tables in a relational DB.
- Using a document-based schema with a strict validation mechanism provides flexibility and some level of type safety.
- It is independent of project entities (domain model). Core functionalities are provided in the FilterTor project and Adhere to the Open/Close principle; no method modification is needed to extend it in new projects.
- Using EF Core, it is possible to parse the JSON definition of some filters and build an Expression equivalent of it. So some filters can be run at the DB level.
- Easily can be integrated into Clean Architecture projects. (Find the sample project)

## Examples

### Sample data model
Consider the following data model:
 
![you_name_it (1)](https://user-images.githubusercontent.com/7770893/208346915-82946387-9082-4599-a122-4a98b10490fd.png)


### Invoices with (IsSettled = true)
```json
{
    "category": "property",
    "entity": "invoice",
    "property": "isSettled",
    "operation": "equalsTo",
    "target": {
        "targetType":"constant",
        "value" : "true" 
        }
}
```

### Invoices between two dates
```json
{
    "category": "property",
    "entity": "invoice",
    "property": "invoiceDate",
    "operation": "between",
    "target": {
        "targetType": "range",
        "minValue" : "1/1/2021",
        "maxValue" : "1/5/2022" 
        }
}
```
### Invoice with specific InvoiceNumbers

```json
{
    "category": "property",
    "entity": "invoice",
    "property": "invoiceNumber",
    "operation": "in",
    "target": {
        "targetType": "array",
        "values" : ["100100","100101", "100104"]
        }
}
```
