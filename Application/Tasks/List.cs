using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Tasks
{
    public class List
    {
        public class Query : IRequest<Result<List<TaskDto>>>
        {
            public TaskParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<TaskDto>>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;
            private readonly IMapper _mapper;
            private readonly IUsernameAccessor _usernameAccessor;

            public Handler(DataContext context, IMapper mapper, IUsernameAccessor usernameAccessor)
            {
                _mapper = mapper;
                _usernameAccessor = usernameAccessor;
                _context = context;
            }

            public async Task<Result<List<TaskDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Tasks
                  .Include(a => a.Assignee)
                  .Include(c => c.CreatedBy)
                  .OrderBy(t => t.Date)
                  .AsQueryable();

                if (request.Params.MyTasks && !request.Params.ShowAll)
                {
                    query = query.Where(t => t.Assignee.UserName == _usernameAccessor.getUsername());
                }

                var tasks = await query.ToListAsync(cancellationToken);

                var tasksToReturn = _mapper.Map<List<TaskDto>>(tasks);

                return Result<List<TaskDto>>.Success(tasksToReturn);
            }
        }
    }
}