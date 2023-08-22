using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class ChatService : IChatService
    {
        private readonly Constants constants;
        public ChatService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Chat chat, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PostAsJsonAsync(constants.ADD_CHAT, chat);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> Delete(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync($"{constants.DELETE_CHAT}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> DeleteAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync(constants.DELETEALL_CHAT);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<Chat?> Get(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync($"{constants.GET_CHAT}/{id}");
                var result = await response.Content.ReadAsAsync<Chat?>();

                return result;
            }
        }

        public async Task<List<Chat>> GetAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_CHAT);
                var result = await response.Content.ReadAsAsync<List<Chat>>();

                return result;
            }
        }

        public async Task<string?> Update(Chat chat, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.CHAT_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PutAsJsonAsync(constants.UPDATE_CHAT, chat);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}