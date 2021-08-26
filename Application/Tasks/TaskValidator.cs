using Domain;
using FluentValidation;

namespace Application.Tasks
{
  public class TaskValidator : AbstractValidator<Task>
  {
    public TaskValidator()
    {
        RuleFor(task => task.Name).NotEmpty();
        RuleFor(task => task.CreatedBy).NotEmpty();
    }
  }
}