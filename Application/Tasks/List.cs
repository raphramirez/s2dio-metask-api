using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Tasks
{
    public class List
    {
        public class Query : IRequest<Result<List<TaskDto>>>
        {
            public TaskParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<TaskDto>>>
        {
            private readonly ILogger<List> _logger;
            private readonly ITaskRepository _taskRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IUserNameAccessor _userNameAccessor;

            public Handler(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper, IUserNameAccessor userNameAccessor)
            {
                _taskRepository = taskRepository;
                _userRepository = userRepository;
                _mapper = mapper;
                _userNameAccessor = userNameAccessor;
            }

            public async Task<Result<List<TaskDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tasks = await _taskRepository.GetAll(

                    // Includes
                    t => t.UserTasks,
                    t => t.CreatedBy
                );

                // filters
                if (request.Params.MyTasks)
                {
                    var user = _userRepository.FirstOrDefault(u => u.Id == _userNameAccessor.getUserName()).Result;
                    tasks = _taskRepository.FilterByAssignee(user, tasks);
                }

                tasks = _taskRepository.FilterByDate(request.Params.Date, tasks);

                var tasksToReturn = _mapper.Map<List<TaskDto>>(tasks);
                return Result<List<TaskDto>>.Success(tasksToReturn);
            }
        }
    }
}