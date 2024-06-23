using API_Assignment.Models;

namespace API_Assignment.BAL.IServices
{
    public interface IUserService
    {
        Task<int> CreateUser(UserModel um);
    }
}
