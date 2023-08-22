using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class QueryService : IQueryService
    {
        private readonly Constants constants;
        public QueryService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Query query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.QUERY_CONTROLLER}/");

                var response = await client.PostAsJsonAsync<Query>(constants.ADD_QUERY, query);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return null;
        }

        public async Task<string?> Delete(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.QUERY_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync($"{constants.DELETE_QUERY}/{id}");

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> DeleteAll(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.QUERY_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.DeleteAsync(constants.DELETEALL_QUERY);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<List<Query>> GetAll(string token)
        {
            List<Query> Queries = new List<Query>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.QUERY_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_QUERY);
                Queries = await response.Content.ReadAsAsync<List<Query>>();
            }

            return Queries;
        }
    }
}


