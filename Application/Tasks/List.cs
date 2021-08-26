using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Tasks
{
  public class List
  {
    public class Query : IRequest<Result<List<TaskDto>>> { }

    public class Handler : IRequestHandler<Query, Result<List<TaskDto>>>
    {
      private readonly DataContext _context;
      private readonly ILogger<List> _logger;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Result<List<TaskDto>>> Handle(Query request, CancellationToken cancellationToken)
      {
        var tasks = await _context.Tasks
          .Include(a => a.Assignees)
          .ThenInclude(u => u.AppUser)
          .ToListAsync(cancellationToken);

        var userTasks = new List<TaskDto>();

        foreach (var task in tasks)
        {
            foreach (var assignee in task.Assignees)
            {
                userTasks.Add
                (
                  new TaskDto 
                  {
                    Name = task.Name,
                    Description = task.Description,
                    Date = assignee.Date,
                    DateCreated = assignee.DateCreated,
                    IsCompleted = assignee.IsCompleted,
                    Assignee = assignee.AppUser.UserName,
                    CreatedBy = task.CreatedBy,
                  }
                );
            }
        }

        return Result<List<TaskDto>>.Success(userTasks);
      }
    }
  }
} 