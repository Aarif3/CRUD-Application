using InventryShop.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace InventryShop.ValidationFile
{
    [Authorize]
    public class TokenCreate
    {
            public static string  signInKey = "z%C*F-J@NcRfUjXn";
        
        public static string JwtCreateToken(SignUp Login)
        {
            var signInKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey));
            var creadential = new SigningCredentials(signInKeys, SecurityAlgorithms.HmacSha256);

            var issuer = "https://localhost:44307/";
            var Audience = "https://localhost:44307/";
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Login.UserName),
                new Claim(ClaimTypes.Role,Login.Role)

            };

            var token = new JwtSecurityToken(issuer,Audience,
                claims,expires:DateTime.Now.AddDays(1),
                signingCredentials:creadential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);



                
        }


        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            
            var handle = new JwtSecurityTokenHandler();
            handle.ValidateToken(token,new TokenValidationParameters()
            {
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            }, out SecurityToken ValidatedToken);
            JwtSecurityToken jwttoken = (JwtSecurityToken)ValidatedToken;
            var identity = new ClaimsIdentity(jwttoken.Claims, "Bearer");

            return new ClaimsPrincipal(identity);

        }

       

    }
}