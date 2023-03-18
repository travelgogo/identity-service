![image](https://user-images.githubusercontent.com/30928752/226088363-97a22951-24f0-4e2c-a84c-10e1fcc01850.png)
 
From Infastructure
dotnet ef migrations add -s ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj -o Data/Migrations AddUserTable --context IdentityContext
dotnet ef database update --startup-project ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj --context IdentityContext
