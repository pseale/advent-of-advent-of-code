$ErrorActionPreference = "Stop"

function Get-ChecksumFor($line, $groupsOfSize) {
    $chars = $line.toCharArray()

    $groups = @($chars `
        | Group-Object `
        | Where-Object { $_.count -eq $groupsOfSize })
    $hasGroups = $groups.Count -ne 0

    if ($hasGroups) { return 1 }
    else { return 0 }
}

function Build-AWastefulCharArray($line) {

    $chars = $line.toCharArray()

    $wastefulCharArray = @()
    for ($i=0; $i -lt $chars.length; $i++) {
        $wastefulCharArray += "char-$($chars[$i])-at-position-$i"
    }

    return $wastefulCharArray
}

function Build-LineComparisonResult($a, $b) {
    $c1 = Build-AWastefulCharArray -line $a
    $c2 = Build-AWastefulCharArray -line $b

    $comparison = Compare-Object $c1 $c2
    $indexesForCharsThatAreDifferent = $comparison `
        | ForEach-Object { [regex]::Match($_.InputObject, '\d+').Groups[0].Value } `
        | Sort-Object
    $sameChars = @()
    for ($i = 0; $i -lt $a.Length; $i++) {
        if ($indexesForCharsThatAreDifferent -notcontains $i) {
            $sameChars += $a[$i]
        }
    }

    return @{
        Line1 = $a;
        Line2 = $b;
        DifferentCharsCount = $comparison.Count / 2;
        SameChars = [string]::Join('', $sameChars)
    }
}

function Build-AllLineComparisonResults($lines) {
    $results = @()

    foreach ($line in $lines) {
        foreach ($candidate in $lines) {
            $results += Build-LineComparisonResult -a $line -b $candidate
        }
    }

    return $results
}

$lines = Get-Content $PSScriptRoot\input.txt

# part A
$groupsOfTwo = $lines | ForEach-Object { Get-ChecksumFor -line $_ -groupsOfSize 2 } `
    | Measure-Object -Sum `
    | Select-Object -ExpandProperty Sum
$groupsOfThree = $lines | ForEach-Object { Get-ChecksumFor -line $_ -groupsOfSize 3 } `
    | Measure-Object -Sum `
    | Select-Object -ExpandProperty Sum
Write-Host "Part A solution:          $($groupsOfTwo * $groupsOfThree)"

# part B
Write-Host "`nRunning... (this takes several minutes)"
$results = Build-AllLineComparisonResults -lines $lines

$partBSolution = $results `
    | Where-Object { $_.differentcharscount -gt 0 } `
    | Sort-Object DifferentCharsCount `
    | Select-Object -First 1 `
    | Select-Object -ExpandProperty SameChars

Write-Host "Part B solution:          $partBSolution"