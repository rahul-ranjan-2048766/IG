using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class CommentService : ICommentService
    {
        private readonly Constants constants;
        public CommentService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Comment comment, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PostAsJsonAsync(constants.ADD_COMMENT, comment);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> Delete(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync($"{constants.DELETE_COMMENT}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> DeleteAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync(constants.DELETEALL_COMMENT);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<Comment?> Get(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync($"{constants.GET_COMMENT}/{id}");
                var result = await response.Content.ReadAsAsync<Comment?>();

                return result;
            }
        }

        public async Task<List<Comment>> GetAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_COMMENT);
                var result = await response.Content.ReadAsAsync<List<Comment>>();

                return result;
            }
        }

        public async Task<string?> Update(Comment comment, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.COMMENT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PutAsJsonAsync(constants.UPDATE_COMMENT, comment);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}