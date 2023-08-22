using Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _context;
        public ChatRepository(DataContext context) => _context = context;

        public async Task<bool> Add(Chat chat)
        {
            chat.Id = 0;
            chat.DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                );

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.Chats.FirstOrDefaultAsync(option => option.Id == id);

            if (data == null)
                return false;

            _context.Chats.Remove(data);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            var data = await _context.Chats.ToListAsync();
            foreach (var f in data)
                _context.Chats.Remove(f);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Chat?> Get(int id) => 
            await _context.Chats.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Chat>> GetAll() =>
            await _context.Chats.ToListAsync();

        public async Task<bool> Update(Chat chat)
        {
            try
            {
                _context.Entry(chat).State = EntityState.Modified;
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
