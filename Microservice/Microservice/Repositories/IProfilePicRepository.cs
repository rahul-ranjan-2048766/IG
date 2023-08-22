using Microservice.Models;

namespace Microservice.Repositories
{
    public interface IProfilePicRepository
    {
        public Task<bool> Add(ProfilePic data);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<ProfilePic?> Get(int id);
        public Task<List<ProfilePic>> GetAll();
        public Task<bool> Update(ProfilePic data);
    }
}
