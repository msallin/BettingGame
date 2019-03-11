Set-StrictMode -Version latest

function Get-Configuration {
    return [PSCustomObject]@{
        UrlBetting = "http://localhost:8081"
        UrlTournament = "http://localhost:8082"
        UrlUserManagement = "http://localhost:8080"
        UrlRanking = "http://localhost:8083"

        AdminEmail = "admin@betting.com"
        AdminPassword = "9N#7sY@IdbLT2b27CUv2"
    }
}

Export-ModuleMember -Function *-*