using Instagram.Models;
using System.Net.Http.Headers;

namespace Instagram.Services
{
    public class ProfilePicService : IProfilePicService
    {
        private readonly Constants constants;
        public ProfilePicService(IConfiguration config) => constants = new Constants(config);

        public async Task<string?> Add(ProfilePic data, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.PROFILEPIC_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new 
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.PostAsJsonAsync(constants.ADD_PROFILEPIC, data);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return null;
        }

        public Task<string?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProfilePic?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProfilePic>> GetAll(string token)
        {
            List<ProfilePic> data = new List<ProfilePic>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    $"{constants.MICROSERVICE_API}/{constants.PROFILEPIC_CONTROLLER}/");

                client.DefaultRequestHeaders.Authorization = new 
                    AuthenticationHeaderValue(constants.AUTHENTICATION_SCHEME, token);

                var response = await client.GetAsync(constants.GETALL_PROFILEPIC);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProfilePic>>();
                    data = result;
                }
            }
            return data;
        }

        public Task<string?> Update(ProfilePic data)
        {
            throw new NotImplementedException();
        }
    }
}

