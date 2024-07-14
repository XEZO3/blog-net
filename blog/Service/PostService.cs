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
        public void Update(Posts post)
        {
            _dbContext.Posts.Update(post);
             _dbContext.SaveChangesAsync();
            
        }

        public async Task<Response<Posts>> Create(Posts post)
        {
            Response<Posts> response = new Response<Posts>();
            
            await _dbContext.Posts.AddAsync(post);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                response.IsSuccess = true;
                response.result = post;
            }
            else { 
                response.IsSuccess = false;
            }
            
            return response;
            
        }

        public async Task<Response<Posts>> Delete(Posts post)
        {
            Response<Posts> response = new Response<Posts>();
            
            _dbContext.Posts.Remove(post);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                response.IsSuccess = true;
            }
            else { 
            response.IsSuccess= false;
                response.Message = "";
            }
            return response;
        }

        public async Task<Response<Posts>> Get(int id)
        {
            Response<Posts> response = new Response<Posts>();
            response.IsSuccess = true;
            response.result = _dbContext.Posts.FirstOrDefault(x => x.Id == id);
            return response;
        }

        public async Task<Response<IEnumerable<Posts>>> GetAll(Expression<Func<Posts, bool>> predicate)
        {
            Response<IEnumerable<Posts>> response = new Response<IEnumerable<Posts>>();
            response.IsSuccess = true;

            if (predicate != null)
            {
                response.result = await _dbContext.Posts.Include(x => x.User).Where(predicate).ToListAsync();
                return response;
            }
            response.result = await _dbContext.Posts.Include(x => x.User).ToListAsync();
            return response;
           }
    }
}
