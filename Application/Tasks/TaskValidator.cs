using Domain.Entities;
using FluentValidation;

namespace Application.Tasks
{
    public class TaskValidator : AbstractValidator<CreateTaskDto>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Name).NotEmpty();
            RuleFor(task => task.Description).NotEmpty();
            RuleFor(task => task.OrganizationId).NotEmpty();
            RuleFor(task => task.Date).NotEmpty();
        }
    }
}