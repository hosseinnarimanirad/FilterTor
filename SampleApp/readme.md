Sample Application

In this section, a sample application is provided based on the Clean architecture, which uses the FilterTor library to store and retrieve filters and use them to filter a sample list API. This sample application consists of the following projects.

- **Api project**: an ASP.NET Core 7 api project
- **Presentation**: consists of the controller classes
- **Application**: consists of the handlers and dto classes
- **Infrastructure/Ef**: the persistence layer, which consists of the ef configuration files and dbContext class
- **Core**: consists of the domains and their business logic
- **Core/FilterTor**: consists of essential classes to implement FilterTor library based on the domains

