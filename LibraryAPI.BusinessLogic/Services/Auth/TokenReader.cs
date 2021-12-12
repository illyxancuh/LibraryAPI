using LibraryAPI.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace LibraryAPI.BusinessLogic.Services.Auth
{
    public class TokenReader : ITokenReader
    {
        public int UserId { get; }
        public string Email { get; }
        public string FullName { get; }
        public string Role { get; }
        public bool IsLoggedIn { get; }

        public TokenReader(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null)
            {
                return;
            }

            HttpContext httpContext = httpContextAccessor.HttpContext;
            if (!httpContext.User.Claims.Any())
            {
                return;
            }

            Email = httpContext.User.FindFirst(ClaimTypes.Email).Value;
            Role = httpContext.User.FindFirst(ClaimTypes.Role).Value;
            UserId = int.Parse(httpContext.User.Claims.Single(claim => claim.Type == AuthClaimTypes.UserId).Value);
            FullName = httpContext.User.Claims.Single(claim => claim.Type == AuthClaimTypes.FullName).Value;

            IsLoggedIn = true;
        }
    }
}
