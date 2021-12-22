param([switch]$skipNpmInstall)

$ErrorActionPreference = "Stop"


function Generate-SingleMermaidDiagram($tree) {
    $name = $tree.Replace("[", "").Replace("]", "").Replace(",", "") + "-mermaid-diagram"

    & dotnet run mermaid $tree > "$name.txt"
    if ($lastexitcode -ne 0) { throw "error with dotnet run (see above)" }

    & .\node_modules\.bin\mmdc.cmd -i "$name.txt" -o "$name.png" -s 4
    if ($lastexitcode -ne 0) { throw "error with mermaid-js-cli (see above)" }

    return @"
    <div class="diagram">
        <h3>$tree</h3>
        <img src="./$name.png" />
    </div>
"@

}


pushd

try {
    cd $PSScriptRoot

    if (!$skipNpmInstall) {
        & npm install '@mermaid-js/mermaid-cli'
        if ($lastexitcode -ne 0) { throw "error with npm (see above)" }
    }

    $html = "<html><head><title>tree diagrams</title>
    <style>
        body { font-family: sans-serif; font-size: 14px; }
        .diagram { margin: 10px; }
    </style>
    </head><body>"

    $html += Generate-SingleMermaidDiagram -tree '[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]'

    $html += "</body></html>"
    $html | out-file -encoding UTF8 mermaid-diagrams.html
    ii mermaid-diagrams.html
} finally { popd }