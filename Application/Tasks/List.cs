using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
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
          .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);

        return Result<List<TaskDto>>.Success(tasks);
      }
    }
  }
}