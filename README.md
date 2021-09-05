# CSVEDITOR-API
Web API created for editing .csv files in browser
App based on ASP.NET Core 
Hosts on IIS 
In app we are used :
MediatR
CsvHelper
ClosedXMl
EntityFramework
SQLServer
Bootstrap
Swagger

There are two controllers 

AccountController :
Login takes LoginDTOs(email,password) returns status code
Register takse RegisterDTOs(email,password,userName)returns UserModel for debuggind
LogOut returns status code


CsvEditorController there are main functions
Get Transactions get transactions from context ,returns List<Transaction>
Import .csv file  add and replace data in context, returns List<Transaction>
Export takes export params ExportDTOs which columns we whanna to see in excel file , returns .xslx file
Create takes data from transaction model  ,returns List<Transaction>
Delete takes transaction id transaction ,returns redirect to main page
Edit takes transaction  returns List<transtaction>
Search takes Client name, returns List<Transaction>
Filter takes FilterDTOs two strings(Status,Type) , returns List<Transaction>



All is working with Mediator commands and handlers

In Models we have 
CsvEditorContext which inherits IdentityUser
and contain Transaction DbSet
Transactions model
DTOs:
LoginDTOs
RegisterDTOs
ExportDTOs contains information about which columns we will need 
FilterDTOs(string Status,string Type)
