using Instagram.Models;

namespace Instagram.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Constants constants;
        public AuthenticationService(IConfiguration config) => constants = new Constants(config);

        public async Task<(AdminResponse?, string?)> AuthenticateAdmin(AdminCred adminCred)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.AUTHENTICATE_CONTROLLER}/");

                var response = await client.PutAsJsonAsync<AdminCred>(
                    constants.AUTHENTICATE_ADMIN, adminCred);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AdminResponse?>();
                    return (result, null);
                } else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return (null, result);
                }
            }
        }

        public async Task<(Response?, string?)> AuthenticateUser(UserCred userCred)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.AUTHENTICATE_CONTROLLER}/");

                var response = await client.PostAsJsonAsync<UserCred>(
                    constants.AUTHENTICATE_USER, userCred);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Response?>();
                    return (result, null);
                } else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return (null, result);
                }
            }
        }
    }
}


