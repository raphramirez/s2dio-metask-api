using Application.Core;
using Application.Profiles;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class RemoveAssignee
    {
        public class Command : IRequest<Result<string>>
        {
            public Guid TaskId { get; set; }
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IUserRepository _userRepository;

            public Handler(ITaskRepository taskRepository, IUserRepository userRepository)
            {
                _taskRepository = taskRepository;
                _userRepository = userRepository;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var task = _taskRepository.FirstOrDefault(
                    t => t.Id == request.TaskId,
                    // includes
                    t => t.UserTasks
                    ).Result;
                if (task == null) return null;

                var user = _userRepository.FindByAuth0Id(request.UserId).Result;
                if (user == null) return null;

                var changes = await _taskRepository.RemoveAssignee(task, user);

                if (!(changes > 0)) return Result<string>.Failure(
                    new ApiErrorResponse
                    {
                        Title = "Request failed.",
                        Instance = "/api/tasks/{id}/assign",
                        Status = (int)HttpStatusCode.BadRequest,
                        Errors = new string[]
              {
                "Failed to remove assignee."
              }
                    }
                );

                return Result<string>.Success("Success");
            }
        }
    }
}
