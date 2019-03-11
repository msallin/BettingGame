
workflow main {
    $urlBetting = "http://localhost:8081"
    $urlTournament = "http://localhost:8082"
    $urlUserManagement = "http://localhost:8080"
    $urlRanking = "http://localhost:8083"

    function RegisterUsers($urlUserManagement) {
        for ($i = 0; $i -lt 150; $i++) {
            Write-Host "user $i"
            $response = Invoke-WebRequest -Uri "$urlUserManagement/api/Registration" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"email`": `"t$i@outlook.com`", `"firstName`": `"Test$i`", `"lastName`": `"t$i`", `"password`": `"test1234`", `"nickname`": `"tt$i`"}"))
        }
    }

    function GetAllGames ($urlUserManagement, $urlTournament) {
        Write-Host "Get all games"
        $emailAddress = "admin@betting.com"
        $password = "9N#7sY@IdbLT2b27CUv2"
        Write-Host "Getting JWT for $emailAddress"
    
        $response = Invoke-WebRequest -Uri "$urlUserManagement/api/SecurityTokenService" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"password`": `"$password`", `"email`": `"$emailAddress`" }")) -UseBasicParsing
        $jwt = $response.Content.Trim('"')
    
        $allGamesResponse = Invoke-WebRequest -Uri "$urlTournament/api/Game" -Headers @{"Authorization" = "Bearer $jwt"; }
        $allGames = $allGamesResponse.Content | ConvertFrom-Json
        $gameId = $allGames[0].Id
        Write-Host "GameId: $gameId"
        return $allGames
    }

    function PlaceBetsForAllGames($i, $allGames, $urlUserManagement, $urlBetting) {
        Write-Host "Place bets for user $i"

        $emailAddress = "t$i@outlook.com"
        $password = "test1234"

        $response = Invoke-WebRequest -Uri "$urlUserManagement/api/SecurityTokenService" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"password`": `"$password`", `"email`": `"$emailAddress`" }")) -UseBasicParsing
        $jwt = $response.Content.Trim('"')

        ForEach ($game in $allGames) {
            $scoreTeamA = Get-Random -Maximum 10
            $scoreTeamB = Get-Random -Maximum 10
            # Bet for a game
            $gameId = $game.Id
            $response = Invoke-WebRequest -Uri "$urlBetting/api/Bet" -Method "POST" -Headers @{"Authorization" = "Bearer $jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes("{  `"gameId`": `"$gameId`",  `"scoreTeamA`": `"$scoreTeamA`",  `"scoreTeamB`": `"$scoreTeamB`"}"))
            $betResponse = Invoke-WebRequest -Uri "$urlBetting/api/Bet" -Method "GET" -Headers @{"Authorization" = "Bearer $jwt"; }
            $bet = $betResponse.Content | ConvertFrom-Json
            if ($bet.ActualResult -ne $null) {
                throw "ActualResult must not be in the bet!"
            }
            if ($bet.betResult -eq $null) {
                throw "BetResult is null!"
            }
            if ($bet.Score -ne 0) {
                throw "Wrong score!"
            }
        }
    }

    function  SetGameResults ($games, $urlUserManagement, $urlTournament) {
        $emailAddress = "admin@betting.com"
        $password = "9N#7sY@IdbLT2b27CUv2"
        $response = Invoke-WebRequest -Uri "$urlUserManagement/api/SecurityTokenService" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"password`": `"$password`", `"email`": `"$emailAddress`" }")) -UseBasicParsing
        $jwt = $response.Content.Trim('"')

        foreach ($game in $games) {
            $scoreTeamA = Get-Random -Maximum 10
            $scoreTeamB = Get-Random -Maximum 10
            $gameId = $game.Id
            $response = Invoke-WebRequest -Uri "$urlTournament/api/Result" -Method "POST" -Headers @{"Authorization" = "Bearer $jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes("{  `"gameId`": `"$gameId`",  `"scoreTeamA`": `"$scoreTeamA`",  `"scoreTeamB`": `"$scoreTeamB`"}"))
        }
    }

    function TriggerRankingRefresh ($urlUserManagement, $urlRanking) {
        $emailAddress = "admin@betting.com"
        $password = "9N#7sY@IdbLT2b27CUv2"
        $response = Invoke-WebRequest -Uri "$urlUserManagement/api/SecurityTokenService" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"password`": `"$password`", `"email`": `"$emailAddress`" }")) -UseBasicParsing
        $jwt = $response.Content.Trim('"')
        Invoke-WebRequest -Uri "$urlRanking/api/TriggerRankingRefresh" -Method "POST" -Headers @{"Authorization" = "Bearer $jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" }
    }

    RegisterUsers $urlUserManagement 
    $allGames = GetAllGames $urlUserManagement $urlTournament
    $startTime = get-date
    foreach -parallel ($i in (0..149)) {PlaceBetsForAllGames $i $allGames $urlUserManagement $urlBetting }
    $parallelElapsedTime = "elapsed time (parallel foreach loop): " + ((get-date) - $startTime).TotalSeconds
    $serialElapsedTime
    $parallelElapsedTime
    SetGameResults $allGames $urlUserManagement $urlTournament
    TriggerRankingRefresh $urlUserManagement $urlRanking
}

main
