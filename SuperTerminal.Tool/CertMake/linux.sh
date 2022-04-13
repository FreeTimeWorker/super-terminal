# 生成私钥和公钥
openssl genrsa -out SuperTerminal.key 2048 
openssl rsa -in SuperTerminal.key -pubout -out SuperTerminal.pem
