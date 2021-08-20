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
        .ForMember(d => d.CreatedBy, o => o.MapFrom(x => x.Creator.UserName))
        .ForMember(d => d.Assignee, o => o.MapFrom(x => x.Assignee.UserName));
    }
  }
}