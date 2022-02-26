using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SuperTerminal.JWT
{
    public class Jwt : IJwt
    {
        private readonly JwtConfig _jwtConfig = new();
        public Jwt(IConfiguration configration)
        {
            configration.GetSection("Jwt").Bind(_jwtConfig);
        }
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="Claims"></param>
        /// <returns></returns>
        public string GetToken(IDictionary<string, object> Claims)
        {
            RSA rsaSigning = RSA.Create();
            rsaSigning.ImportRSAPrivateKey(Convert.FromBase64String(_jwtConfig.SigningPriKey), out _);//私钥签名
            RSA rsaEncrypt = RSA.Create();
            rsaEncrypt.ImportRSAPublicKey(Convert.FromBase64String(_jwtConfig.EncryptPubKey), out _);//公钥加密
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_jwtConfig.Lifetime),
                Claims = Claims,
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsaSigning.ExportParameters(true)), SecurityAlgorithms.RsaSsaPssSha512),//非对称私钥签名,公钥验签
                EncryptingCredentials = new EncryptingCredentials(new RsaSecurityKey(rsaEncrypt.ExportParameters(false)), SecurityAlgorithms.RsaOAEP, SecurityAlgorithms.Aes128CbcHmacSha256)//非对称加密

            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        public bool ValidateToken(string Token, out Dictionary<string, object> Clims)
        {
            RSA rsaSigning = RSA.Create();
            rsaSigning.ImportRSAPublicKey(Convert.FromBase64String(_jwtConfig.SigningPubKey), out _);//公钥验签
            RSA rsaEncrypt = RSA.Create();
            rsaEncrypt.ImportRSAPrivateKey(Convert.FromBase64String(_jwtConfig.EncryptPriKey), out _);//私钥解密
            Clims = new Dictionary<string, object>();
            if (string.IsNullOrWhiteSpace(Token))
            {
                return false;
            }
            JwtSecurityTokenHandler handler = new();
            try
            {
                JwtSecurityToken jwt = handler.ReadJwtToken(Token);
                if (jwt == null)
                {
                    return false;
                }
                TokenValidationParameters validationParameters = new()
                {
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new RsaSecurityKey(rsaSigning.ExportParameters(false)),//公钥解密 --签名算法
                    TokenDecryptionKey = new RsaSecurityKey(rsaEncrypt.ExportParameters(true)),//私钥解密--解密算法
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = _jwtConfig.ValidateLifetime,//是否验证失效时间
                    ValidAudience = _jwtConfig.Audience,
                    ValidIssuer = _jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,//是否验证签名
                };
                ClaimsPrincipal principal = handler.ValidateToken(Token, validationParameters, out SecurityToken securityToken);
                foreach (Claim item in principal.Claims)
                {
                    Clims.Add(item.Type, item.Value);
                }
                return true;
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
