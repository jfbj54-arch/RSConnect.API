## Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para dentro do container
COPY . .

# Restaura o projeto (caminho correto)
RUN dotnet restore RSConnect.API.csproj

# Publica o projeto (caminho correto)
RUN dotnet publish RSConnect.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Porta correta do Railway (8888)
ENV ASPNETCORE_URLS=http://0.0.0.0:8888
EXPOSE 8888

ENTRYPOINT ["dotnet", "RSConnect.API.dll"]


