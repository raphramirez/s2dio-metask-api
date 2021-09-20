using Application.Core;
using Application.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class ToggleComplete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITaskRepository _taskRepository;
            private readonly IUsernameAccessor _usernameAccessor;
            public Handler(IUserRepository userRepository, ITaskRepository taskRepository, IUsernameAccessor usernameAccessor)
            {
                _userRepository = userRepository;
                _taskRepository = taskRepository;
                _usernameAccessor = usernameAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // get user
                var user = await _userRepository.FirstOrDefault(u => u.UserName == _usernameAccessor.getUsername());

                // get task
                var task = await _taskRepository.Get(request.Id);
                if (task == null) return null;

                var result = await _taskRepository.ToggleComplete(task) > 0;

                if (!result) return Result<Unit>.Failure("Failed to create task");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
