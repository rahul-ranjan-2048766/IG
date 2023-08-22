using Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context) => this._context = context;

        public async Task<bool> Add(Community data)
        {
            data.Id = 0;
            data.DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));
            await _context.GetCommunity.AddAsync(data);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.GetCommunity.FirstOrDefaultAsync(option => 
                            option.Id == id);

            if (data != null)
                _context.GetCommunity.Remove(data);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAll()
        {
            var messages = await _context.GetCommunity.ToListAsync();
            foreach (var message in messages)
                _context.GetCommunity.Remove(message);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Community?> Get(int id) =>
            await _context.GetCommunity.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Community>> GetAll() => 
            await _context.GetCommunity.ToListAsync();

        public async Task<bool> Update(Community data)
        {
            try
            {
                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
