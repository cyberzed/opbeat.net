$accessToken = '0d85cde89fadf8d18c057dd9572b6a2579aaa85e'
$applicationid = '554e269e6f'
$organizationId = '42173c5126aa4fdf8acdf368c8555f7c'

$sha1 = 'D5E5FDD40F607787FE57486226647A7C770C4A1D'

#https://intake.opbeat.com/api/v1/organizations/<organization-id>/apps/<app-id>/releases/
$uri = "https://intake.opbeat.com/api/v1/organizations/$organizationId/apps/$applicationid/releases/"

$userAgent = 'opbeat.net/1.0'

$headers = @{
	'Authorization' = "Bearer $accessToken"
	'Content-Type' = 'application/json'
}

$body = ConvertTo-Json @{
	rev = "$sha1"
	status = 'completed'
}

$result = Invoke-RestMethod -Method Post -Headers $headers -Uri $uri -UserAgent $userAgent -Body $body

Write-Host $result
