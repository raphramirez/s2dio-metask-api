using Application.Core;
using Domain.Repositories;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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

                if (!(changes > 0)) return Result<Unit>.Failure("Faled to delete the task");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
