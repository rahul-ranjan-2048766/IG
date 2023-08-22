using Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context) => _context = context;

        public async Task<bool> Add(Comment comment)
        {
            comment.Id = 0;
            comment.DateTime = TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                );
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(option => option.Id == id);
            if (comment == null)
                return false;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            var comments = await _context.Comments.ToListAsync();
            foreach (var comment in comments)
                _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Comment?> Get(int id) =>
            await _context.Comments.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Comment>> GetAll() =>
            await _context.Comments.ToListAsync();

        public async Task<bool> Update(Comment comment)
        {
            try
            {
                _context.Entry(comment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
