using Microsoft.AspNetCore.Http;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.FindFirst(CustomClaimTypes.UserId)?.Value;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("No valid user could be found in this HTTP Context");
            }

            return int.Parse(userId);
        }
    }
}
