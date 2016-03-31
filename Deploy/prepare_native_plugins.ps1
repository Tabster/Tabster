Write-Host "Fetching featured plugin list..."

$plugins_dir = "$env:APPVEYOR_BUILD_FOLDER\Deploy\Plugins"

New-Item "$plugins_dir" -type directory

$wc = New-Object System.Net.WebClient

$j = $wc.DownloadString("http://tabster.org/featured_plugins.json") | ConvertFrom-Json | Select -ExpandProperty featured_plugins

Foreach ($plugin in $j)
{
    if ($plugin.native) {
        Write-Host "Downloading $($plugin.download)`n"
        Write-Host [System.IO.Path]::GetFileName($plugin.download)
        $zip_path = [System.IO.Path]::Combine($plugins_dir, [System.IO.Path]::GetFileName($plugin.download))
        Write-Host "Download Location: " + "$zip_path"
        $wc.DownloadFile($plugin.download, $zip_path)

        Write-Host "Unzipping $($plugin.download)`n"
        $output_dir = [System.IO.Path]::Combine($plugins_dir, $plugin.name)
        7z x "$zip_path" -o"$output_dir" -r -aoa

        Remove-Item "$zip_path"
    }
}