using API_Assignment.DAL.DBContext;
using API_Assignment.DAL.Entity;
using API_Assignment.DAL.IRepository;

namespace API_Assignment.DAL.Repository
{
    public class PostRepository : Repository<Post, BlogContext>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context)
        {

        }
    }
}
