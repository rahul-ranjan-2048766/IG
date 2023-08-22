using Microservice.Models;

namespace Microservice.Services
{
    public interface IAuthenticateService
    {
        public Task<(Response?, bool)> AuthenticateUser(UserCred userCred);
        public Task<(AdminResponse?, bool)> AuthenticateAdmin(AdminCred adminCred);
    }
}


