FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY BettingGame.sln ./
COPY BettingGame.Tournament.Web/BettingGame.Tournament.Web.csproj BettingGame.Tournament.Web/
RUN dotnet restore BettingGame.Tournament.Web/BettingGame.Tournament.Web.csproj
COPY . .
WORKDIR /src/BettingGame.Tournament.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BettingGame.Tournament.Web.dll"]
