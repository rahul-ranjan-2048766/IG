using Instagram.Models;

namespace Instagram.Services
{
    public class AdminService : IAdminService
    {
        private readonly Constants constants;
        public AdminService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(Admin data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.PostAsJsonAsync(constants.ADD_ADMIN, data);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.DeleteAsync($"{constants.DELETE_ADMIN}/{id}");
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<string?> DeleteAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.DeleteAsync(constants.DELETEALL_ADMIN);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<Admin?> Get(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.GetAsync($"{constants.GET_ADMIN}/{id}");
                var result = await response.Content.ReadAsAsync<Admin?>();

                return result;
            }
        }

        public async Task<List<Admin>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.GetAsync(constants.GETALL_ADMIN);
                var result = await response.Content.ReadAsAsync<List<Admin>>();

                return result;
            }
        }

        public async Task<string?> Update(Admin data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.ADMIN_CONTROLLER}/");

                var response = await client.PutAsJsonAsync(constants.UPDATE_ADMIN, data);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
