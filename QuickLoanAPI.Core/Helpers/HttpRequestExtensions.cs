using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Helpers
{
  public static class HttpRequestExtensions
  {
    private static string GetJwtToken(HttpRequest request)
    {
      string authorizationHeader = request.Headers["Authorization"];

      if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
      {
        return authorizationHeader.Substring("Bearer ".Length).Trim();
      }

      return null;
    }
    public static int GetUserIdbyJWT(this HttpRequest request)
    {
      var jwtTokenString = GetJwtToken(request);
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(jwtTokenString);

      var claims = token.Claims;
      var id = token.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
      return int.Parse(id);
    }
  }
}
