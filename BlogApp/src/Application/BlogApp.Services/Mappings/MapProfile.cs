using AutoMapper;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Services.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //Neyi-neye dönüştüreceğimizi yazdık.
            CreateMap<Blog, BlogCardResponse>();
            CreateMap<Category, CategoryDisplayResponse>();
            CreateMap<CreateNewBlogRequest, Blog>();
            CreateMap<UserRegisterRequest, User>();
            CreateMap<UserLoginRequest, User>();
            CreateMap<BlogCardDto, BlogCardResponse>();
            CreateMap<UpdateBlogRequest, Blog>();
            CreateMap<Blog, UpdateBlogRequest>();
        }
    }
}

