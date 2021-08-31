using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
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
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
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
                var task = await _context.Tasks.FindAsync(request.Task.Id);

                if (task == null) return null;

                // get assignee
                var assignee = await _context.Users.FirstOrDefaultAsync(user => user.UserName == request.Task.Assignee.UserName);
                if (assignee == null) return Result<Unit>.Failure("Assignee does not exists.");

                task.Assignee = request.Task.Assignee;
                task.Name = request.Task.Name;
                task.Description = request.Task.Description;
                task.Date = request.Task.Date;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update task.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
