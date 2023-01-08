using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Console.Jwt
{
    public class JwtTest
    {
        public static void TryValidateToken()
        {
            string nodeAccessToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyR3VpZCI6ImEwMDU2NmFiLTYwZDktNDllZi1hOWI3LWUyMGU1YzBmZjZkZSIsIlVzZXJOYW1lIjoiYW5keTAwMDUiLCJpYXQiOjE1NDk4NTM3MDUsImV4cCI6MTU1MjQ0NTcwNSwiYXVkIjoidHdlZWJhYSIsImlzcyI6Imh0dHBzOi8vdHdlZWJhYS5jb20ifQ.kwIu0T1m77U3Za1b53472C6S9NaokrWevIEidOQdHqo";
            string nodeAccessToken2 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyR3VpZCI6ImEwMDU2NmFiLTYwZDktNDllZi1hOWI3LWUyMGU1YzBmZjZkZSIsIlVzZXJOYW1lIjoiYW5keTAwMDUiLCJpYXQiOjE1NDk4NTk5NjUsImV4cCI6MTU1MjQ1MTk2NSwiYXVkIjoidHdlZWJhYSIsImlzcyI6Imh0dHBzOi8vdHdlZWJhYS5jb20ifQ.whsws3YBtdUhRHzmfkYGIxVshAipX-utjhYCm6J0LH0";
            string netAccessToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyR3VpZCI6ImFlZTlhODk3LTFlMDktNDc4ZC04YTE5LTk2M2JkMDk4ZDNlNSIsIlVzZXJOYW1lIjoiYW5keTAwMDMiLCJleHAiOjE1OTcxNTUwNzcsImlzcyI6Imh0dHBzOi8vdHdlZWJhYS5jb20iLCJhdWQiOiJ0d2VlYmFhIn0.t1yaeK1eF_OVzN7BF5uOFJzkdxz3DPAfiYyIQJYhh6k";
            string netAccessToken2 = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyR3VpZCI6ImFlZTlhODk3LTFlMDktNDc4ZC04YTE5LTk2M2JkMDk4ZDNlNSIsIlVzZXJOYW1lIjoiYW5keTAwMDMiLCJleHAiOjE1OTcxNjEyODQsImlzcyI6Imh0dHBzOi8vdHdlZWJhYS5jb20iLCJhdWQiOiJ0d2VlYmFhIn0.UtxdELeRh455j36xj1oE9c3mMdu3ckmntbBGFVKlFv0";

            var jwt = new JwtSecurityToken(nodeAccessToken);
            var sub = jwt.Claims.First(c => c.Type == "iss").Value;
            //sub.Dump();

            try
            {
                string issuer = "https://tweebaa.com";
                var audience = "tweebaa";
                var secret = "random_secret_key";
                var secretBytes = Encoding.UTF8.GetBytes(secret);
                var secretBase64 = "random_secret_key_base64";
                var secretBase64Encode = "random_secret_key_base64_url";
                var secretBase64Bytes = Encoding.UTF8.GetBytes(secretBase64);
                var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(secretBase64Bytes);
                var validationParams = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    //	ValidateLifetime = true,
                    //	ValidateIssuerSigningKey = true,
                    //	ValidateIssuer = false,
                    //	ValidateAudience = false,
                };
                var handler = new JwtSecurityTokenHandler();
                // return all claims like issuer, audience, expiration, subject
                Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
                var principal = handler.ValidateToken(netAccessToken2, validationParams, out validatedToken);
                //validatedToken.Dump();
                //principal.Dump();
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private readonly static string securityKeyBase64 = "YWJjZGVmZ2hpamts";

        public static string IssueToken()
        {
            var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(securityKeyBase64));

            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey,
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "myemail@myprovider.com"),
                new Claim(ClaimTypes.Role, "Administrator"),
            }, "Custom");

            var securityTokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Audience = "https://my.website.com",
                Issuer = "https://my.tokenissuer.com",

                Subject = claimsIdentity,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            // way1 to create token
            //var token = new JwtSecurityToken(issuer: securityTokenDescriptor.Issuer
            //    , audience: securityTokenDescriptor.Audience
            //    , claims: claimsIdentity.Claims
            //    , notBefore: null
            //    , expires: DateTime.Now.AddDays(180).Date.ToUniversalTime()
            //    , signingCredentials: signingCredentials);
            //var signedAndEncodedToken = tokenHandler.WriteToken(token);
            //System.Console.WriteLine(signedAndEncodedToken);

            // way2 to create token
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
            System.Console.WriteLine(plainToken);
            System.Console.WriteLine(signedAndEncodedToken);
            return signedAndEncodedToken;
        }

        public static void ValidateToken(string jwtToken)
        {
            var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(securityKeyBase64));
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                {
                    "https://my.website.com",
                    "https://my.otherwebsite.com"
                },
                ValidIssuers = new string[]
                {
                    "https://my.tokenissuer.com",
                    "https://my.othertokenissuer.com"
                },
                IssuerSigningKey = signingKey
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
            var validatedPrincipal = tokenHandler.ValidateToken(jwtToken,
                tokenValidationParameters, out validatedToken);

            System.Console.WriteLine(validatedPrincipal.Identity.Name);
            System.Console.WriteLine(validatedToken.ToString());

            System.Console.ReadLine();
        }
    }
}
