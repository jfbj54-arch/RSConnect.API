# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY RSConnect.API.csproj .
RUN dotnet restore RSConnect.API.csproj

# Copiar todo o projeto
COPY . .

# Publicar o projeto
RUN dotnet publish RSConnect.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar arquivos publicados
COPY --from=build /app/publish .

# Copiar wwwroot manualmente (ESSENCIAL)
COPY --from=build /src/wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "RSConnect.API.dll"]
