using Application.Core;
using Application.Notifications;
using AutoMapper;
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
            public Domain.Task Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly PlutoContext _context;
            private readonly IMapper _mapper;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(PlutoContext context, IMapper mapper, FirebaseNotificationService notificationService)
            {
                _context = context;
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
                var task = await _context.Tasks
                    .Include(a => a.Assignee)
                    .FirstOrDefaultAsync(x => x.Id == request.Task.Id);

                if (task == null) return null;

                var oldAssignee = await _context.Users.FirstOrDefaultAsync(user => user.UserName == request.Task.Assignee.UserName);

                // get new assignee
                var assignee = await _context.Users.FirstOrDefaultAsync(user => user.UserName == request.Task.Assignee.UserName);
                if (assignee == null) return Result<Unit>.Failure("Assignee does not exists.");

                bool assigneeChanged = false;

                if (task.Assignee.UserName != assignee.UserName)
                {
                    assigneeChanged = true;
                    task.Assignee = request.Task.Assignee;
                }
                task.Name = request.Task.Name;
                task.Description = request.Task.Description;
                task.Date = request.Task.Date;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update task.");

                if (assigneeChanged)
                {
                    // Notify new assignee
                    var regTokens = await _context.NotificationTokens
                        .Where(x => x.AppUser.UserName == assignee.UserName)
                        .Select(t => t.Value)
                        .ToListAsync();
                    if (regTokens.Count > 0)
                    {
                        await FirebaseNotificationService.CreateNotificationAsync(
                            regTokens,
                            "Metask",
                            $"You have a new task: {task.Name}"
                        );
                    }
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
