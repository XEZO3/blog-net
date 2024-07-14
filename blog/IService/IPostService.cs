using blog.Models;
using System.Linq.Expressions;

namespace blog.IService
{
    public interface IPostService
    {
        public Task<Response<Posts>> Create(Posts post);
        public void Update(Posts post);
        public Task<Response<Posts>> Delete(Posts post);
        public Task<Response<IEnumerable<Posts>>> GetAll(Expression<Func<Posts, bool>> predicate = null);
        public Task<Response<Posts>> Get(int id);
    }
}
