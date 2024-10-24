using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Ultis
{
    public static class JwtDecode
    {
        public static string DecodeTokens(string jwtToken, string nameClaim)
        {
            var _tokenHandler = new JwtSecurityTokenHandler();
            Claim? claim = _tokenHandler.ReadJwtToken(jwtToken).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            return claim != null ? claim.Value : "Error!!!";
        }
    }
}
