using Instagram.Models;

namespace Instagram.Services
{
    public interface IMessageService
    {
        public Task<string?> Add(Community data, string token);
        public Task<List<Community>> GetAll(string token);
    }
}
