using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Application.Notifications;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Tasks
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateTaskDto Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IUserRepository _userRepository;
            private readonly IUserNameAccessor _userNameAccessor;
            private readonly INotificationTokenRepository _notificationTokenRepository;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(ITaskRepository taskRepository, IUserRepository userRepository, IUserNameAccessor userNameAccessor, INotificationTokenRepository notificationTokenRepository, FirebaseNotificationService notificationService)
            {
                _taskRepository = taskRepository;
                _userRepository = userRepository;
                _userNameAccessor = userNameAccessor;
                _notificationTokenRepository = notificationTokenRepository;
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
                var newTask = new Domain.Entities.Task();

                // get created by
                var createdBy = await _userRepository.FindByAuth0Id(_userNameAccessor.getUserName());
                if (createdBy == null) return null;
                newTask.CreatedBy = createdBy;

                // get assignees
                if (request.Task.Assignees.Any())
                {
                    var assigneeIds = new List<string>();
                    foreach (var userTask in request.Task.Assignees)
                    {
                        assigneeIds.Add(userTask.Id);
                    }
                    var assignees = new List<UserTask>();
                    foreach (var id in assigneeIds)
                    {
                        var foundUser = await _userRepository.FindByAuth0Id(id);
                        assignees.Add(new UserTask
                        {
                            AppUser = foundUser
                        });
                    }
                    newTask.UserTasks = assignees;
                }

                // Info
                newTask.Name = request.Task.Name;
                newTask.DateCreated = DateTime.Now;
                newTask.Date = request.Task.Date;
                newTask.OrganizationId = request.Task.OrganizationId;
                newTask.Description = request.Task.Description;
                newTask.IsCompleted = false;

                var result = _taskRepository.Add(newTask);

                if (!(result.Result > 0)) return Result<Unit>.Failure("Failed to create task");

                //// Notify assignee
                //var regTokens = await _notificationTokenRepository.GetUserTokens(assignee);
                //if (regTokens.Any())
                //{
                //    await FirebaseNotificationService.CreateNotificationAsync(
                //        regTokens.ToList(),
                //        "Metask",
                //        $"You have a new task: {request.Task.Name}"
                //    );
                //}

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}