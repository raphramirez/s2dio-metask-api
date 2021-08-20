using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
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
        var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _usernameAccessor.GetUsername());

        request.Task.Creator = currentUser;

        _context.Tasks.Add(request.Task);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return Result<Unit>.Failure("Failed to create task");

        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}