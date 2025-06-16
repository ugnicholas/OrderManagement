using Microsoft.AspNetCore.Http;
using OrderManagement.Application.Interfaces;
using System.Security.Claims;

namespace OrderManagement.Infrastructure.Shared
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(userIdString, out var id) ? id : Guid.Empty;
            }
        }
    }
}
