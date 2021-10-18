using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Profiles;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RegisterUserDto User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.User).SetValidator(new RegistrationValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                // create new appuser
                var newUser = new AppUser
                {
                    Id = request.User.Id,
                    Name = request.User.Name,
                    Nickname = request.User.Nickname,
                    Email = request.User.Email,
                    Picture = request.User.Picture,
                    UserTasks = new List<UserTask>(),
                };

                // check if user already exists
                var foundUser = _userRepository.FindByAuth0Id(newUser.Id).Result;

                int changes;
                if (foundUser == null)
                {
                    // create new
                    changes = await _userRepository.Add(newUser);
                }
                else
                {
                    // update user info
                    foundUser.Name = request.User.Name;
                    foundUser.Nickname = request.User.Nickname;
                    foundUser.Email = request.User.Email;
                    foundUser.Picture = request.User.Picture;
                    changes = await _userRepository.SaveChangesAsync();
                }

                if (!(changes > 0)) return Result<Unit>.Failure(
                      new ApiErrorResponse
                      {
                          Title = "Request failed.",
                          Instance = "/api/tasks/{id}",
                          Status = (int)HttpStatusCode.BadRequest,
                          Errors = new string[]
                        {
                           "Failed to register user to database."
                        }
                      }
                  );

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
