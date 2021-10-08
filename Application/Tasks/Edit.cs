using Application.Core;
using Application.Notifications;
using AutoMapper;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Entities.Task Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly PlutoContext _context;
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(ITaskRepository taskRepository, IMapper mapper, FirebaseNotificationService notificationService)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
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
                //var task = await _taskRepository.FirstOrDefault(x => x.Id == request.Task.Id, x => x.Assignee);

                //if (task == null) return null;

                //var oldAssignee = await _userRepository.FirstOrDefault(user => user.UserName == request.Task.Assignee.UserName);

                //// get new assignee
                //var assignee = await _userRepository.FirstOrDefault(user => user.UserName == request.Task.Assignee.UserName);
                //if (assignee == null) return Result<Unit>.Failure("Assignee does not exists.");

                //bool assigneeChanged = false;

                //if (task.Assignee.UserName != assignee.UserName)
                //{
                //    assigneeChanged = true;
                //    task.Assignee = request.Task.Assignee;
                //}
                //task.Name = request.Task.Name;
                //task.Description = request.Task.Description;
                //task.Date = request.Task.Date;

                //var result = await _context.SaveChangesAsync() > 0;

                //if (!result) return Result<Unit>.Failure("Failed to update task.");

                //if (assigneeChanged)
                //{
                //    // Notify new assignee
                //    var regTokens = await _context.NotificationTokens
                //        .Where(x => x.AppUser.UserName == assignee.UserName)
                //        .Select(t => t.Value)
                //        .ToListAsync();
                //    if (regTokens.Count > 0)
                //    {
                //        await FirebaseNotificationService.CreateNotificationAsync(
                //            regTokens,
                //            "Metask",
                //            $"You have a new task: {task.Name}"
                //        );
                //    }
                //}

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
