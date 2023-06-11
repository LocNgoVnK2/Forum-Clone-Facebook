using AutoMapper;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using MiniForum.Models;
using System.IO;

namespace MiniForum
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // Add as many of these lines as you need to map your objects

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => new FormFile(new MemoryStream(src.Photo), 0, src.Photo.Length, "Photo", "temp.jpg")));

            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => MapPhoto(src.Photo)));
            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<FriendFollow, User_FollowViewModel>();
            CreateMap<User_FollowViewModel, FriendFollow>();
        }

        private byte[] MapPhoto(IFormFile photo)
        {
            using (var ms = new MemoryStream())
            {
                photo?.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}