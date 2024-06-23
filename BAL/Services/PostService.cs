using API_Assignment.BAL.IServices;
using API_Assignment.DAL.Entity;
using API_Assignment.DAL.IRepository;
using API_Assignment.Models;
using AutoMapper;
using static API_Assignment.Helper.Enum;

namespace API_Assignment.BAL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _PostRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository pmrepo, IMapper mapper)
        {
            _PostRepo = pmrepo;
            _mapper = mapper;
        }

        public async Task<int> CreatePost(PostModel pm)
        {
            Post? newPost = _mapper.Map<Post>(pm);
            newPost.CreatedBy = (int)RoleTypeEnum.ADMIN;
            newPost.CreatedDate = DateTime.Now;
            if (_PostRepo.Add(newPost))
            {
                _PostRepo.SaveChangesManaged();
            }
            return newPost.Id;
        }


    }




    
    
        
}
