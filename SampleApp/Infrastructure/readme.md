# Infrastructure
The Infrastructure project typically includes data access implementations. 
In a typical ASP.NET Core web application, these implementations include the 
Entity Framework (EF) DbContext, any EF Core Migration objects that have 
been defined, and data access implementation classes. The most common way to 
abstract data access implementation code is through the use of the Repository 
design pattern.

In addition to data access implementations, the Infrastructure project should 
contain implementations of services that must interact with infrastructure 
concerns. These services should implement interfaces defined in the Application Core, 
and so Infrastructure should have a reference to the Application Core project.

# Infrastructure types
	EF Core types (DbContext, Migration)
	Data access implementation types (Repositories)
	Infrastructure-specific services (for example, FileLogger or SmtpNotifier)

ref: 
https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
