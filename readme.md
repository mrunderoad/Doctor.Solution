# Steps

- `dotnet ef migrations add <Name>`

  Add migration after changing structure of potential DB

- `dotnet ef database update`

  After verifying migration looks correct

- `dotnet ef migrations remove`

  Fix last migration before pushing to GH




git clone repo
cd into directory where csproj file is
dotnet ef database update -> Add database & tables to running MySQL database server on your computer

