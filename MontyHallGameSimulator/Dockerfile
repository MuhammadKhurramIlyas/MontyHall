#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MontyHallGameSimulator/MontyHallGameSimulator.csproj", "MontyHallGameSimulator/"]
COPY ["MontyHallGameSimulator.Interfaces/MontyHallGameSimulator.Interfaces.csproj", "MontyHallGameSimulator.Interfaces/"]
COPY ["MontyHallSimulatorDataAccess/MontyHallSimulatorDataAccess.csproj", "MontyHallSimulatorDataAccess/"]
COPY ["MontyHallSimulatorDataAccess.Interfaces/MontyHallSimulatorDataAccess.Interfaces.csproj", "MontyHallSimulatorDataAccess.Interfaces/"]

RUN dotnet restore "MontyHallGameSimulator/MontyHallGameSimulator.csproj"
RUN dotnet restore "MontyHallGameSimulator.Interfaces/MontyHallGameSimulator.Interfaces.csproj"
RUN dotnet restore "MontyHallSimulatorDataAccess/MontyHallSimulatorDataAccess.csproj"
RUN dotnet restore "MontyHallSimulatorDataAccess.Interfaces/MontyHallSimulatorDataAccess.Interfaces.csproj"

COPY . .
WORKDIR "/src/MontyHallGameSimulator"
RUN dotnet build "MontyHallGameSimulator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MontyHallGameSimulator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MontyHallGameSimulator.dll"]