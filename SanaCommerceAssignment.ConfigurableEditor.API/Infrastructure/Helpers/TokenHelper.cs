using SanaCommerceAssignment.ConfigurableEditor.API.Models.DTOs;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using System.IdentityModel.Tokens.Jwt;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Helpers;
public class TokenHelper
{
    public static TokenDto GetClaims(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claims = new Dictionary<string, string>();
        foreach (var claim in jwtToken.Claims)
        {
            if (!claims.ContainsKey(claim.Type))
            {
                claims.Add(claim.Type, claim.Value);
            }
        }

        TokenDto dto = new(claims.ContainsKey("Name") ? claims["Name"] : "",
            claims.ContainsKey("Type") ? (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), claims["Type"]) : UserTypeEnum.Unknown);

        return dto;
    }
}
