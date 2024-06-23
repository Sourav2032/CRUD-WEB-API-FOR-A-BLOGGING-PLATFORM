using API_Assignment.DAL.Entity;
using API_Assignment.Models;
using AutoMapper;

namespace API_Assignment.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<PostModel, Post>().ReverseMap();
        }
    }
}
