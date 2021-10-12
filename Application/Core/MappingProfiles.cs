using Application.Notifications;
using Application.Tasks;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Task, Task>();

            CreateMap<Task, TaskDto>()
                .ForMember(d => d.Assignees, s => s.MapFrom(x => x.UserTasks))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(x => x.CreatedBy));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Id, s => s.MapFrom(x => x.Id))
                .ForMember(d => d.Name, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Email, s => s.MapFrom(x => x.Email))
                .ForMember(d => d.Picture, s => s.MapFrom(x => x.Picture));

            CreateMap<UserTask, Profiles.Profile>()
                .ForMember(d => d.Id, s => s.MapFrom(x => x.AppUser.Id))
               .ForMember(d => d.Name, s => s.MapFrom(x => x.AppUser.Name))
               .ForMember(d => d.Email, s => s.MapFrom(x => x.AppUser.Email))
               .ForMember(d => d.Picture, s => s.MapFrom(x => x.AppUser.Picture));

            CreateMap<NotificationToken, NotificationTokenDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.Nickname))
                .ForMember(d => d.Token, o => o.MapFrom(s => s.Value));
        }
    }
}