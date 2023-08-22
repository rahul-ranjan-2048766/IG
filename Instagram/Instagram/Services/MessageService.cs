using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class MessageService : IMessageService
    {
        private readonly Constants constants;
        public MessageService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Community data, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.MESSAGE_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PostAsJsonAsync(constants.ADD_MESSAGE, data);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<List<Community>> GetAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.MESSAGE_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_MESSAGE);
                var result = await response.Content.ReadAsAsync<List<Community>>();

                return result;

            }
        }
    }
}
