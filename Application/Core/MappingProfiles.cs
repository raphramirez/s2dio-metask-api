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

            CreateMap<Task, TaskDto>();

            CreateMap<UserTask, TaskDto>()
                .ForMember(d => d.Date, s => s.MapFrom(x => x.Date))
                .ForMember(d => d.DateCreated, s => s.MapFrom(x => x.DateCreated))
                .ForMember(d => d.IsCompleted, s => s.MapFrom(x => x.IsCompleted))
                .ForMember(d => d.Assignee, s => s.MapFrom(x => x.AppUser.UserName))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(x => x.Task.CreatedBy));

        }
    }
}