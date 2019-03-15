Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

Import-Module ./Modules/Configuration.psm1 -Force
Import-Module ./Modules/BettingGame.psm1 -Force

$config  = Get-Configuration
Add-Member -InputObject $config @{ UserCount = 50 }

function RegisterUsers($config) {
    Write-Host "Register users..."
    for ($i = 0; $i -lt $config.UserCount; $i++) {
        $user = @{
            email = "tr$i@betting.com"
            firstName = "Test$i"
            lastName = "t$i"
            password = "test"
            nickname = "tt$i"
        }
        Write-Host "$i"
        New-User -Url $config.UrlUserManagement -User $user
    }
}

function PlaceBetsForAllGames($i, $allGames, $config) {
    Write-Host "Place bets for user $i..."
    $userJwt = Get-SecurityToken -Url $config.UrlUserManagement -Email "tr$i@betting.com" -Password "test"
    Write-Host $userJwt

    ForEach ($game in $allGames) {
        $bet = @{
            gameId = $game.Id
            scoreTeamA = Get-Random -Maximum 10
            scoreTeamB = Get-Random -Maximum 10
        }
        Write-Host $bet.gameId
        New-Bet -Url $config.UrlBetting -Jwt $userJwt -Bet $bet
        $betFromRemote = Get-Bet -Url $config.UrlBetting -Jwt $userJwt -GameId $bet.gameId
        if ($null -ne $betFromRemote.actualResult) {
            throw "ActualResult must not be in the bet!"
        }
        if ($null -eq $betFromRemote.betResult) {
            throw "BetResult is null!"
        }
        if ($betFromRemote.Score -ne 0) {
            throw "Wrong score!"
        }
    }
}

function  SetGameResults ($games, $config, $jwt) {
    Write-Host "Set game results..."
    foreach ($game in $games) {
        $result = @{
            gameId = $game.Id
            scoreTeamA = Get-Random -Maximum 10
            scoreTeamB = Get-Random -Maximum 10
        }
        Write-Host $game.Id
        New-Result -Url $config.UrlTournament -Jwt $jwt -Result $result
    }
}

function TriggerRankingRefresh ($urlUserManagement, $config, $jwt) {
    Write-Host "Trigger ranking refresh..."
    Invoke-RankingRefresh -Url $config.UrlRanking -Jwt $jwt
}

function Main {
    $jwt = Get-SecurityToken -Url $config.UrlUserManagement -Email $config.AdminEmail -Password $config.AdminPassword
    RegisterUsers $config
    $allGames = Get-Games -Url $config.UrlTournament -Jwt $jwt
    $startTime = get-date
    foreach ($i in (0..($config.UserCount - 1))) {
        PlaceBetsForAllGames $i $allGames $config
    }
    $parallelElapsedTime = "elapsed time (parallel foreach loop): " + ((get-date) - $startTime).TotalSeconds
    $parallelElapsedTime
    SetGameResults $allGames $config $jwt
    Invoke-RankingRefresh -Url $config.UrlRanking -Jwt $jwt
}

Main