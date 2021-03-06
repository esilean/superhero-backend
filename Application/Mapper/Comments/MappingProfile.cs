using System;
using System.Linq;
using Application.Comments.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => new DateTime(s.CreatedAt.Ticks, DateTimeKind.Utc)))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}