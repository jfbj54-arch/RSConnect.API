# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura o projeto correto
RUN dotnet restore RSConnect.API/RSConnect.API.csproj

# Publica o projeto correto
RUN dotnet publish RSConnect.API/RSConnect.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Porta correta do Railway
ENV ASPNETCORE_URLS=http://0.0.0.0:8888
EXPOSE 8888

ENTRYPOINT ["dotnet", "RSConnect.API.dll"]

