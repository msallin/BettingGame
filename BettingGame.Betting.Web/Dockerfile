FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY BettingGame.sln ./
COPY BettingGame.Betting.Web/BettingGame.Betting.Web.csproj BettingGame.Betting.Web/
RUN dotnet restore BettingGame.Betting.Web/BettingGame.Betting.Web.csproj 
COPY . .
WORKDIR /src/BettingGame.Betting.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "BettingGame.Betting.Web.dll"]
