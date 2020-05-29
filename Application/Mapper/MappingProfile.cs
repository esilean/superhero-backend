

using Application.Activities.DTO;
using Application.Attend.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.UserName))
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.User.DisplayName));
        }

    }
}