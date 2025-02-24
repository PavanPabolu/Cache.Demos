
A Step by Step Guide to In-Memory Caching in ASP.NET Core
https://www.ezzylearning.net/tutorial/a-step-by-step-guide-to-in-memory-caching-in-asp-net-core

Distributed Caching in ASP.NET Core using Redis Cache
https://www.ezzylearning.net/tutorial/distributed-caching-in-asp-net-core-using-redis-cache

a.Cache Tag Helper:
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/cache-tag-helper?view=aspnetcore-5.0

b.Distributed Cache Tag Helper: 
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/distributed-cache-tag-helper?view=aspnetcore-5.0

c.Response Caching Middleware:
https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-5.0

d.ResponseCache:
https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-5.0#responsecache-attribute

Distributed caching in ASP.NET Core
https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-8.0&source=docs

**************************************************************************************************
PM>
Scaffold-DbContext -Connection "Server=localhost\\SQLEXPRESS; Database=AdventureWorksLT2022; Trusted_Connection=True; MultipleActiveResultSets=true;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -ContextDir "Data" -Context "AdventureWorksDbContext" -Tables "Product"

Error:"The term 'Scaffold-DbContext' is not recognized as the name of a cmdlet, function, script file, or operable program"
Sol: 
* Install-Package Microsoft.EntityFrameworkCore.Tools
* Install-Package Microsoft.EntityFrameworkCore.SqlServer
* Scaffold-DbContext ...
-Error: To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration
* Scaffold-DbContext "Name=DefaultConnection" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -ContextDir "Data" -Context "AdventureWorksDbContext" -Tables "Product"
**************************************************************************************************