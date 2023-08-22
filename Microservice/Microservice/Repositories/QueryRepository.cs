using Microsoft.EntityFrameworkCore;
using Microservice.Models;

namespace Microservice.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly DataContext _context;
        public QueryRepository(DataContext context) => this._context = context;

        public async Task<bool> Add(Query query)
        {
            query.Id = 0;
            query.DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));
            await _context.Queries.AddAsync(query);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Query? query = await _context.Queries.FirstOrDefaultAsync(option => option.Id == id);
            if (query == null) 
                return false;
            _context.Queries.Remove(query);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAll()
        {
            List<Query> queries = await _context.Queries.ToListAsync();
            foreach (Query query in queries)
                _context.Queries.Remove(query);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Query?> Get(int id) => 
            await _context.Queries.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Query>> GetAll() =>
            await _context.Queries.ToListAsync();

        public async Task<bool> Update(Query query)
        {
            try
            {
                _context.Entry(query).State = EntityState.Modified;
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

