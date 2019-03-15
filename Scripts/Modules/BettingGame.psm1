Set-StrictMode -Version latest

# -------------------------------------------------------------
# UserManagement
# -------------------------------------------------------------

function Get-SecurityToken {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Email,
        [Parameter(Mandatory=$true)]
        [string]$Password,
        [Parameter(Mandatory=$true)]
        [string]$Url
    )

    $response = Invoke-WebRequest -Uri "$Url/api/SecurityTokenService" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; "Referer" = "http://localhost:8080/"; } -Body ([System.Text.Encoding]::UTF8.GetBytes("{ `"password`": `"$Password`", `"email`": `"$Email`"}")) -UseBasicParsing
    EnsureSuccessfulResponse($response)
    return $response.Content.Trim('"')
}

function New-User {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [object]$User
    )

    $json = $User | ConvertTo-Json -Compress
    $response = Invoke-WebRequest -Uri "$Url/api/Registration" -Method "POST" -Headers @{"Content-Type" = "application/json-patch+json"; "accept" = "application/json"; } -Body ([System.Text.Encoding]::UTF8.GetBytes($json))
    EnsureSuccessfulResponse($response)
    return $response.Content | ConvertFrom-Json
}

function Get-Profile {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [string]$Jwt
    )

    $response = Invoke-WebRequest -Uri "$Url/api/Profile" -Headers @{ "Authorization" = "Bearer $Jwt"; "accept" = "text/plain"; }
    EnsureSuccessfulResponse($response)
    return $response.Content | ConvertFrom-Json
}

# -------------------------------------------------------------
# Tournament
# -------------------------------------------------------------
function New-Team {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [object]$Team,
        [Parameter(Mandatory=$true)]
        [array]$Url
    )

    $json = $Team | ConvertTo-Json -Compress
    $response = Invoke-WebRequest -Uri "$Url/api/Team" -Method "POST" -Headers @{"Authorization" = "Bearer $Jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes($json)) -UseBasicParsing
    EnsureSuccessfulResponse($response)
}

function Get-Teams {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [array]$Url
    )

    $response = Invoke-WebRequest -Uri "$Url/api/Team" -Method "GET" -Headers @{"Authorization" = "Bearer $jwt"; "Content-Type" = "application/json-patch+json"; } -UseBasicParsing
    EnsureSuccessfulResponse($response)
    return $response.Content | ConvertFrom-Json
}

function Get-Games {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [array]$Url
    )

    $response = Invoke-WebRequest -Uri "$Url/api/Game" -Headers @{"Authorization" = "Bearer $Jwt"; }
    EnsureSuccessfulResponse($response)
    return $response.Content | ConvertFrom-Json
}

function New-Game {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [object]$Game
    )

    $json = $Game | ConvertTo-Json -Compress
    $response = Invoke-WebRequest -Uri "$Url/api/Game" -Method "POST" -Headers @{"Authorization" = "Bearer $jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes($json)) -UseBasicParsing
    EnsureSuccessfulResponse($response)
}

# -------------------------------------------------------------
# Betting
# -------------------------------------------------------------
function New-Bet {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [object]$Bet
    )

    $json = $Bet | ConvertTo-Json -Compress
    $response = Invoke-WebRequest -Uri "$Url/api/Bet" -Method "POST" -Headers @{"Authorization" = "Bearer $Jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes($json))
    EnsureSuccessfulResponse($response)
}

function Get-Bet {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [string]$GameId
    )

    $response = Invoke-WebRequest -Uri "$Url/api/Bet?GameId=$GameId" -Method "GET" -Headers @{"Authorization" = "Bearer $Jwt"; }
    EnsureSuccessfulResponse($response)
    return $response.Content | ConvertFrom-Json
}

function New-Result {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [string]$Url,
        [Parameter(Mandatory=$true)]
        [object]$Result
    )

    $json = $Result | ConvertTo-Json -Compress
    $response = Invoke-WebRequest -Uri "$Url/api/Result" -Method "POST" -Headers @{"Authorization" = "Bearer $Jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" } -Body ([System.Text.Encoding]::UTF8.GetBytes($json))
    EnsureSuccessfulResponse($response)
}

function Invoke-RankingRefresh {
    param (
        [Parameter(Mandatory=$true)]
        [string]$Jwt,
        [Parameter(Mandatory=$true)]
        [string]$Url
    )
    $response = Invoke-WebRequest -Uri "$Url/api/TriggerRankingRefresh" -Method "POST" -Headers @{"Authorization" = "Bearer $Jwt"; "Content-Type" = "application/json-patch+json"; "accept" = "application/json" }
    EnsureSuccessfulResponse($response)
}

# -------------------------------------------------------------
# Utility
# -------------------------------------------------------------
function EnsureSuccessfulResponse ($response) {
    if ($response.StatusCode -gt 299) {
        throw "Request was not successful!"
    }
}

Export-ModuleMember -Function *-*