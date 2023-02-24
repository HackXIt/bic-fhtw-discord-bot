﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["bic-fhtw-discord-bot.csproj", "./"]
RUN dotnet restore "bic-fhtw-discord-bot.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "bic-fhtw-discord-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bic-fhtw-discord-bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bic-fhtw-discord-bot.dll"]
