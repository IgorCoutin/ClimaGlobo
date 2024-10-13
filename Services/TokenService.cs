using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClimaGloboApi.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using minimalApi.Models;

namespace ClimaGloboApi.Services
{
    public class TokenService
    {

        private readonly IConfiguration _configuration;
        public enum ClaimType
        {
            id,
           nome
        }

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateCookie(HttpContext httpContext, string token)
        {
            httpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(60),
                Domain = _configuration["Jwt:Host"],
            });
        }

        public string GenerateToken(LoggedUserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
             new Claim("id", user.codigo.ToString()),
             new Claim("nome", user.nome.ToString()),

            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GetTokenFromCookie(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            return httpContext.Request.Cookies["JwtToken"];
        }

        public string GetTokenValue(string token, ClaimType key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken
                ?? throw new Exception("Invalid token");

            var claim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == key.ToString()) ?? throw new Exception("Claim not found");
            return claim.Value;
        }

    }

}