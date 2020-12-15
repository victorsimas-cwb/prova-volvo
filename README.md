# Volvo Assessment - Dotnet Core

### Running application
``` dotnet run --project VolvoTrucks.WebApp ``` inside Solution root folder.

### Running tests
Command ``` dotnet test ``` inside Solution root folder. Run tests after running the application at least one time to ensure database will be created.

### Possible database or CLI troubles
I have no another machine to test if could be any problem with MSSQL version or dotnet CLI tools. By the way, there are a few things you could do to try to solve if it happens (I'm confident it won't happen!).

1. ```dotnet tool install --global dotnet-ef```
2. ```dotnet tool update --global dotnet-ef```
3. ```dotnet ef database update```
