FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BackEnd.csproj ./
RUN dotnet restore "BackEnd.csproj"
COPY . .
COPY ./API/appsettings.json ./appsettings.json
WORKDIR /src
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile", "--urls=http://*:8080"]
