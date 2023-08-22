using Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context) => _context = context;

        public async Task<bool> Add(Post data)
        {
            data.Id = 0;
            data.DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                );

            await _context.Posts.AddAsync(data);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.Posts.FirstOrDefaultAsync(option => option.Id == id);
            if (data == null)
                return false;
            _context.Posts.Remove(data);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            var data = await _context.Posts.ToListAsync();
            foreach (var f in data)
                _context.Posts.Remove(f);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Post?> Get(int id) => 
            await _context.Posts.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Post>> GetAll() => await _context.Posts.ToListAsync();

        public async Task<bool> Update(Post data)
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
