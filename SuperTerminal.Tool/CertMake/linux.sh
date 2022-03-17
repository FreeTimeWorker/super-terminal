# 生成私钥和公钥
openssl genrsa -out SuperTerminal.key 1024 
openssl rsa -in SuperTerminal.key -pubout -out SuperTerminal.pem
