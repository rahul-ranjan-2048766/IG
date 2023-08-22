using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class PostService : IPostService
    {
        private readonly Constants constants;
        public PostService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Post data, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PostAsJsonAsync(constants.ADD_POST, data);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> Delete(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync($"{constants.DELETE_POST}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> DeleteAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync(constants.DELETEALL_POST);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<Post?> Get(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync($"{constants.GET_POST}/{id}");
                var result = await response.Content.ReadAsAsync<Post?>();

                return result;
            }
        }

        public async Task<List<Post>> GetAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_POST);
                var result = await response.Content.ReadAsAsync<List<Post>>();

                return result;
            }
        }

        public async Task<string?> Update(Post data, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.POST_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PutAsJsonAsync(constants.UPDATE_POST, data);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}