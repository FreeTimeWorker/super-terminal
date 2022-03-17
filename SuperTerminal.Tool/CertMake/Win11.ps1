echo 生成证书
New-SelfSignedCertificate -Type Custom -Subject "CN=SuperTerminal" -FriendlyName "超级终端管理端证书" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\LocalMachine\My" -KeyExportPolicy NonExportable
echo 证书生成成功
$certs= Get-ChildItem -Path cert:\localMachine\my
$targetCert
foreach($cert in $certs)
{
    $cert.Subject
    if($cert.Subject.Equals("CN=SuperTerminal"))
    {
        $targetCert=$cert;
    }
}
echo 导出证书
$current = Split-Path -Parent $MyInvocation.MyCommand.Definition
$outpath = $current+"\SuperTerminalClient.cer";
Export-Certificate -Type CERT -Cert $targetCert -FilePath $outpath
echo 导出证书完成,证书名称SuperTerminalClient.cer
pause

