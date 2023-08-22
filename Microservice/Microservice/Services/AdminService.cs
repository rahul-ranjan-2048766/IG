using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repository;
        public AdminService(IAdminRepository repository) => this.repository = repository;

        public async Task<bool> Add(Admin admin) => await repository.Add(admin);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Admin?> Get(int id) => await repository.Get(id);

        public async Task<List<Admin>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Admin admin) => await repository.Update(admin);
    }
}
