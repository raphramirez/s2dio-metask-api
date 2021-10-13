using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Users
{
  public class RegistrationValidator : AbstractValidator<RegisterUserDto>
  {
    public RegistrationValidator()
    {
      RuleFor(user => user.Id).NotEmpty();
      RuleFor(user => user.Name).NotEmpty();
      RuleFor(user => user.Email).NotEmpty();
      RuleFor(user => user.Nickname).NotEmpty();
      RuleFor(user => user.Picture).NotEmpty();
    }
  }
}