


Scaffold-DbContext -Connection "Server=DESKTOP-6SDKTRC; Database= AdventureWorks; Trusted_Connection=True; MultipleActiveResultSets=true;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -ContextDir "Data" -Context "AdventureWorksDbContext" -Tables "Product"

Scaffold-DbContext -Connection "Server=localhost\\SQLEXPRESS; Database=AdventureWorksLT2022; Trusted_Connection=True; MultipleActiveResultSets=true;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -ContextDir "Data" -Context "AdventureWorksDbContext" -Tables "Product"
