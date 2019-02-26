using AutoMapper;
using BlogEngine.Api.ViewModels;
using BlogEngine.Domain.Models;

namespace BlogEngine.Api.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserInputViewModel, User>();

            CreateMap<Tag, TagViewModel>();
            CreateMap<TagInputViewModel, Tag>();

            CreateMap<Post, PostViewModel>();
            CreateMap<PostInputViewModel, Post>();

            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentInputViewModel, Comment>();

            CreateMap<PostTag, PostTagViewModel>();
        }
    }
}