### What is this service?
This is the service to authenticate and authorize for all services of the system. It uses Duende IdentityServer(formerly known as IdentityServer4) to implement OAuth2.0 flow

OAuth 2.0 is the industry-standard protocol for authorization.

https://oauth.net/2/

https://docs.duendesoftware.com/identityserver/v6

https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/identity-server

### Architecture
Its source code architecture follows the Onion Architecture which was introduced by Jeffrey Palermo.

https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/

![image](https://user-images.githubusercontent.com/30928752/226092727-de006edf-7d2c-4200-a3e8-a8ccf13b36f5.png)

Map to source code:

**User Interface:** It is APIs, Severless Functions,...

![image](https://user-images.githubusercontent.com/30928752/226093211-d9083c03-3ed4-4fe0-b01d-facde8352e52.png)

**Infrastructure:** Everything not related to business but configurations, integrations such as what and where database is, email sending, file system, cloud implementation,... should be put into here.

![image](https://user-images.githubusercontent.com/30928752/226093749-a4745f54-1db8-4d14-a79e-0f8d81c02e7a.png)

**Application Services:** The implementation for handling and processing business logic.

![image](https://user-images.githubusercontent.com/30928752/226094141-1ddf8907-9463-41be-ac81-8f858152705d.png)

**Domain Services:** This provide methods to interact with Domain layer. It's so commonly and can be used by other services. So it is seperated into a nuget package so that other services in the system can use

![image](https://user-images.githubusercontent.com/30928752/226094578-89c2b830-a443-4b39-b124-eabc6a8f3925.png)

![image](https://user-images.githubusercontent.com/30928752/226094877-b19ac696-81cf-4ee1-84f2-f5dae8b2fd65.png)


**Domain:**  Objects or entities of business will be reflected and put into here

![image](https://user-images.githubusercontent.com/30928752/226094754-e391775a-6223-48d4-a801-cec9d6f1248c.png)

### Golive
https://idp-sea-wa.azurewebsites.net/

![image](https://user-images.githubusercontent.com/30928752/226088363-97a22951-24f0-4e2c-a84c-10e1fcc01850.png)

--------
From Infastructure

dotnet ef migrations add -s ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj -o Data/Migrations AddUserTable --context IdentityContext
dotnet ef database update --startup-project ../GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj --context IdentityContext

Setup:


