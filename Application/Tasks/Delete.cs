using Application.Core;
using Application.Profiles;
using Domain.Repositories;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks
{
  public class Delete
  {
    public class Command : IRequest<Result<Unit>>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
      private readonly ITaskRepository _taskRepository;

      public Handler(ITaskRepository taskRepository)
      {
        _taskRepository = taskRepository;
      }

      public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
      {
        var task = await _taskRepository.Get(request.Id);

        if (task == null) return null;

        var changes = await _taskRepository.Remove(task);

        if (!(changes > 0)) return Result<Unit>.Failure(
            new ApiErrorResponse
            {
              Title = "Request failed.",
              Instance = "/api/tasks/{id}",
              Status = (int)HttpStatusCode.BadRequest,
              Errors = new string[]
{
                "Failed to delete task."
}
            }
        );

        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}
