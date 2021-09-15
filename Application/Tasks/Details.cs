using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tasks
{
    public class Details
    {
        public class Query : IRequest<Result<TaskDto>>
        {
            // query parameters
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TaskDto>>
        {
            private readonly IMapper _mapper;
            private readonly PlutoContext _context;
            public Handler(PlutoContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<TaskDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var task = await _context.Tasks
                  .Include(t => t.Assignee)
                  .Include(t => t.CreatedBy)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                var taskToReturn = _mapper.Map<TaskDto>(task);

                return Result<TaskDto>.Success(taskToReturn);
            }
        }
    }
}