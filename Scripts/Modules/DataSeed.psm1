Set-StrictMode -Version latest

function Start-TournamentSeed {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [object]$Config,
        [Parameter(Mandatory=$true)]
        [string]$Path
    )

    $data = GetTournamentDataFromDisk -Path $Path
    SeedTeams $jwt $data.teams $Config
    SeedGames $jwt $data.games $Config
}

function GetTournamentDataFromDisk($path) {
    $data = (Get-Content $path | Out-String | ConvertFrom-Json)
    return $data
}

function SeedTeams ($jwt, $teams, $config) {
    foreach ($team in $teams) {
        Write-Host "Create: " $team
        New-Team -Jwt $jwt -Team $team -Url $config.UrlTournament
    }
}

function SeedGames ($jwt, $games, $config) {
    $allTeams = Get-Teams -Jwt $jwt -Url $config.UrlTournament

    foreach ($game in $games) {
        Write-Host "Create: " $game
        $newGame = @{ start = $Game.Start; type = $Game.Type; }
        if($game.Type -eq "group") {
            $newGame.group = $game.Group
            
            # The ids need to be set, not the names.
            $newGame.TeamA = @($allTeams | Where-Object { $_.iso2 -eq $game.TeamA })[0].id
            $newGame.teamB = @($allTeams | Where-Object { $_.iso2 -eq $game.TeamB })[0].id
        }

        New-Game -Jwt $jwt -Url $config.UrlTournament -Game $newGame
    }
}

Export-ModuleMember *-*