using Instagram.Models;

namespace Instagram.Services
{
    public interface IAdminService
    {
        public Task<string?> Add(Admin data);
        public Task<string?> Delete(int id);
        public Task<string?> DeleteAll();
        public Task<Admin?> Get(int id);
        public Task<List<Admin>> GetAll();
        public Task<string?> Update(Admin data);
    }
}
