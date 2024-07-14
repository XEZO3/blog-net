using blog.Models;
using System.Linq.Expressions;

namespace blog.IService
{
    public interface IPostService
    {
        public Task<bool> Create(Posts post);
        public void Update(Posts post);
        public Task<bool> Delete(Posts post);
        public Task<IEnumerable<Posts>> GetAll(Expression<Func<Posts, bool>> predicate = null);
        public Task<Posts> Get(int id);
    }
}
