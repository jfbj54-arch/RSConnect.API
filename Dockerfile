# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# Publicar o projeto
RUN dotnet publish RSConnect.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Copiar wwwroot manualmente (ESSENCIAL)
COPY --from=build /src/wwwroot ./wwwroot

