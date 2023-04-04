# FilterTor Project
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
Consider creating a discount module for the following data model. 

![image](https://user-images.githubusercontent.com/7770893/208354329-1f365f50-c394-4056-bd2f-01258b65c224.png)

Clients should be able to define and store the conditions under which discounts are granted for a period of time. 

1. All golden customers (customers having the "Golden" type in CustomerGroup table)
1. All customers except those who belong to "Limited" group
1. All golden and private customers 
1. All private customers with credit greater than 300,000
1. All customers except "Suspended" and "Limited" ones
1. All new customers (RegisteredDate within one month) 
1. All "FMCG" Invoices with TotalAmount greater than 40,000
1. All "Medical" Invoices for "Government" customers with TotalAmount greater than 30,000



## More pages

[FilterTor tutorial page](https://github.com/hosseinnarimanirad/FilterTor/tree/main/FilterTor)
