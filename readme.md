# Solution oluştur
dotnet new sln -n CRM

# Projeleri oluştur
dotnet new classlib -n CRM.Data
dotnet new classlib -n CRM.Business
dotnet new mvc -n CRM.Web

# Projeleri solution'a ekle
dotnet sln add CRM.Data/CRM.Data.csproj
dotnet sln add CRM.Business/CRM.Business.csproj
dotnet sln add CRM.Web/CRM.Web.csproj

# Web -> Business
cd CRM.Web
dotnet add reference ../CRM.Business/CRM.Business.csproj

# Business -> Data
cd ../CRM.Business
dotnet add reference ../CRM.Data/CRM.Data.csproj

# Migration oluştur
cd ../CRM.Data
dotnet ef migrations add InitialCreate
dotnet ef database update
