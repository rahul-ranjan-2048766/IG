using Instagram.Models;

namespace Instagram.Services
{
    public interface IAuthenticationService
    {
        public Task<(Response?, string?)> AuthenticateUser(UserCred userCred);
        public Task<(AdminResponse?, string?)> AuthenticateAdmin(AdminCred adminCred);
    }
}


