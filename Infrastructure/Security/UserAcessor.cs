using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAcessor : IUserAcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAcessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.NameIdentifier)?.Value;

            return username;
        }

        public string GetCurrentEmail()
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.Email)?.Value;

            return email;
        }

        public string GetCurrentDisplayName()
        {
            var displayName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.GivenName)?.Value;

            return displayName;
        }
    }
}