using API_Assignment.BAL.IServices;
using API_Assignment.DAL.Entity;
using API_Assignment.DAL.IRepository;
using API_Assignment.Models;
using AutoMapper;

namespace API_Assignment.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository umrepo, IMapper mapper)
        {
            _UserRepo = umrepo;
            _mapper = mapper;
        }

        public async Task<int> CreateUser(UserModel um)
        {
            User? newUser = _mapper.Map<User>(um);
            if (_UserRepo.Add(newUser))
            {
                _UserRepo.SaveChangesManaged();
            }
            return newUser.Id;
        }
    }
}
