# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia apenas o csproj primeiro
COPY RSConnect.API.csproj .
RUN dotnet restore RSConnect.API.csproj

# Copia o restante do código
COPY . .
RUN dotnet publish RSConnect.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copia os artefatos publicados da etapa de build
COPY --from=build /app/publish .

# Expõe a porta padrão
EXPOSE 8080

# Define o entrypoint
ENTRYPOINT ["dotnet", "RSConnect.API.dll"]
