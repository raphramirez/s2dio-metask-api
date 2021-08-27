using Application.Tasks;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles ()
        {
            CreateMap<Task, Task>();

            CreateMap<Task, TaskDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            CreateMap<UserTask, Profiles.AssignedTask>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName));

            CreateMap<AppUser, Profiles.Profile>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName));
        }
    }
}