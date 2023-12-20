using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using backend.Exceptions;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace backend.Controllers;

public static class AuthEndPoint
{
    public static void MapSecurityEndpoints(this IEndpointRouteBuilder routes, WebApplicationBuilder builder)
    {
        routes.MapPost("/authenticate", [AllowAnonymous](User user) =>
        {
            Console.WriteLine("Try to authenticate: " + user.Username + " " + user.Password);

            var userFound = User.TestUsers.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (userFound == null)
            {
                throw new ErrorDeCliente("Usuario o contraseña incorrectos", HttpStatusCode.Unauthorized);
            }

            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userFound.Id!),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username!),
                    new Claim(JwtRegisteredClaimNames.Email, user.Username!)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            
            return Results.Ok(jwtToken);
        });

        routes.MapGet("/whoami", [Authorize](HttpContext context) =>
        {
            Console.WriteLine("whoami");

            var user = context.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                return Results.Unauthorized();
            }

            var userId = user.FindFirst("Id")?.Value;

            var userInfo = User
                .TestUsers
                .FirstOrDefault(u => u.Id == userId);

            return Results.Ok(userInfo);
        });
    }
}