# 项目介绍
用.net core实现一个可以控制其他PC机器执行本机命令的程序。包括一个管理端winform，一个消息处理端 webapi以及一个运行到宿主机器的服务  windows服务|linux服务
也许后期会朝着jenkins的方向走，做一个安全可靠的运维工具

[所有项目相关的知识点介绍都在这里，目前以一周一篇的频率，持续更新中](https://www.zhihu.com/column/c_1441347599417094144) 项目进度和文章编写速度保持同步。

管理端：SuperTerminal.Manager 注册需要本地安装证书
服务端如果是安装在linux下，那么需要通过服务端安装完毕后当前目录下的 SuperTerminal.pem 放到管理端的运行根目录下
服务端如果是安装在windows下,需要服务端生成的 SuperTerminal.pem 安装到本地证书目录 当前用户我的下边
生成证书的方法：
## Linux:
```
openssl genrsa -out SuperTerminal.key 2048
openssl rsa -in SuperTerminal.key -pubout -out SuperTerminal.pem
```
其中SuperTerminal.key需要放到服务端的根目录下 
## windows 以下内容放到 gen.ps1 文件中，以管理员身份打开powershell后到当前目录 执行 ./gen.ps1  windows10/11都可以执行。
如果安装服务的机器不能执行该脚本，可以将下边脚本中 可导出私钥注释放开，同时注释禁止导出私钥。
执行后win+R 输入mmc，文件->添加/删除管理单元  选择证书，选择我的用户账户，计算机账户。
通过脚本生成的证书就在证书（本地计算机）个人、证书下， 对于不能在服务端执行脚本的服务器，在这里将私钥导出，在目标服务器导入私钥到该目录下
公钥信息需要导入到管理端所在计算机的 证书/当前用户/个人/证书中
```
echo 生成证书
#禁止导出私钥
New-SelfSignedCertificate -Type Custom -Subject "CN=SuperTerminal" -FriendlyName "超级终端管理端证书" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\LocalMachine\My" -KeyExportPolicy NonExportable
#可导出私钥 --windows服务器不支持的情况下,通过本地生成后，手动导入 mmc->文件添加删除管理单元->证书->计算机账户->导入  个人节点 SuperTerminal
#New-SelfSignedCertificate -Type Custom -Subject "CN=SuperTerminal" -FriendlyName "超级终端管理端证书" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\LocalMachine\My"
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
```
## 客户端 SuperTerminal.Client 
在完整发布文件夹中已经包含了windows,linux的发布脚本
只需要在相应环境执行insall.sh/insall.bat输入服务端信息即可完成注册。

## 安装 
### Server端：
针对windows和linux的安装都是通过脚本绿色安装的
在完整release发布下  针对window和linux都是一键安装
window下以管理员身份执行start.bat.
安装后，会安装一个本地的mysql,端口是3388 ,证书需要自己去执行Win11.ps1生成 ，server端的默认端口是8086，如果有特殊端口要求，修改 web文件夹下的appsettings.json中的urls即可
linux下以root权限执行install.sh
如果首次将文件复制到服务器的情况也许需要执行 chmod +x install.sh 赋予执行权限
执行install.sh，会安装mysql，配置文件在当前目录mysql-8.0.28/my.cnf 端口依旧是3388 ，完全不影响系统中安装其他版本的mysql.
server端的端口依旧是8086，自定义方式和windows一样。
### Client端
执行install.bat/install.sh  按照要求填入服务端地址和别名即可
### 管理端
目前不需要安装，windows7 sp1以上的机器应该可以直接运行，不过在运行前需要根据不同的服务端安装证书。
## 卸载
windows下执行 uninstall.bat
linux下执行 uninstall.sh   
安装仅会在当前目录下写文件，windows会创建一个服务，linux也仅仅是创建一个服务，在卸载的时候，会将创建的这些都删除，mysql的数据也会在卸载的时候清理。
所以卸载前需要考虑情况要不要执行

## 说明
增加证书环节的目的还是为了安全，毕竟这个工具相对而言是比较危险的，管理人员必须是可信人员，在特定机器执行。
因为相应的服务都是以超级管理员身份注册的，所以理论上来说它是拥有目标机器所有操作权限的。而对于管理端而言，他不需要记住所有机器的账号。
只需要持有证书即可对所有可管理的机器做任何事，安装软件、重启等，重要的是，可以批量对多个终端执行相同命令
所有安装均不需要单独安装.net环境,所以相对来说可能体积会稍大一些。


## 完整release文件请通过网盘下载
### 百度网盘：
链接：https://pan.baidu.com/s/1essQAXMFe6C-SdTmFbIcTA
提取码：8086
### 迅雷网盘：
「链接：https://pan.xunlei.com/s/VN-qJNoSah7QXNyCepWrgvz-A1 提取码：xtcx」

