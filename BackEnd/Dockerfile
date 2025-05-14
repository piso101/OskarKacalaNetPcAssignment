# Dockerfile - ASP.NET 8.0 + Hot Reload + PostgreSQL
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Obraz SDK do budowania i Hot Reload
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BackEnd.csproj", "./"]
RUN dotnet restore "BackEnd.csproj"
COPY . . 
ENV DefaultConnection="Host=db;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword"
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile", "--urls=http://*:8080"]