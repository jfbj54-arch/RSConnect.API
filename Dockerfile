# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY RSConnect.API.csproj .
RUN dotnet restore RSConnect.API.csproj
COPY . .
RUN dotnet publish RSConnect.API.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RSConnect.API.dll"]
