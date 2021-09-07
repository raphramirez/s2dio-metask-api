using Application.Notifications;
using Application.Tasks;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Task, Task>();

            CreateMap<Task, TaskDto>()
                .ForMember(d => d.Assignee, o => o.MapFrom(s => s.Assignee))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedBy));

            CreateMap<AppUser, Profiles.Profile>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName));

            CreateMap<NotificationToken, NotificationTokenDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Token, o => o.MapFrom(s => s.Value));
        }
    }
}