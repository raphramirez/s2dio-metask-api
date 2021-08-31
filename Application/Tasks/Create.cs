using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
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
            private readonly DataContext _context;
            private readonly IUsernameAccessor _usernameAccessor;
            public Handler(DataContext context, IUsernameAccessor usernameAccessor)
            {
                _usernameAccessor = usernameAccessor;
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
                var createdBy = await _context.Users.FirstOrDefaultAsync(user => user.UserName == _usernameAccessor.getUsername());

                // get assignee
                var assignee = await _context.Users.FirstOrDefaultAsync(user => user.UserName == request.Task.Assignee.UserName);

                if (assignee == null) return null;

                request.Task.CreatedBy = createdBy;
                request.Task.Assignee = assignee;

                // set DateCreated
                request.Task.DateCreated = DateTime.Now;

                request.Task.IsCompleted = false;

                _context.Tasks.Add(request.Task);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create task");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}