using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
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
            private readonly IMapper _mapper;

            public Handler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<TaskDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tasks = await _taskRepository.GetAll(

                    // Includes
                    t => t.UserTasks,
                    t => t.CreatedBy
                );

                var tasksToReturn = _mapper.Map<List<TaskDto>>(tasks);
                return Result<List<TaskDto>>.Success(tasksToReturn);
            }
        }
    }
}