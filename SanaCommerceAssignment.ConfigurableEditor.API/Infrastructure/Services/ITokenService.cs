using Microsoft.IdentityModel.Tokens;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.API.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
public interface ITokenService
{
    Task<ServiceResult> GetTokenAsync(string userName, UserTypeEnum type);
}

public class TokenService : ITokenService
{

    public async Task<ServiceResult> GetTokenAsync(string userName, UserTypeEnum type)
    {
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.SecurityKey));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("Type", type.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, userName),
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(TokenConstants.ValidMinutes),
            Audience = TokenConstants.Audience,
            Issuer = TokenConstants.Issuer,
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = tokenHandler.WriteToken(token);
        TokenDto dto = new(jwtToken, type, tokenDescriptor.Expires.Value);
        return new(true, "", dto);
    }
}