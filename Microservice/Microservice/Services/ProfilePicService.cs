using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class ProfilePicService : IProfilePicService
    {
        private readonly IProfilePicRepository repository;
        public ProfilePicService(IProfilePicRepository repository) => this.repository = repository;

        public async Task<bool> Add(ProfilePic data) => await repository.Add(data);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<ProfilePic?> Get(int id) => await repository.Get(id);

        public async Task<List<ProfilePic>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(ProfilePic data) => await repository.Update(data);
    }
}

