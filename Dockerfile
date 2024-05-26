FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/GoGo.Idp.IdpServer/GoGo.Idp.IdpServer.csproj", "src/GoGo.Idp.IdpServer/"]
RUN dotnet restore "src\GoGo.Idp.IdpServer\GoGo.Idp.IdpServer.csproj"
COPY . .
WORKDIR "/src/src/GoGo.Idp.IdpServer"
RUN dotnet build "GoGo.Idp.IdpServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoGo.Idp.IdpServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoGo.Idp.IdpServer.dll"]
