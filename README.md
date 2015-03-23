# Very Simple Data

A lightweight data access component based on [Simple.Data](https://github.com/markrendle/Simple.Data) for executing 
SQL Server stored procedures.

This project was created to better understand the C# dynamic type and 
how Simple.Data utilizes it. A big shout out to Mark Rendle and the 
contributors to Simple.Data for all their hard work!

[Using dynamic and DynamicObject in C#](http://blog.theodybrothers.com/2014/10/using-dynamic-and-dynamicobject-in-c_24.html)

## Usage Examples

Executing a stored procedure ("GetProducts") and supplying two named 
parameters ("SuplierId" and "CategoryId"). The database's connection 
string is loaded from the appSettings:

	var database = Database.WithNamedConnectionString("connectionString");
	var results = database.GetProducts(SuplierId: "LOCC11", CatgegoryId: "Soccer");

Executing a storedProcedure ("PurgeLog") with no parameters:

	var database = Database.WithConnectionString("Server=./SqlServer;Initial Catalog=Main; Username=myUsername; Password=myPassword;");
	var results = database.PurgeLog();
