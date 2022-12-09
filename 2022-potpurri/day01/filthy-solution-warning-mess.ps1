$example = @"
1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
"@

$example.split("`n`n") | % { $_.split("`n") | measure-object -sum } | sort sum -desc | select -first 3 | select -ExpandProperty sum

$partA = [IO.File]::ReadAllText("$PSScriptRoot\input.txt")
$partA = $partA.Replace("`r", "") # strip Windows-only CR from CRLF

# note that we expect the text file is explicitly stored as LF 
$partA.split("`n`n") | % { $_.split("`n") | measure-object -sum } | sort sum -desc | select -first 3 | select count, sum

write-host "part b"
$partA.split("`n`n") | % { $_.split("`n") | measure-object -sum } | sort sum -desc | select -first 3 | select -ExpandProperty sum | measure-object -sum | select -ExpandProperty Sum