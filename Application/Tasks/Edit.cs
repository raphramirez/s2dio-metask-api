using Application.Core;
using Application.Notifications;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateTaskDto Task { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly PlutoContext _context;
            private readonly ITaskRepository _taskRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly FirebaseNotificationService _notificationService;

            public Handler(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper, FirebaseNotificationService notificationService)
            {
                _taskRepository = taskRepository;
                _userRepository = userRepository;
                _mapper = mapper;
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
                var task = await _taskRepository.FirstOrDefault(x => x.Id == request.Task.Id,
                    // includes
                    x => x.UserTasks,
                    x => x.CreatedBy
                );

                if (task == null) return null;

                task.Name = request.Task.Name;
                task.Description = request.Task.Description;
                task.Date = request.Task.Date;

                var changes = _taskRepository.Edit();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}