using API_Assignment.DAL.DBContext;
using API_Assignment.DAL.Entity;
using API_Assignment.DAL.IRepository;

namespace API_Assignment.DAL.Repository
{
    public class UserRepository : Repository<User, BlogContext>, IUserRepository
    {
        public UserRepository(BlogContext context) : base(context)
        {

        }
    }
}
