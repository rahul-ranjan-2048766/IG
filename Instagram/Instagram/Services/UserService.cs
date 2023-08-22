using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class UserService : IUserService
    {
        private readonly Constants constants;
        public UserService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.USER_CONTROLLER}/");

                var response = await client.PostAsJsonAsync<User>(constants.ADD_USER, user);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                var result2 = await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string?> Delete(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.USER_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new 
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync($"{constants.DELETE_USER}/{id}");

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        public Task<string?> DeleteAll(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Get(int id, string token)
        {
            User data = new User();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.USER_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new 
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync($"{constants.GET_USER}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<User?>();
                    if (result != null) data = result;
                }
            }
            return data;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.USER_CONTROLLER}/");

                var response = await client.GetAsync(constants.GETALL_USER);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<User>>();
                    users.AddRange(result);
                }
            }
            return users;
        }

        public async Task<string?> Update(User user, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.USER_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new 
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PutAsJsonAsync(constants.UPDATE_USER, user);
                if (response != null)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return null;
        }
    }
}
