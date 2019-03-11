FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
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
