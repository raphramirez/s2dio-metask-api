using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Application.Notifications;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tasks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Task Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly PlutoContext _context;
            private readonly IUsernameAccessor _usernameAccessor;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(PlutoContext context, IUsernameAccessor usernameAccessor, FirebaseNotificationService notificationService)
            {
                _usernameAccessor = usernameAccessor;
                _notificationService = notificationService;
                _context = context;
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
                // get created by
                var createdBy = await _context.Users
                    .FirstOrDefaultAsync(user => user.UserName == _usernameAccessor.getUsername());

                // get assignee
                var assignee = await _context.Users
                    .FirstOrDefaultAsync(user => user.UserName == request.Task.Assignee.UserName);

                if (assignee == null) return null;

                request.Task.CreatedBy = createdBy;
                request.Task.Assignee = assignee;

                // set DateCreated
                request.Task.DateCreated = DateTime.Now;

                request.Task.IsCompleted = false;

                _context.Tasks.Add(request.Task);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create task");

                // Notify assignee
                var regTokens = await _context.NotificationTokens
                    .Where(x => x.AppUser.UserName == assignee.UserName)
                    .Select(t => t.Value)
                    .ToListAsync();
                if (regTokens.Count > 0)
                {
                    await FirebaseNotificationService.CreateNotificationAsync(
                        regTokens,
                        "Metask",
                        $"You have a new task: {request.Task.Name}"
                    );
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}