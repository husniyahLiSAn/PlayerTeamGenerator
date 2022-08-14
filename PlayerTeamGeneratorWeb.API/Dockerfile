#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PlayerTeamGeneratorWeb.API/PlayerTeamGeneratorWeb.API.csproj", "PlayerTeamGeneratorWeb.API/"]
RUN dotnet restore "PlayerTeamGeneratorWeb.API/PlayerTeamGeneratorWeb.API.csproj"
COPY . .
WORKDIR "/src/PlayerTeamGeneratorWeb.API"
RUN dotnet build "PlayerTeamGeneratorWeb.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayerTeamGeneratorWeb.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayerTeamGeneratorWeb.API.dll"]