#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MontyHallGame/MontyHallGame.csproj", "MontyHallGame/"]
COPY ["MontyHall.Interfaces/MontyHallGame.Interfaces.csproj", "MontyHallGame.Interfaces/"]
COPY ["MontyHallDataAccess/MontyHallDataAccess.csproj", "MontyHallDataAccess/"]
COPY ["MontyHallDataAccess.Interfaces/MontyHallDataAccess.Interfaces.csproj", "MontyHallDataAccess.Interfaces/"]

RUN dotnet restore "MontyHallGame/MontyHallGame.csproj"
RUN dotnet restore "MontyHallGame.Interfaces/MontyHallGame.Interfaces.csproj"
RUN dotnet restore "MontyHallDataAccess/MontyHallDataAccess.csproj"
RUN dotnet restore "MontyHallDataAccess.Interfaces/MontyHallDataAccess.Interfaces.csproj"

COPY . .
WORKDIR "/src/MontyHallGame"
RUN dotnet build "MontyHallGame.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MontyHallGame.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MontyHallGame.dll"]