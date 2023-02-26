 From Infastructure
dotnet ef migrations add -s ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj -o Data/Migrations AddUserTable --context IdentityContext
dotnet ef database update --startup-project ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj --context IdentityContext