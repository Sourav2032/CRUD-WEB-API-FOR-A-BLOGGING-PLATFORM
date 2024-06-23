using API_Assignment.Models;

namespace API_Assignment.BAL.IServices
{
    public interface IPostService
    {
        Task<int> CreatePost(PostModel pm);
    }
}
