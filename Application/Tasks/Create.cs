using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Notifications;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Tasks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Entities.Task Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly INotificationTokenRepository _notificationTokenRepository;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(ITaskRepository taskRepository, INotificationTokenRepository notificationTokenRepository, FirebaseNotificationService notificationService)
            {
                _taskRepository = taskRepository;
                _notificationTokenRepository = notificationTokenRepository;
                _notificationService = notificationService;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Task).SetValidator(new TaskValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                //// get created by
                //var createdBy = await _userRepository.GetByUsername(_usernameAccessor.getUsername());

                //// get assignee
                //var assignee = await _userRepository.GetByUsername(request.Task.Assignee.UserName);

                //if (assignee == null) return null;

                //request.Task.CreatedBy = createdBy;
                //request.Task.Assignee = assignee;

                //// set DateCreated
                //request.Task.DateCreated = DateTime.Now;

                //request.Task.IsCompleted = false;

                //var result = _taskRepository.Add(request.Task);

                //if (!(result.Result > 0)) return Result<Unit>.Failure("Failed to create task");

                //// Notify assignee
                //var regTokens = await _notificationTokenRepository.GetUserTokens(assignee);
                //if (regTokens.Any())
                //{
                //    await FirebaseNotificationService.CreateNotificationAsync(
                //        regTokens.ToList(),
                //        "Metask",
                //        $"You have a new task: {request.Task.Name}"
                //    );
                //}

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}