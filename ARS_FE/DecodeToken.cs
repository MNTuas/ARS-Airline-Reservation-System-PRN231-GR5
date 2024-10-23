using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ARS_FE
{
    public static class DecodeToken
    {
        public static string DecodeTokens(string jwtToken, string nameClaim)
        {
            var _tokenHandler = new JwtSecurityTokenHandler();
            Claim? claim = _tokenHandler.ReadJwtToken(jwtToken).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            return claim != null ? claim.Value : "Error!!!";
        }
    }
}
