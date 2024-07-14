using blog.Data;
using blog.IService;
using blog.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace blog.Service
{
    public class PostService : IPostService
    {
        private readonly DBContext _dbContext;
        public PostService(DBContext dbContext) {
        _dbContext = dbContext;
        }
        public async Task<bool> Create(Posts post)
        {
            await _dbContext.Posts.AddAsync(post);
            var result = _dbContext.SaveChanges();
            if (result > 0) {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Posts post)
        {
            _dbContext.Posts.Remove(post);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Posts> Get(int id)
        {
            return _dbContext.Posts.FirstOrDefault(x=>x.Id == id);
        }

        public async Task<IEnumerable<Posts>> GetAll(Expression<Func<Posts, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await _dbContext.Posts.Include(x => x.User).Where(predicate).ToListAsync();
            }
                return await _dbContext.Posts.Include(x => x.User).ToListAsync();
        }

        public void Update(Posts post)
        {
            _dbContext.Posts.Update(post);
             _dbContext.SaveChangesAsync();
            
        }
    }
}
