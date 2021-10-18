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
        public class Command : IRequest<Result<string>>
        {
            public RegisterUserDto User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
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

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
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
                if (foundUser != null)
                {
                    var apiErrorResponse = new ApiErrorResponse
                    {
                        Title = "One or more validation errors occured",
                        Instance = "/api/account/register",
                        Status = (int)HttpStatusCode.BadRequest,
                        Errors = new string[]
                      {
              "Account already exists on the database."
                      }
                    };

                    return Result<string>.Failure(apiErrorResponse);
                }

                var changes = await _userRepository.Add(newUser);
                if (!(changes > 0)) return Result<string>.Failure(
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

                return Result<string>.Success("Success");
            }
        }
    }
}
