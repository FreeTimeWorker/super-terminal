# 生成私钥和公钥
openssl genrsa -out SuperTerminal.Key 1024 
openssl rsa -in SuperTerminal.Key -pubout -out SuperTerminal.pem
