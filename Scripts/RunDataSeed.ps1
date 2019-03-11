Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

Import-Module ./Modules/Configuration.psm1 -Force
Import-Module ./Modules/BettingGame.psm1 -Force
Import-Module ./Modules/DataSeed.psm1 -Force

$config = Get-Configuration
$jwt = Get-SecurityToken -Email $config.AdminEmail -Password $config.AdminPassword -Url $config.UrlUserManagement
$dataPath = "$PSScriptRoot\data.json"

Start-TournamentSeed -Jwt $jwt -Path $dataPath -Config $config
