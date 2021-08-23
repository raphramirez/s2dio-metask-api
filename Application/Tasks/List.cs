using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Tasks
{
  public class List
  {
    public class Query : IRequest<List<Domain.Task>> { }

    public class Handler : IRequestHandler<Query, List<Domain.Task>>
    {
      private readonly DataContext _context;
      private readonly ILogger<List> _logger;

      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<List<Domain.Task>> Handle(Query request, CancellationToken cancellationToken)
      {
        return await _context.Tasks.ToListAsync(cancellationToken);
      }
    }
  }
}