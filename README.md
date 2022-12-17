# FilterTor
Complex filtering library to define, store, parse, and apply various types of filters on different entities. 
 
FilterTor library introduce different types of possible filters and store them using JSON semantic. Then it provide restore, validation and ways to apply defined filters on DB. 

### Main Features

- Using a document based model (JSON-based) it can be used to define various types of filters. Moreover, in order to store various filters, there is no need to create different tables in a relational DB.
- Using a document based loosely shema together with a strick validation mechanisem provde both flexibility and some level of type safety.
- It is independent of project entities (domain model). Core funtionalities are provided in FilterTor project and Adhering to the Open/Close principle, no method modification is needed to extend it in new projects.
- Using EF Core it is possible to parse JSON definition of  some filters and build Expression equivalent of it. So some filters can be run in DB level.


## Examples

Consider the following data model:

- Customer (Id, Name, RegisteredDate, Credit)
- CustomerGroup (Id, CreateTime, Type, CustomerId )
- Invoice (Id, CreateTime, InvoiceNumber, InvoiceDate, IsSettled, TotalAmount, CusotmerId, InvoiceType)
- InvoiceDetail (Id, InvoiceId, ProductId, Count, UnitPrice, Discount, IsPrize) 

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
};
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