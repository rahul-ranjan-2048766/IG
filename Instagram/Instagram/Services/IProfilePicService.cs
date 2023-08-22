using Instagram.Models;

namespace Instagram.Services
{
    public interface IProfilePicService
    {
        public Task<string?> Add(ProfilePic data, string token);
        public Task<string?> Delete(int id);
        public Task<string?> DeleteAll();
        public Task<ProfilePic?> Get(int id);
        public Task<List<ProfilePic>> GetAll(string token);
        public Task<string?> Update(ProfilePic data);
    }
}
