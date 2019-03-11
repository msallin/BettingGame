# SmokeTest.ps1
# Remove or clear all MongoDb Collections before running this script.

Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

Import-Module ./Modules/Configuration.psm1 -Force
Import-Module ./Modules/DataSeed.psm1 -Force
Import-Module ./Modules/BettingGame.psm1 -Force

$config = $null

function SeedTournamentData() {
    $dataPath = "$PSScriptRoot\data.json"
    Start-TournamentSeed -Jwt $jwt -Path $dataPath -Config $config
}

function RegisterUsers() {
    Write-Host "Create users..."
    for ($i = 0; $i -lt 5; $i++) {
        Write-Host "user $i"
        $user = @{
            email = "t$i@betting.com";
            firstName = "Test$i";
            lastName = "t$i";
            password = "test";
            nickname = "tt$i"
        }
        New-User -Url $config.UrlUserManagement -User $user
    }
}

function Main () {
    $config = Get-Configuration

    RegisterUsers

    # Get the admin profile
    $jwt = Get-SecurityToken -Email $config.AdminEmail $config.AdminPassword $config.UrlUserManagement
    $profile = Get-Profile -Url $config.UrlUserManagement -Jwt $jwt
    Write-Host "Your profile:"
    Write-Host $profile
    if ($profile.Email -ne $config.AdminEmail) {
        throw "Wrong profile!"
    }

    SeedTournamentData

    Write-Host "Get all teams..."
    $allTeams = Get-Teams -Url $config.UrlTournament -Jwt $jwt
    $teamAId = $allTeams[0].Id
    $teamBId = $allTeams[1].Id
    Write-Host "TeamA: $teamAId"
    Write-Host "TeamB: $teamBId"

    Write-Host "Get all games..."
    $allGames = Get-Games -Url $config.UrlTournament -Jwt $jwt
    $gameId = $allGames[0].Id
    Write-Host "GameId: $gameId"

    Write-Host "Get an enduser jwt..."
    $endUserJwt = Get-SecurityToken -Email "t1@betting.com" -Password "test" -Url $config.UrlUserManagement

    Write-Host "Bet for a game..."
    $bet = @{
        gameId = $gameId
        scoreTeamA = 2
        scoreTeamB = 0
    }
    New-Bet -Url $config.UrlBetting -Jwt $endUserJwt -Bet $bet
    $bet = Get-Bet -Url $config.UrlBetting -Jwt $endUserJwt -GameId $bet.gameId
    if ($null -ne $bet.ActualResult) {
        throw "ActualResult must not be in the bet!"
    }
    if ($null -eq $bet.betResult) {
        throw "BetResult is null!"
    }
    if ($bet.Score -ne 0) {
        throw "Wrong score!"
    }

    Write-Host "Set result for a game..."
    $result = @{
        gameId = $gameId
        scoreTeamA = 1
        scoreTeamB = 0
    }
    New-Result -Url $config.UrlTournament -Jwt $jwt -Result $result

    Write-Host "Check the bet result..."
    $bet = Get-Bet -Url $config.UrlBetting -Jwt $endUserJwt -GameId $result.gameId
    if ($null -eq $bet.ActualResult) {
        throw "ActualResult not in the bet!"
    }
    if ($bet.Score -ne 7) {
        throw "Wrong score!"
    }

    Write-Host "Trigger ranking refresh..."
    Invoke-RankingRefresh -Url $config.UrlRanking -Jwt $jwt

    Write-Host "Smoke test done!" -ForegroundColor Green
}

Main